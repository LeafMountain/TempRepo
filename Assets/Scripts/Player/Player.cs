using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    #region Variables
    #region Variables set in the editor
    [SerializeField] private GameObject playerCharacter;
    [SerializeField] private float characterSelectCooldown = 0.5f;
    #endregion Variables set in the editor
    private int selectedCharacter = 0;
    private float characterSelectCooldownTimer = 0;
    private int lifesLeft;
    #region Properties
    public bool PlayerReady { get; set; } = false;
    public int LifesLeft { get => lifesLeft; }
    public int PlayerID { get; set; }
    public int GamePadID { get; set; }
    public int GetSelectedCharacter { get => selectedCharacter; }
    public Gamepad GetGamePad { get => (Gamepad)InputSystem.GetDeviceById(GamePadID); }
    public GameObject GetPlayerCharacterGameObject { get => playerCharacter; }
    public Character GetPlayerCharacterScript { get => PlayerManager.instance.PlayableCharacters[selectedCharacter]; }
    #endregion Properties
    #endregion Variables

    #region Unity Methods
    private void Update()
    {
        // Decrease the timer if it's set
        if(characterSelectCooldownTimer > 0)
        {
            characterSelectCooldownTimer -= Time.deltaTime;
        }
    }
    #endregion Unity Methods

    #region Character Methods
    public void ActivateCharacter()
    {
        // Instantiate the selected character
        playerCharacter = Instantiate(PlayerManager.instance.PlayableCharacters[selectedCharacter]?.GetPrefab, transform);

        // Set lifes left to the set starting lifes
        lifesLeft = GameManager.instance.StartingLifes;

        // Activate the gameobject
        playerCharacter.SetActive(true);
    }

    public void DeactivateCharacter()
    {
        // Reset the position
        playerCharacter.transform.position = Vector3.zero;

        // Deactivate the gameobject
        playerCharacter.SetActive(false);
    }

    #endregion Character Methods

    #region Control Event Methods
    private void OnMove(InputValue value)
    {
        // If we're in the Character select screen, the player haven't marked themself
        // as ready yet and the character select cooldown timer is less then or equal to 0
        if(IsCharacterSelectScene() && !PlayerReady && characterSelectCooldownTimer <= 0)
        {
            // Read the value of the input stick
            Vector2 stickMovement = value.Get<Vector2>();

            // If it's right then increase selected character and set cooldown
            // If it's left then decrease selected character and set cooldown
            if(stickMovement.x > 0.5)
            {
                selectedCharacter++;
                characterSelectCooldownTimer = characterSelectCooldown;
            }
            else if(stickMovement.x < -0.5)
            {
                selectedCharacter--;
                characterSelectCooldownTimer = characterSelectCooldown;
            }

            // Make sure that the selected character is not less than 0 and not more than
            // the amount of playable characters. If it is then set it to the opposite to
            // make it look like you can go "around"
            if(selectedCharacter < 0)
            {
                selectedCharacter = PlayerManager.instance.PlayableCharacters.Count - 1;
            }
            else if(selectedCharacter >= PlayerManager.instance.PlayableCharacters.Count)
            {
                selectedCharacter = 0;
            }

            // Update the character portrait
            PlayerManager.instance.UpdateSelectedCharacterPortrait(selectedCharacter, PlayerID);
        }
    }

    private void OnJump()
    {
        // Make sure that the current scene is the character select scene
        if(IsCharacterSelectScene())
        {
            PlayerReady = !PlayerReady;
            PlayerManager.instance.UpdateReadyText(PlayerID);
        }
    }
    #endregion Control Event Methods

    private bool IsCharacterSelectScene()
    {
        // This line took too much place so it decreased readability so I created a method for it
        return GameManager.instance.CurrentScene == GameManager.instance.CharacterSelectSceneIndex;
    }

    public void RemoveLife()
    {
        lifesLeft--;
    }
}
