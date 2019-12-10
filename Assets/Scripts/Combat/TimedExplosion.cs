using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class TimedExplosion : MonoBehaviour
{
    #region Variables

    #region Unity Components

    private Animator animator;

    #endregion unity Components

    #region Set In Editor

    [Header("Timers")]
    [SerializeField] private float startExplodeTimer;    
    [Header("Damage")]
    [SerializeField] private float explosionDamage;

    #endregion Set In Editor

    #region Local

    private HashSet<GameObject> targets = new HashSet<GameObject>();
    private float explodeTimer;    
    private bool destroy;
    private float animationTimer;
    private float startAnimationTimer = 0.3f;

    #endregion Local

    #endregion Variables

    #region Unity Functions

    void Start()
    {
        animator = GetComponent<Animator>();
        Assert.IsNotNull(animator, "Failed to find Animator component.");

        ResetExplodeTimer();
        ResetAnimationTimer();
    }

    private void Update()
    {
        explodeTimer -= Time.deltaTime;
        if (explodeTimer <= 0 && !destroy)
        {
            animator.SetBool("Explode", true);
            destroy = true;
        }

        if (destroy)
        {
            animationTimer -= Time.deltaTime;
            if (animationTimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    private void FixedUpdate()
    {
        if (destroy && animationTimer <= startAnimationTimer)
        {
            AffectGameObjects();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        targets.Add(collision.gameObject);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        targets.Remove(collision.gameObject);
    }

    #endregion Unity Functions

    #region Functions

    private void AffectGameObjects()
    {
        foreach(GameObject gameObject in targets)
        {
            PlayerCharacter playerCharacter = gameObject.GetComponent<PlayerCharacter>();
            if (playerCharacter != null)
            {
                playerCharacter.TakeDamage(explosionDamage);
            }

            // Todo: fix push. First attempt at pushing gameObjects aside from blast. 
            // Not used but left for future attempts. 

            /*
            Rigidbody2D rigidbody = gameObject.GetComponent<Rigidbody2D>();
            if (rigidbody != null)
            {
                rigidbody.AddForce(new Vector2(gameObject.transform.position.x * explosionDamage * 20, gameObject.transform.position.y * explosionDamage * 20), ForceMode2D.Impulse);
            }
            */
        }
    }

    #endregion Functions

    #region Reset

    private void ResetExplodeTimer()
    {
        explodeTimer = startExplodeTimer;
    }

    private void ResetAnimationTimer()
    {
        animationTimer = startAnimationTimer;
    }

    #endregion Reset
}
