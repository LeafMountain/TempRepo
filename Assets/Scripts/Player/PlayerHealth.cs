using UnityEngine;
using System;
using UnityEngine.Assertions;

[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(PlayerCharacter))]
[RequireComponent(typeof(Player))]
public class PlayerHealth : MonoBehaviour
{
    #region Variables

    public float healthValue;
    [NonSerialized]public bool healthValueReset;
    public UiHealthValue uiHealthValue;

    // Sorry for the wierd structure here, didn't want to remove Johannes code
    // The above is his variables and the following is mine.
    [SerializeField] private float startingHealth;

    private PlayerMovement playerMovement;
    private PlayerCharacter playerCharacter;
    private Player player;

    #region Properties
    public float DefaultHealthValue { get => startingHealth; }
    public int GetDashes { get { if (playerMovement == null) { playerMovement = GetComponent<PlayerMovement>(); } return playerMovement.NumberOfDashes; } }
    public int LifesLeft { get { if (player == null) { player = GetComponentInParent<Player>(); } return player.LifesLeft; } }
    #endregion Properties

    #endregion Variables

    private void Awake()
    {
        healthValue = startingHealth;
        playerMovement = GetComponent<PlayerMovement>();
        playerCharacter = GetComponent<PlayerCharacter>();
        player = GetComponentInParent<Player>();

        Assert.IsNotNull(playerMovement, "Player Movement script could not be found on Player");
        Assert.IsNotNull(playerCharacter, "Player Character script could not be found on Player");
        Assert.IsNotNull(player, "Player script could not be found on Player");
    }

    public void ResetHealth()
    {
        // Reset the players health back to it's original value
        healthValue = startingHealth;
    }

    public void TakeDamage(float damage)
    {
        // Remove damage and then check for death
        healthValue -= damage;
        CheckForDeath();
    }

    public void KillPlayer()
    {
        // Kill the player by setting their health to -1 and
        // then check for death.
        healthValue = -1;
        CheckForDeath();
    }

    public void CheckForDeath()
    {
        // Only check for death on players that's ready for a
        // fight.
        if (playerCharacter.ReadyForFight)
        {
            // If the players health is less than or equal to 0
            // call player died.
            if (healthValue <= 0)
            {
                PlayerManager.instance.PlayerDied(transform);
            }
        }
    }
}
