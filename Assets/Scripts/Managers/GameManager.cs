using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    #region Variables
    #region Editor variables
    [Header("Scenes")]
    [SerializeField] private int mainMenuSceneIndex;
    [SerializeField] private int characterSelectSceneIndex;
    [SerializeField] private int playSceneIndex;
    [SerializeField] private int winSceneIndex;

    [Header("Play Settings")]
    [SerializeField] private int numberOfLives = 3;
    #endregion Editor variables

    private List<GameObject> uiPlayerInformation = new List<GameObject>();

    public static GameManager instance;

    #region Property variables
    public int StartingLifes { get => numberOfLives; }
    public int MainMenuSceneIndex { get => mainMenuSceneIndex; }
    public int CharacterSelectSceneIndex { get => characterSelectSceneIndex; }
    public int PlaySceneIndex { get => playSceneIndex; }
    public int WinSceneIndex { get => winSceneIndex; }
    public int CurrentScene { get; private set; }
    #endregion Property variables
    #endregion Variables
    #region Unity Methods
    private void Awake()
    {
        // Create a singleton out of this unless there's already one 
        // and make sure it doesn't get destroyed on scene change
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
        CurrentScene = mainMenuSceneIndex;
    }
    private void OnEnable()
    {
        // Add the OnSceneLoaded to the SceneManagers event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    #endregion Unity Methods
    #region SceneManegment
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Set the current scene to the buildindex
        CurrentScene = scene.buildIndex;

        if(CurrentScene == playSceneIndex)
        {
            PlayerManager.instance.StartNewGameRound();
            // If this is the playscene then we need to assign the right UI
            // to the right player.
            GameObject playerInformation = GameObject.Find("PlayerInformation");
            
            // Loop through every ui transform
            for(int i = 0; i < playerInformation.transform.childCount; i++)
            {
                // Add it to a ui list to keep track of if we need to clear the UI
                uiPlayerInformation.Add(playerInformation.transform.GetChild(i).gameObject);
                
                // Inactivate those gameobjects that not will be used
                if(PlayerManager.instance.GetPlayerCount - 1 < i)
                {
                    playerInformation.transform.GetChild(i).gameObject.SetActive(false);
                }
                else
                {
                    UiHealthValue uiHealthValue = playerInformation.transform.GetChild(i).GetComponent<UiHealthValue>();
                    Assert.IsNotNull(uiHealthValue, "UI Health value could not be found.");

                    PlayerHealth playerHealth = PlayerManager.instance.GetPlayerWithID(i + 1).GetComponentInChildren<PlayerHealth>();
                    Assert.IsNotNull(playerHealth, "Player Health component could not be found on player");


                    uiHealthValue.playerHealth = playerHealth;
                    playerHealth.uiHealthValue = uiHealthValue;
                }
            }
            AudioManager.Instance.PlayGameMusic();
            PlaySceneManager.Instance.StartScene();
        }
        else
        {
            if(CurrentScene == mainMenuSceneIndex)
            {
                // Play menu music
                AudioManager.Instance.PlayMenuMusic();
            }
            else if(CurrentScene == characterSelectSceneIndex)
            {
                GameObject playerCards = GameObject.Find("PlayerCards");

                if (PlayerManager.instance.GetPlayerCount > 0)
                {
                    int i = 0;
                    foreach (var playerControllerPair in PlayerManager.instance.Players)
                    {
                        int selectedCharacter = playerControllerPair.Value.GetComponent<Player>().GetSelectedCharacter;
                        playerCards.transform.GetChild(i).GetComponentInChildren<PlayerCard>().ActivatePlayerCard();
                        playerCards.transform.GetChild(i).GetComponentInChildren<PlayerCard>().UpdateHandPosition(selectedCharacter, i);
                        playerCards.transform.GetChild(i).GetComponentInChildren<PlayerCard>().UpdatePortrait(PlayerManager.instance.PlayableCharacters[selectedCharacter].GetSelectPortrait);
                        playerCards.transform.GetChild(i).GetComponentInChildren<Text>().text = PlayerManager.instance.PlayableCharacters[selectedCharacter].GetName.ToUpper();
                        i++;
                    }
                }
            }
            if(uiPlayerInformation.Count > 0)
            {
                // Clear the UI
                uiPlayerInformation.Clear();
            }
        }
    }

    public void LoadScene(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    #endregion SceneManegment
}
