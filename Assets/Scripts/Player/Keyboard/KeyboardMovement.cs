using UnityEngine;
using UnityEngine.Assertions;

// This was just a quick attempt to get some basic keyboard functionality 
// to test out the game without controllers. Probably more to come. 
// It is also based on an older version of the mevement script.
// Currently not used.

#region Requirements

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]

#endregion Requirements

public class KeyboardMovement : MonoBehaviour
{
    #region Variables

    #region Unity Components
    
    private Animator animator;
    private Rigidbody2D playerRigidbody;    
    private SpriteRenderer spriteRenderer;
    private Transform playerTransform;

    #endregion Unity Components

    #region Set In Editor

    [Header("Choose groundLayers")]
    [SerializeField] private LayerMask mask;
    [Header("Forces")]
    [SerializeField] private float airForce;
    [SerializeField] private float maxAirSpeed;
    [Header("Speed")]
    [SerializeField] private float jumpForce;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float startSpeed;
    [Header("Timers")]
    [SerializeField] private float startDashTime;
    [SerializeField] private float dashDrop;
    [SerializeField] private int startNumberOfDashes;
    [Header("Abilities")]
    [SerializeField] private float startDashCooldown;

    #endregion Set In Editor

    #region Public
 
    public float facingDirection;

    #endregion Public

    #region Local

    private float dashCooldown;
    private int numberOfDashes;
    private float dashTime;
    private bool jump;
    private bool falling;
    private bool dash;
    private bool grounded;
    private bool facingRight = true;
    private bool move;
    private bool dashUp;
    private Vector2 moveDirection;

    #endregion Local

    #endregion Variables

    #region Unity Functions

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Assert.IsNotNull(spriteRenderer, "Failed to find SpriteRenderer component.");

        animator = GetComponent<Animator>();
        Assert.IsNotNull(animator, "Failed to find Animator component.");

        playerTransform = GetComponent<Transform>();
        Assert.IsNotNull(playerTransform, "Failed to find Transform component.");

        playerRigidbody = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(playerRigidbody, "Failed to find Rigibody2D component.");

        dashTime = startDashTime;
        numberOfDashes = startNumberOfDashes;
        dashCooldown = startDashCooldown;
    }

    void Update()
    {
        InputHandler();
        CheckIfGrounded();
    }

    private void FixedUpdate()
    {
        Move();
        DashHandler();
    }

    #endregion Unity Functions

    #region Input

    private void InputHandler()
    {
        animator.SetFloat("Speed", Mathf.Abs(playerRigidbody.velocity.x));

        if (Input.GetKeyDown(KeyCode.D))
        {
            move = true;
            moveDirection = Vector2.right;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            move = true;
            moveDirection = Vector2.left;
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            dashUp = true;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (numberOfDashes > 0)
            {
                dash = true;
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (grounded)
            {
                jump = true;
            }  
        }
    }

    #endregion Input

    #region Movement

    private void Move()
    {
        if (move)
        {
            CheckDirection();
            if (falling && facingRight && Mathf.Abs(playerRigidbody.velocity.x) < maxAirSpeed)
            {
                playerRigidbody.AddForce(new Vector2(airForce, 0));
            }
            else if (falling && !facingRight && Mathf.Abs(playerRigidbody.velocity.x) < maxAirSpeed)
            {
                playerRigidbody.AddForce(new Vector2(-airForce, 0));
            }
            else if (!facingRight && grounded)
            {
                playerRigidbody.velocity = new Vector2(-startSpeed, 0f);
            }
            else if (facingRight && grounded)
            {
                playerRigidbody.velocity = new Vector2(startSpeed, 0f);
            }

            if (dash)
            {
                if (numberOfDashes > 0)
                {
                    Dash();
                }
            }
            move = false;
        }

        if (jump)
        {
            Jump();
        }
    }

    private void Dash()
    {
        if (dashUp)
        {
            animator.SetBool("DashUp", true);
            animator.SetBool("Jumping", false);
        }
        else
        {
            animator.SetBool("Dash", true);
            animator.SetBool("Jumping", false);
        }

        CheckDirection();

        if (dashTime <= 0)
        {
            animator.SetBool("Dash", false);
            animator.SetBool("DashUp", false);
            numberOfDashes--;
            Debug.Log(numberOfDashes);
            dash = false;
            dashUp = false;
            dashTime = startDashTime;
            dashDrop = 0.1f;
        }
        else
        {
            playerRigidbody.velocity = moveDirection * dashSpeed;
            dashTime -= Time.deltaTime;
            playerRigidbody.velocity -= new Vector2(playerRigidbody.velocity.x * dashDrop, playerRigidbody.velocity.y * dashDrop);
            dashDrop += 0.1f;
        }
    }

    private void Jump()
    {
        CheckDirection();
        animator.SetBool("Jumping", true);
        playerRigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        falling = true;
        jump = false;
        grounded = false;
    }

    #endregion Movement

    #region Helper Functions

    private void DashHandler()
    {
        if (numberOfDashes <= 0)
        {
            dashCooldown -= Time.fixedDeltaTime;
            /*
            if (dashCooldown == startDashCooldown - 1)
            {
                //dashCooldown = startDashCooldown;
                numberOfDashes++;
            }
            */
            if (dashCooldown <= 0)
            {
                numberOfDashes = startNumberOfDashes;
                dashCooldown = startDashCooldown;
            }
        }
    }

    #region Debug

#if UNITY_EDITOR

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(transform.position, new Vector2(2f, 0.01f));  
    }

#endif

    #endregion Debug

    private void CheckIfGrounded()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down, Color.red, 0.1f);
        grounded = Physics2D.OverlapArea(new Vector2(transform.position.x, transform.position.y), new Vector2(transform.position.x + 2, transform.position.y - 0.2f), mask);
        
        if (grounded)
        {
            numberOfDashes = startNumberOfDashes;
            animator.SetBool("Falling", false);
            animator.SetBool("Jumping", false);
            falling = false;
        }
        else
        {
            falling = true;
            animator.SetBool("Falling", true);
        }
    }

    private void CheckDirection()
    {
        facingDirection = moveDirection.x;
        if (facingDirection > 0 && !facingRight)
        {
            transform.Rotate(0f, 180f, 0f);
            facingRight = true;
        }
        else if (facingDirection < 0 && facingRight)
        {
            transform.Rotate(0f, 180f, 0f);
            facingRight = false;
        }
    }

    #endregion Helper Functions
}