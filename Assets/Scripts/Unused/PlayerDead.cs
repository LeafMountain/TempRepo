using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerDead : MonoBehaviour
{
    public PlayerHealth playerHealth;

    [SerializeField]private bool playerDead;

    private void Update()
    {
        if (playerHealth.healthValue >= 0)
        {
            DisablePlayer();
        }
    }

    private void DisablePlayer()
    {
        gameObject.SetActive(false);
    }
}
