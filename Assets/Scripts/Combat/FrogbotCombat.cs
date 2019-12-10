using UnityEngine;
using UnityEngine.Assertions;

#region Requirements

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
[RequireComponent(typeof(ReadPlayerInput))]

#endregion Requirements

public class FrogbotCombat : MonoBehaviour
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
    [SerializeField] private Transform firePoint;
    [Header("Import from Prefabs")]
    [SerializeField] private GameObject laserPrefab;
    [SerializeField] private GameObject waterBlastPrefab;
    [SerializeField] private GameObject minePrefab;
    [Header("Timers")]
    [SerializeField] private float shotTimer = 0.5f;
    [Header("Distances")]
    [SerializeField] private float reach = 0.5f;
    [Header("Damage")]
    [SerializeField] private int laserDamage = 5;           // At the moment controlled from bullet script
    [SerializeField] private int waterBlastDamage = 5;      // At the moment controlled from bullet script
    [SerializeField] private int flameThrowerDamage = 5;
    [Header("Collider Tags")]
    [SerializeField] private string playerTag;

    #endregion Set In Editor

    #region Local

    private float currentShotTime = 0f;
    private bool waterBlast;
    private bool laser;
    private bool flamethrower;

    #endregion Local

    #endregion Variables

    #region Unity Functions

    private void Start()
    {
        Assert.IsNotNull(laserPrefab, "Failed to find LaserPrefab GameObject.");

        Assert.IsNotNull(minePrefab, "Failed to find MinePrefab GameObject.");

        Assert.IsNotNull(waterBlastPrefab, "Failed to find WaterBlastPrefab GameObject.");

        Assert.IsNotNull(firePoint, "Failed to find FirePoint component.");

        animator = GetComponent<Animator>();
        Assert.IsNotNull(animator, "Failed to find Animator component.");

        playerMovement = GetComponent<PlayerMovement>();
        Assert.IsNotNull(playerMovement, "Failed to find PlayerMovement script.");

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
        WaterBlast();
        Laser();
        Flamethrower();
    }

    #endregion Unity Functions

    #region Input

    private void InputHandler()
    {
        if (readPlayerInput.Shoot != Vector2.zero && currentShotTime <= 0)
        {
            laser = true;
            currentShotTime = shotTimer;
        }

        if (readPlayerInput.AttackRequest && currentShotTime <= 0)
        {
            waterBlast = true;
            currentShotTime = shotTimer;
        }
        readPlayerInput.AttackRequest = false;

        if (readPlayerInput.ChargedShotPressed && currentShotTime <= 0)
        {
            flamethrower = true;
            currentShotTime = shotTimer;
        }
        readPlayerInput.ChargedShotPressed = false;
    }

    #endregion Input

    #region Actions

    private void WaterBlast()
    {
        if (waterBlast)
        {
            animator.SetBool("Attack", true);
            GameObject bullet = Instantiate(waterBlastPrefab, firePoint.position, firePoint.rotation);
            bullet.GetComponent<Bullet>().SetDirection(Vector3.right);

            // In the process of changing how projectiles and hits work.

            /*
            bool facingRight = GetComponent<PlayerMovement>().FacingRight;
            Vector2 fromPosition = new Vector2(facingRight ? transform.position.x + 1 : transform.position.x - 1, transform.position.y + 1);
            Vector2 direction = new Vector2(facingRight ? 1 : -1, 0);
            RaycastHit2D raycastHit = Physics2D.Raycast(fromPosition, direction, reach);
            if (raycastHit)
            {
                if (raycastHit.transform.tag.Equals(playerTag) && raycastHit.transform != transform)
                {
                    raycastHit.transform.GetComponent<PlayerCharacter>().TakeDamage(waterBlastDamage);
                }
            }
            */

            waterBlast = false;
        }
        else
        {
            animator.SetBool("Attack", false);
        }
    }

    private void Laser()
    {
        if (laser)
        {
            float yShootDirection = readPlayerInput.Shoot.y;
            playerMovement.CheckDirection();
            animator.SetBool("Shoot", true);
            animator.SetFloat("YShootDirection", yShootDirection);
            if (yShootDirection < -0.75)
            {
                Instantiate(minePrefab, firePoint.position, firePoint.rotation);
            }
            else
            {
                GameObject bullet = Instantiate(laserPrefab, firePoint.position, firePoint.rotation);
                bullet.GetComponent<Bullet>().SetDirection(Vector3.right);

                // In the process of changing how projectiles and hits work.

                /*
                bool facingRight = GetComponent<PlayerMovement>().FacingRight;
                Vector2 fromPosition = new Vector2(facingRight ? transform.position.x + 1 : transform.position.x - 1, transform.position.y + 1);
                Vector2 direction = new Vector2(facingRight ? 1 : -1, 0);
                RaycastHit2D raycastHit = Physics2D.Raycast(fromPosition, direction, reach);
                if (raycastHit)
                {
                    if (raycastHit.transform.tag.Equals(playerTag) && raycastHit.transform != transform)
                    {
                        raycastHit.transform.GetComponent<PlayerCharacter>().TakeDamage(laserDamage);
                    }
                }
                */
            } 

            laser = false;
        }
        else
        {
            animator.SetBool("Shoot", false);
        }
    }

    private void Flamethrower()
    {
        if (flamethrower && playerMovement.Grounded)
        {
            animator.SetBool("ChargedShot", true);
            bool facingRight = GetComponent<PlayerMovement>().FacingRight;
            Vector2 fromPosition = new Vector2(facingRight ? transform.position.x + 1 : transform.position.x - 1, transform.position.y + 1);
            Vector2 direction = new Vector2(facingRight ? 1 : -1, 0);
            RaycastHit2D raycastHit = Physics2D.Raycast(fromPosition, direction, reach);
            if (raycastHit)
            {
                if (raycastHit.transform.tag.Equals(playerTag) && raycastHit.transform != transform)
                {
                    raycastHit.transform.GetComponent<PlayerCharacter>().TakeDamage(flameThrowerDamage);
                }
            }
            flamethrower = false;
        }
        else
        {
            animator.SetBool("ChargedShot", false);
        }
    }

    #endregion Actions
}
