using UnityEngine;
using UnityEngine.Assertions;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(ToggleSpriteColor))]
[RequireComponent(typeof(PlayerSounds))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(Player))]
public class PlayerCharacter : MonoBehaviour
{
    #region Variables

    #region Scripts
    private ToggleSpriteColor toggleSpriteColor;
    private Player player;
    private PlayerHealth playerHealth;
    private PlayerSounds playerSounds;
    private PlayerMovement playerMovement;
    #endregion scripts

    #region Timers and bools
    private float currentVibrationTimer = 0f;
    private bool vibrating = false;
    private bool readyForFight = false;
    #endregion Timers and bools

    #region Set in the editor
    [Header("Vibration settings")]
    [SerializeField] private float highFrequenceMotorSpeed = 0.5f;
    [SerializeField] private float lowFrequenceMotorSpeed = 0.5f;
    [SerializeField] private float vibrationTimer = 0.3f;
    #endregion Set in the editor

    #region Properties
    public bool ReadyForFight { get => readyForFight; }
    #endregion Properties

    #endregion Variables
    #region Unity Methods
    private void Awake()
    {
        toggleSpriteColor = GetComponent<ToggleSpriteColor>();
        Assert.IsNotNull(toggleSpriteColor, "Toggle sprite color script could not be found.");

        playerHealth = GetComponent<PlayerHealth>();
        Assert.IsNotNull(playerHealth, "Player health could not be found.");

        playerSounds = GetComponent<PlayerSounds>();
        Assert.IsNotNull(playerSounds, "Player sounds could not be found.");

        playerMovement = GetComponent<PlayerMovement>();
        Assert.IsNotNull(playerMovement, "Player movement could not be found.");

        player = GetComponentInParent<Player>();
        Assert.IsNotNull(player, "Player could not be found.");
    }

    private void Update()
    {
        if(vibrating)
        {
            currentVibrationTimer -= Time.deltaTime;
            
            if(currentVibrationTimer <= 0)
            {
                StopVibration();
            }
        }
    }
    #endregion Unity Methods
    #region Damage Methods
    public void TakeDamage(float damage)
    {

        playerHealth.TakeDamage(damage);
        toggleSpriteColor.StartToggle();
        playerSounds.PlayHitSound();
        vibrating = true;
        currentVibrationTimer = vibrationTimer;
        player.GetGamePad.SetMotorSpeeds(lowFrequenceMotorSpeed, highFrequenceMotorSpeed);
    }

    public void ResetHealth()
    {
        if (!playerHealth)
        {
            return;
        }

        playerHealth.ResetHealth();
    }
    #endregion Damage Methods
    #region Character Methods
    public void ResetCharacter()
    {
        if(!playerHealth || !playerMovement || !player)
        {
            return;
        }
        StopVibration();
        transform.position = Vector3.zero;
        playerMovement.ResetDashes();
        playerHealth.ResetHealth();
    }

    public void ReadyCharacter()
    {
        readyForFight = true;
    }

    public void UnreadyCharacter()
    {
        readyForFight = false;
    }
    #endregion Character Methods
    #region Vibration Methods
    public void StopVibration()
    {
        vibrating = false;
        player.GetGamePad.SetMotorSpeeds(0.0f, 0.0f);
    }
    #endregion Vibration Methods
}
