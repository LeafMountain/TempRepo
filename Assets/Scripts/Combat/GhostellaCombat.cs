using UnityEngine;
using UnityEngine.Assertions;

#region Requirements

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(ReadPlayerInput))]

#endregion Requirements

public class GhostellaCombat : MonoBehaviour
{
    #region Variables

    #region Unity Components

    private Animator animator;      
    private CapsuleCollider2D playerCollider;  
    private Rigidbody2D playerRigidbody;

    #endregion Unity Components

    #region Imported Classes

    private PlayerMovement playerMovement;    
    private ReadPlayerInput readPlayerInput;

    #endregion Imported Classes

    #region Set In Editor

    [Header("Import from child object")]
    [SerializeField] private Transform firePoint;
    [Header("Import from Prefabs")]
    [SerializeField] private GameObject bulletPrefab;
    [Header("Timers")]
    [SerializeField] private float shotTimer = 0.5f;
    [SerializeField] private float heavyShotTimer = 0.2f;
    [Header("Distances")]
    [SerializeField] private float reach = 0.5f;
    [SerializeField] private float blinkRange = 5f;
    [Header("Damage")]
    [SerializeField] private int headButDamage = 5;
    [SerializeField] private int heavyProjectileDamage = 5;     // At the moment controlled from bullet script
    [SerializeField] private int spinAttackDamage = 5;
    [Header("Collider Tags")]
    [SerializeField] private string playerTag;

    #endregion Set In Editor

    #region Local

    private float currentShotTime = 0f;
    private float heavyShotDelay = 0f;
    private bool headbutt;
    private bool blink;
    private bool heavyProjectile;
    private bool heavyShotFired = false;

    #endregion Local

    #endregion Variables

    #region Unity Functions

    private void Start()
    {
        Assert.IsNotNull(firePoint, "Failed to find FirePoint component.");

        playerRigidbody = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(playerRigidbody, "Failed to find Rigidbody2D script.");

        playerCollider = GetComponent<CapsuleCollider2D>();
        Assert.IsNotNull(playerCollider, "Failed to find CapsuleCollider2D component.");

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
        if (heavyShotDelay > 0)
        {
            heavyShotDelay -= Time.deltaTime;
        }
        if (PlaySceneManager.Instance.RoundRunning)
        {
            InputHandler();
        }
    }

    private void FixedUpdate()
    {
        Headbutt();
        Blink();
        HeavyProjectile();
        CheckForHeavyShot();
    }

    #endregion Unity Functions

    #region Input

    private void InputHandler()
    {
        if (readPlayerInput.Shoot != Vector2.zero && currentShotTime <= 0)
        {
            blink = true;
            currentShotTime = shotTimer;
        }

        if (readPlayerInput.AttackRequest)
        {
            headbutt = true;
        }
        readPlayerInput.AttackRequest = false;

        if (readPlayerInput.ChargedShotPressed && currentShotTime <= 0)
        {
            heavyProjectile = true;
            currentShotTime = shotTimer;
            heavyShotDelay = heavyShotTimer;
            heavyShotFired = true;
        }
        readPlayerInput.ChargedShotPressed = false;
    }

    #endregion Input

    #region Actions

    private void Headbutt()
    {
        if (headbutt)
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
                    raycastHit.transform.GetComponent<PlayerCharacter>().TakeDamage(headButDamage);
                }
            }

            headbutt = false;
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }

    private void Blink()
    {
        if (blink)
        {
            float yShootDirection = readPlayerInput.Shoot.y;
            if (yShootDirection < 0)
            {
                // Todo: Try out attackPoints for left and right and tighten colliders
                animator.SetBool("Shoot", true);
                animator.SetFloat("YShootDirection", yShootDirection);
                bool facingRight = GetComponent<PlayerMovement>().FacingRight;
                Vector2 fromPosition = new Vector2(facingRight ? transform.position.x + 1 : transform.position.x - 1, transform.position.y + 1);
                Vector2 direction = new Vector2(facingRight ? 1 : -1, 0);
                RaycastHit2D raycastHit = Physics2D.Raycast(fromPosition, direction, reach);
                if (raycastHit)
                {
                    if (raycastHit.transform.tag.Equals(playerTag) && raycastHit.transform != transform)
                    {
                        raycastHit.transform.GetComponent<PlayerCharacter>().TakeDamage(spinAttackDamage);
                    }
                }
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
            }
            else
            {
                playerRigidbody.MovePosition(transform.position + new Vector3(readPlayerInput.Shoot.normalized.x * blinkRange, readPlayerInput.Shoot.normalized.y * blinkRange, 0));
                // Todo: toggle player collider while blinking
            }
            blink = false;
        }
        else
        {
            animator.SetBool("Shoot", false);
        }
        
    }

    private void HeavyProjectile()
    {
        if (heavyProjectile && playerMovement.Grounded)
        {
            animator.SetBool("ChargedShot", true);

            heavyProjectile = false;
        }
        else
        {
            animator.SetBool("ChargedShot", false);
        }
    }

    private void CheckForHeavyShot()
    {
        if (heavyShotFired && heavyShotDelay <= 0)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Bullet>().SetDirection(Vector3.right);
            heavyShotFired = false;
        }
    }

    #endregion Functions
}
