using UnityEngine;
using UnityEngine.Assertions;

#region Requirements

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(ReadPlayerInput))]

#endregion Requirements

public class PacmonsterCombat : MonoBehaviour
{
    #region Variables

    #region Unity Components

    private Animator animator;

    #endregion Unity Components

    #region Imported Classes

    private PlayerMovement playerMovement;
    private ReadPlayerInput readPlayerInput;

    #endregion Imported Classes

    #region Set In Editor

    [Header("Import from child object")]
    [SerializeField] private Transform attackPoint;     // Todo: Try out attackPoint and tighten colliders
    [SerializeField] private Transform upAttackPoint;
    [Header("Timers")]
    [SerializeField] private float shotTimer = 0.5f;
    [Header("Distances")]
    [SerializeField] private float reach = 0.5f;
    [Header("Damage")]
    [SerializeField] private int spinAttackDamage = 5;
    [SerializeField] private int directionalAttackDamage = 5;
    [SerializeField] private int heavyAttackDamage = 5;
    [Header("Collider Tags")]
    [SerializeField] private string playerTag;

    #endregion Set In Editor

    #region Local

    private float currentShotTime = 0f;
    private bool spinAttack;
    private bool directionalAttack;
    private bool heavyAttack;

    #endregion Local

    #endregion Variables

    #region Unity Functions

    private void Start()
    {
        playerMovement = GetComponent<PlayerMovement>();
        Assert.IsNotNull(playerMovement, "Failed to find PlayerMovement script.");
        
        animator = GetComponent<Animator>();
        Assert.IsNotNull(animator, "Failed to find Animator component.");
        
        readPlayerInput = GetComponentInParent<ReadPlayerInput>();
        Assert.IsNotNull(readPlayerInput, "Failed to find ReadPlayerInput script.");
    }

    void Update()
    {
        if(currentShotTime > 0)
        {
            currentShotTime -= Time.deltaTime;
        }

        InputHandler();
    }

    private void FixedUpdate()
    {
        SpinAttack();
        DirectionalAttack();
        HeavyAttack();
    }

    #endregion Unity Functions

    #region Input

    private void InputHandler()
    {
        if (readPlayerInput.Shoot != Vector2.zero && currentShotTime <= 0)
        {
            directionalAttack = true;
            currentShotTime = shotTimer;
        }

        if (readPlayerInput.AttackRequest)
        {
            spinAttack = true;
        }
        // TODO: Decouple this. The PacmonsterCombat shouldn't control the input
        readPlayerInput.AttackRequest = false;

        if (readPlayerInput.ChargedShotPressed)
        {
            heavyAttack = true;
        }
        // TODO: Decouple this. The PacmonsterCombat shouldn't control the input
        readPlayerInput.ChargedShotPressed = false;
    }

    #endregion Input

    #region Actions

    private void SpinAttack()
    {
        //TODO: This code is very messy and hard to understand. Try to make this clearer. It might even be a good idea to go into the PlayerMovement and changing "FacingRight" to the actual direction.
        if (spinAttack)
        {
            // Todo: Try out attackPoint and tighten colliders
            animator.SetBool("Attack", true);
            bool facingRight = GetComponent<PlayerMovement>().FacingRight;
            Vector2 fromPosition = new Vector2(facingRight ? transform.position.x + 1 : transform.position.x - 1, transform.position.y + 1);
            Vector2 direction = new Vector2(facingRight ? 1 : -1, 0);

            // TODO: Made it its own method.
            GetNearbyPlayer(fromPosition, direction, reach)?.TakeDamage(spinAttackDamage);

            // TODO: While turnary might feel cool to use, this makes it very hard to easily see what's happening
            fromPosition = new Vector2(facingRight ? transform.position.x - 1 : transform.position.x + 1, transform.position.y + 1);
            direction = new Vector2(facingRight ? -1 : 1, 0);

            GetNearbyPlayer(fromPosition, direction, reach)?.TakeDamage(spinAttackDamage);

            spinAttack = false;
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }

    private PlayerCharacter GetNearbyPlayer(Vector2 fromPosition, Vector2 direction, float reach)
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(fromPosition, direction, reach);
        // raycastHit cannot be null. Check transfrom instead
        if(raycastHit.transform == transform)
        {
            return null;
        }

        return raycastHit.transform?.GetComponent<PlayerCharacter>();
    }

    private void DirectionalAttack()
    {
        if (directionalAttack)
        {
            float yShootDirection = readPlayerInput.Shoot.y;
            playerMovement.CheckDirection();
            animator.SetBool("Shoot", true);
            animator.SetFloat("YShootDirection", yShootDirection);
            if (yShootDirection > 0.75)
            {
                // TODO: Like I wrote before, this code has been written multiple times. Use a method instead.
                GetNearbyPlayer(upAttackPoint.position, Vector2.up, reach)?.TakeDamage(directionalAttackDamage);
            }
            else
            {
                // Todo: Try out attackPoint and tighten colliders
                bool facingRight = GetComponent<PlayerMovement>().FacingRight;
                Vector2 fromPosition = new Vector2(facingRight ? transform.position.x + 1 : transform.position.x - 1, transform.position.y + 1);
                Vector2 direction = new Vector2(facingRight ? 1 : -1, 0);
                
                // TODO: Like I wrote before, this code has been written multiple times. Use a method instead.
                GetNearbyPlayer(fromPosition, direction, reach)?.TakeDamage(directionalAttackDamage);
            }

            directionalAttack = false;
        }
        else
        {
            animator.SetBool("Shoot", false);
        }
    }

    private void HeavyAttack()
    {
        // Todo: Try out attackPoint and tighten colliders
        if (heavyAttack && playerMovement.Grounded)
        {
            animator.SetBool("ChargedShot", true);
            bool facingRight = GetComponent<PlayerMovement>().FacingRight;
            Vector2 fromPosition = new Vector2(facingRight ? transform.position.x + 1 : transform.position.x - 1, transform.position.y + 1);
            Vector2 direction = new Vector2(facingRight ? 1 : -1, 0);

            GetNearbyPlayer(fromPosition, direction, reach * 2)?.TakeDamage(heavyAttackDamage);
            heavyAttack = false;
        }
        else
        {
            animator.SetBool("ChargedShot", false);
        }
    }

    #endregion Actions
}
