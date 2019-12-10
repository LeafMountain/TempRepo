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
        readPlayerInput.AttackRequest = false;

        if (readPlayerInput.ChargedShotPressed)
        {
            heavyAttack = true;
        }
        readPlayerInput.ChargedShotPressed = false;
    }

    #endregion Input

    #region Actions

    private void SpinAttack()
    {
        if (spinAttack)
        {
            // Todo: Try out attackPoint and tighten colliders
            animator.SetBool("Attack", true);
            bool facingRight = GetComponent<PlayerMovement>().FacingRight;
            Vector2 fromPosition = new Vector2(facingRight ? transform.position.x + 1 : transform.position.x - 1, transform.position.y + 1);
            Vector2 direction = new Vector2(facingRight ? 1 : -1, 0);
            RaycastHit2D raycastHit = Physics2D.Raycast(fromPosition, direction, reach);
            if(raycastHit)
            {
                if(raycastHit.transform.tag.Equals(playerTag) && raycastHit.transform != transform)
                {
                    raycastHit.transform.GetComponent<PlayerCharacter>().TakeDamage(spinAttackDamage);
                }
            }
            // Todo: Try out attackPoint and tighten colliders
            fromPosition = new Vector2(facingRight ? transform.position.x - 1 : transform.position.x + 1, transform.position.y + 1);
            direction = new Vector2(facingRight ? -1 : 1, 0);
            raycastHit = Physics2D.Raycast(fromPosition, direction, reach);
            if (raycastHit)
            {
                if (raycastHit.transform.tag.Equals(playerTag) && raycastHit.transform != transform)
                {
                    raycastHit.transform.GetComponent<PlayerCharacter>().TakeDamage(spinAttackDamage);
                }
            }

            spinAttack = false;
        }
        else
        {
            animator.SetBool("Attack", false);
        }
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
                RaycastHit2D raycastHit = Physics2D.Raycast(upAttackPoint.position, Vector2.up, reach);
                if (raycastHit)
                {
                    if (raycastHit.transform.tag.Equals(playerTag) && raycastHit.transform != transform)
                    {
                        raycastHit.transform.GetComponent<PlayerCharacter>().TakeDamage(directionalAttackDamage);
                    }
                }
            }
            else
            {
                // Todo: Try out attackPoint and tighten colliders
                bool facingRight = GetComponent<PlayerMovement>().FacingRight;
                Vector2 fromPosition = new Vector2(facingRight ? transform.position.x + 1 : transform.position.x - 1, transform.position.y + 1);
                Vector2 direction = new Vector2(facingRight ? 1 : -1, 0);
                RaycastHit2D raycastHit = Physics2D.Raycast(fromPosition, direction, reach);
                if (raycastHit)
                {
                    if (raycastHit.transform.tag.Equals(playerTag) && raycastHit.transform != transform)
                    {
                        raycastHit.transform.GetComponent<PlayerCharacter>().TakeDamage(directionalAttackDamage);
                    }
                }
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
            RaycastHit2D raycastHit = Physics2D.Raycast(fromPosition, direction, reach * 2); // longer reach
            if (raycastHit)
            {
                if (raycastHit.transform.tag.Equals(playerTag) && raycastHit.transform != transform)
                {
                    raycastHit.transform.GetComponent<PlayerCharacter>().TakeDamage(heavyAttackDamage);
                }
            }
            heavyAttack = false;
        }
        else
        {
            animator.SetBool("ChargedShot", false);
        }
    }

    #endregion Actions
}
