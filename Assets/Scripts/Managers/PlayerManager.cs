using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    #region Variables

    [Header("Player Prefab")]
    [SerializeField] private GameObject playerPrefab;
    [Header("Playable Characters")]
    [SerializeField] private List<string> characters = new List<string>();
    [SerializeField] private List<Character> playableCharacters = new List<Character>();
    [Header("Settings")]
    [SerializeField] private int minPlayers = 2;
    [SerializeField] private int maxPlayers = 4;

    public int ConnectedControllers { get => Gamepad.all.Count; }
    public int GetPlayerCount { get => players.Count; }
    public List<string> Characters { get => characters; }
    public int PlayersAlive { get; private set; }
    public List<Player> DeadPlayers { get => playersDead; }
    public List<Character> PlayableCharacters { get => playableCharacters; }
    public Dictionary<int, GameObject> Players { get => players; }

    private Dictionary<int, GameObject> players = new Dictionary<int, GameObject>();
    private List<Player> playersAlive = new List<Player>();
    private List<Player> playersDead = new List<Player>();

    private GameObject playerCards;

    public static PlayerManager instance;

    #endregion Variables

    #region Unity Methods

    private void Awake()
    {
        // Create a singleton if it doesn't already exist.
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }

    private void Update()
    {
        // Check if this is the Character select screen
        if (GameManager.instance.CurrentScene == GameManager.instance.CharacterSelectSceneIndex)
        {
            // If a player presses the startbutton and is not already is the list and the playercount is less than 4
            if (Gamepad.current.startButton.wasPressedThisFrame && !players.ContainsKey(Gamepad.current.deviceId) && players.Count < maxPlayers)
            {
                if (playerCards == null)
                {
                    playerCards = GameObject.Find("PlayerCards");
                } 
                CreateNewPlayer(Gamepad.current);
            }
            // If a player presses the startbutton and every player is ready with at least min players
            if (Gamepad.current.startButton.wasPressedThisFrame && CheckIfEveryPlayerIsReady() && players.Count >= minPlayers)
            {
                SceneManager.LoadScene(GameManager.instance.PlaySceneIndex);
            }
            // If the east button is pressed then unchoose every character and load the main menu scene
            if (Gamepad.current.buttonEast.wasPressedThisFrame)
            {
                UnChooseEveryCharacter();
                SceneManager.LoadScene(GameManager.instance.MainMenuSceneIndex);
            }
        }

        // If we're at the playsceneindex and start and select is pressed then return to the
        // main menu. This was mainly used as a debug feature during development
        if(GameManager.instance.CurrentScene == GameManager.instance.PlaySceneIndex)
        {
            if(Gamepad.current.startButton.isPressed && Gamepad.current.selectButton.wasPressedThisFrame)
            {
                DeactivateEveryCharacter();
                SceneManager.LoadScene(GameManager.instance.MainMenuSceneIndex);
            }
        }

        // If we're at the winscene and press the south button then  return to the main menu
        if (GameManager.instance.CurrentScene == GameManager.instance.WinSceneIndex && Gamepad.current.buttonSouth.wasPressedThisFrame)
        {
            GameManager.instance.LoadScene(GameManager.instance.MainMenuSceneIndex);
        }
    }
    #endregion Unity Methods

    #region Other Methods

    private void CreateNewPlayer(Gamepad gamepad)
    {
        // Create a new player and add it to the players list
        GameObject newPlayer = GameObject.Instantiate(playerPrefab);
        players.Add(gamepad.deviceId, newPlayer);

        // Set the gameobjects name to display player number and gamepad id and set it's
        // parent to be this transform so it survives scenechanges.
        newPlayer.name = $"Player {players.Count} [Gamepad: {gamepad.deviceId}]";
        newPlayer.transform.parent = transform;

        // Get the Playerscript
        Player player = newPlayer.GetComponent<Player>();

        //Set the ID and gamepadID in the Playerscript
        newPlayer.GetComponent<Player>().PlayerID = players.Count;
        newPlayer.GetComponent<Player>().GamePadID = gamepad.deviceId;

        // Update character select UI to show the playerid and update the portrait
        UpdateSelectedCharacterPortrait(newPlayer.GetComponent<Player>().GetSelectedCharacter, players.Count);
        playerCards.transform.GetChild(players.Count - 1).GetComponent<PlayerCard>().ActivatePlayerCard();
    }

    private bool CheckIfEveryPlayerIsReady()
    {
        // Loop through every player in the playerlist and if any of them is not
        // ready then return false. Otherwise return true
        foreach (var player in players)
        {
            if(!player.Value.GetComponent<Player>().PlayerReady)
            {
                return false;
            }
        }

        return true;
    }

    public void UpdateSelectedCharacterPortrait(int id, int playerID)
    {
        // Make sure that we're at the character select index
        if(GameManager.instance.CurrentScene == GameManager.instance.CharacterSelectSceneIndex)
        {
            if (playerCards == null)
            {
                playerCards = GameObject.Find("PlayerCards");
            }
            // Get the playercard that belongs to the playerid (-1 since those begin with 1 and not 0)
            // Then get the selectect character from the Player script
            PlayerCard currentPlayerCard = playerCards.transform.GetChild(playerID - 1).GetComponentInChildren<PlayerCard>();
            int selectedCharacter = GetPlayerWithID(playerID).GetComponent<Player>().GetSelectedCharacter;

            Assert.IsNotNull(currentPlayerCard, "Player card component could not be found on playercard");

            // Update portrait and hand position
            currentPlayerCard.UpdatePortrait(playableCharacters[selectedCharacter].GetSelectPortrait);
            currentPlayerCard.UpdateHandPosition(selectedCharacter, playerID);

            // Update the characters name
            playerCards.transform.GetChild(playerID - 1).GetComponentInChildren<Text>().text = playableCharacters[selectedCharacter].GetName.ToUpper();
        }
    }

    public void UpdateReadyText(int playerID)
    {
        // Make sure that we're at the character select index
        if (GameManager.instance.CurrentScene == GameManager.instance.CharacterSelectSceneIndex)
        {
            if (playerCards == null)
            {
                GameObject.Find("PlayerCards");
            }
            // Get the player card that belongs to the player id (-1 since those begin with 1 and not 0)
            PlayerCard currentPlayerCard = playerCards.transform.GetChild(playerID - 1).GetComponent<PlayerCard>();
            Assert.IsNotNull(currentPlayerCard, "Player card component could not be found on player card");
            // Toggle the ready text
            currentPlayerCard.ToggleReadyText();
        }
    }

    public GameObject GetPlayerWithID(int playerID)
    {
        // Loop through every player in the player list and then check for the one that
        // has the player id that was given.
        foreach (var playerControllerPair in players)
        {
            if(playerControllerPair.Value.GetComponent<Player>().PlayerID == playerID)
            {
                return playerControllerPair.Value;
            }
        }

        return null;
    }

    public void UnChooseEveryCharacter()
    {
        // Run deactivate character on every player
        foreach (var player in players)
        {
            Player play = player.Value.GetComponent<Player>();
            Assert.IsNotNull(play, "Player script not found on Player");
            
            play.DeactivateCharacter();
        }

        UnreadyEveryPlayer();
    }

    public void UnreadyEveryPlayer()
    {
        // Run through every player and set their ready status to false
        foreach (var player in players)
        {
            Player play = player.Value.GetComponent<Player>();
            Assert.IsNotNull(play, "Player script not found on Player");

            play.PlayerReady = false;
        }
    }

    public void ResetEveryCharacter()
    {
        foreach (var playerControllerPair in players)
        {
            // Loop through every player and call reset character
            Player play = playerControllerPair.Value.GetComponent<Player>();
            Assert.IsNotNull(play, "Player script not found on player.");

            PlayerCharacter playerCharacter = play.GetPlayerCharacterGameObject.GetComponent<PlayerCharacter>();

            playerCharacter?.ResetCharacter();
        }
    }

    public void StartNewGameRound()
    {
        // Every round Unready every player, reset every character,
        // clear both alive and dead players.

        UnreadyEveryPlayer();
        ResetEveryCharacter();
        playersAlive.Clear();
        playersDead.Clear();

        int playerIndex = 0;

        foreach (var playerControllerPair in players)
        {
            // Loop through every player and activate their character
            // and ready their character. Then add the character to the
            // camera follow script and the player to the players alive list.
            // Lastly set the players position to a spawnpoints position.
            Player player = playerControllerPair.Value.GetComponent<Player>();
            player.ActivateCharacter();
            
            PlayerCharacter playerCharacter = player.GetPlayerCharacterGameObject.GetComponent<PlayerCharacter>();
            playerCharacter.ReadyCharacter();
            
            MainCamera mainCamScript = Camera.main.GetComponent<MainCamera>();
            mainCamScript.CameraTargets.Add(player.GetPlayerCharacterGameObject.transform);

            playersAlive.Add(player);
            player.GetPlayerCharacterGameObject.transform.position = PlaySceneManager.Instance.SpawnPoints[playerIndex++].position;
        }
    }

    public void DeactivateEveryCharacter()
    {
        foreach (var playerControllerPair in players)
        {
            Player player = playerControllerPair.Value.GetComponent<Player>();
            Assert.IsNotNull(player, "Player script could not be found on player.");

            MainCamera mainCamScript = Camera.main.GetComponent<MainCamera>();

            // Unready every character and remove them from the camera target scripts
            // and then deactivate them.
            player.GetPlayerCharacterGameObject.GetComponent<PlayerCharacter>().UnreadyCharacter();
            Camera.main.GetComponent<MainCamera>().CameraTargets.Remove(player.GetPlayerCharacterGameObject.transform);
            player.DeactivateCharacter();
        }
    }

    public void PlayerDied(Transform player)
    {
        // Get the player script and remove a life
        Player playerScript = player.GetComponentInParent<Player>();
        playerScript.RemoveLife();

        // If a player doesn't have any lifes left then we need to remove them from the game
        // remove them from the players alive list and add them to players dead list

        // If there's only one character left then he have won so we need to load the winscene.
        if(playerScript.LifesLeft <= 0)
        {
            player.gameObject.SetActive(false);
            Camera.main.GetComponent<MainCamera>().CameraTargets.Remove(player);
            playersAlive.Remove(playerScript);
            playersDead.Add(playerScript);

            if (playersAlive.Count <= 1)
            {
                foreach (var playerAlive in playersAlive)
                {
                    playersDead.Add(playerAlive);
                }
                DeactivateEveryCharacter();
                SceneManager.LoadScene(GameManager.instance.WinSceneIndex);
            }
        }
        else
        {
            // Player have lifes left so reposition it to a spawnpoint and reset it's health
            int startPos = Random.Range(0, PlaySceneManager.Instance.SpawnPoints.Length);

            PlayerCharacter playerCharacter = playerScript.GetPlayerCharacterGameObject.GetComponent<PlayerCharacter>();
            Assert.IsNotNull(playerCharacter, "Player character script could not be found on player");

            playerCharacter.transform.position = PlaySceneManager.Instance.SpawnPoints[startPos].position;
            playerCharacter.ResetHealth();
        }
    }

    #endregion Other Methods
}
