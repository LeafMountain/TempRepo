using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempDoDamaged : MonoBehaviour
{
    PlayerHealth playerHealth;

    public float damagedAmount = 25f;
    private void Awake()
    {
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            playerHealth.TakeDamage(damagedAmount);
        }
    }
}
