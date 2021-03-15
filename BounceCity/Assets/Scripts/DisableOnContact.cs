using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnContact : MonoBehaviour
{
    public float respawnTime; //The time that the object is disabled for.
    private void OnCollisionEnter(Collision collision) //disable the object when the player collides
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            gameObject.SetActive(false);
            Invoke("Respawn", respawnTime);
        }
    }

    void Respawn() //re-enable the object after a period of time
    {
        gameObject.SetActive(true);
    }
}
