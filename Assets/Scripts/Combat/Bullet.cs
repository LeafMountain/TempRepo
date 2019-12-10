using UnityEngine;
using UnityEngine.Assertions;

#region Requirements

[RequireComponent(typeof(Rigidbody2D))]

#endregion Requirements

public class Bullet : MonoBehaviour
{
    #region Variables

    #region Unity Components

    private Rigidbody2D bulletRigidbody;

    #endregion Unity Components

    #region Set In Editor

    [Header("Import from Prefabs")]
    [SerializeField] private GameObject impactEffect;
    [Header("Speed")]
    [SerializeField] private float bulletSpeed;
    [Header("Damage")]
    [SerializeField] private float damage;
    [Header("Timers")]
    [SerializeField] private float lifeSpan;
    [Header("Collider Tags")]
    [SerializeField] private string playerTag;
    [SerializeField] private string bulletTag;

    #endregion Set In Editor

    #region Local

    private float currentTime;

    #endregion Local

    #endregion Variables

    #region Unity Functions

    private void Start()
    {
        Assert.IsNotNull(impactEffect, "Failed to find ImpactEffect GameObject.");
        
        bulletRigidbody = GetComponent<Rigidbody2D>();
        Assert.IsNotNull(bulletRigidbody, "Failed to find Rigibody2D component.");
    }

    private void Update()
    {
        currentTime += Time.deltaTime;

        if(currentTime >= lifeSpan)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(impactEffect, transform.position, transform.rotation);
        if(collision.gameObject.tag == playerTag)
        {
            PlayerCharacter playerCharacter = collision.transform.GetComponent<PlayerCharacter>();
            if (playerCharacter != null)
            {
                playerCharacter.TakeDamage(damage);
            }
            else
            {
                collision.transform.GetComponent<PlayerHealth>().TakeDamage(damage);
                collision.transform.GetComponent<PlayerSounds>().PlayHitSound();
            }
        }
        else if(collision.gameObject.tag == bulletTag)
        {
            AudioManager.Instance.PlayBulletExplosion();
            Destroy(collision.gameObject);
        }
        Destroy(gameObject);
    }

    #endregion Unity Functions

    #region Functions

    public void SetDirection(Vector3 direction)
    {
        if(bulletRigidbody == null)
        {
            bulletRigidbody = GetComponent<Rigidbody2D>();
        }
        bulletRigidbody.velocity = transform.TransformDirection(direction) * bulletSpeed * Time.deltaTime;
    }

    #endregion Functions
}