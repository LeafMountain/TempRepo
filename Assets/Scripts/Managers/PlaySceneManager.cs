using UnityEngine;
using UnityEngine.UI;

public class PlaySceneManager : MonoBehaviour
{
    #region Variables

    [Header("SpawnPoints")]
    [SerializeField] private Transform[] spawnPoints;
    [Header("Start Settings")]
    [SerializeField] private bool countdown = true;
    [SerializeField] private float countdownTime = 3f;
    [SerializeField] private float showBrawlForTime = 1f;
    [SerializeField] private Image numberDisplay;
    [SerializeField] private Sprite countdownThree;
    [SerializeField] private Sprite countdownTwo;
    [SerializeField] private Sprite countdownOne;
    [SerializeField] private Sprite countdownBrawl;

    private float currentCountdownTime = 0f;
    private float currentBrawlCountdown = 0f;

    private bool roundRunning = false;
    private bool showingBrawl = false;
    private bool showingCountdown = false;

    public static PlaySceneManager Instance;
    public Transform[] SpawnPoints { get => spawnPoints; }
    public bool RoundRunning { get => roundRunning; }

    #endregion Variables

    #region Unity Functions

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Update()
    {
        Countdown();
        DisplayBrawl();

    }

    private void OnDestroy()
    {
        roundRunning = false;
        Instance = null;
    }

    #endregion Unity Functions

    #region Functions

    public void StartScene()
    {
        if(countdown)
        {
            currentCountdownTime = countdownTime;
            numberDisplay.sprite = countdownThree;
            showingCountdown = true;
        }
    }
    private void Countdown()
    {
        if (showingCountdown)
        {
            currentCountdownTime -= Time.deltaTime;

            if (currentCountdownTime <= 0f)
            {
                numberDisplay.sprite = countdownBrawl;
                currentBrawlCountdown = showBrawlForTime;

                showingCountdown = false;
                showingBrawl = true;
                roundRunning = true;
            }
            else if (currentCountdownTime <= 1f)
            {
                numberDisplay.sprite = countdownOne;
            }
            else if (currentCountdownTime <= 2f)
            {
                numberDisplay.sprite = countdownTwo;
            }
        }
    }

    private void DisplayBrawl()
    {
        if(showingBrawl)
        {
            currentBrawlCountdown -= Time.deltaTime;
            
            if(currentBrawlCountdown <= 0)
            {
                numberDisplay.color = Color.clear;
            }
        }
    }

    #endregion Functions
}
