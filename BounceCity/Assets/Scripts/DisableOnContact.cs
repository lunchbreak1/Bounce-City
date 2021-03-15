using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnContact : MonoBehaviour
{
    public float respawnTime; // The time that the object is disabled for.

    // Disable the object when the player collides
    private void OnCollisionEnter(Collision collision) 
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Invoke("Respawn", respawnTime);
        }
    }

    // Re-enable the object after a period of time
    void Respawn() 
    {
        gameObject.SetActive(true);
    }
}
