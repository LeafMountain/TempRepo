using UnityEngine;
using UnityEngine.Assertions;

public class Void : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        // If the collider that enters has the tag player then call kill it
        // otherwise just set the gameobject to false.
        if(col.transform.tag == "Player")
        {
            PlayerHealth playerHealth = col.transform.GetComponent<PlayerHealth>();
            Assert.IsNotNull(playerHealth, $"Player without PlayerHealth component: {col.gameObject.name}");
            playerHealth?.KillPlayer();
        }
        else
        {
            col.gameObject.SetActive(false);
        }
    }
}
