using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody body; // the RigidBody of this component's GameObject
    MessageBox messageBox; // the MessageBox that displays text.

    public float speed, // the horizontal speed at which the player moves forward
                 rotateSpeed, // the speed at which the player rotates
                 jumpPower, // how high the player can bounce
                 jumpFactor, // the rate at which the player's jump
                 lowerLimit, // the lowest elevation the player can reach
                 upperLimit; // the highest elevation the player can reach

    private float moveX, // the horizontal input from the user
                  moveY; // the vertical input from the user

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
        messageBox = MessageBox.Get();
    }

    // Called once per physics frame. Sets the speed and rotation based on user input
    void FixedUpdate() 
    {
        body.AddForce(gameObject.transform.forward * moveY * speed);
        transform.Rotate(Vector2.up * moveX * rotateSpeed);
    }

    // This method gets the value of the user input
    private void OnMove(InputValue movementValue) 
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        moveX = movementVector.x;
        moveY = movementVector.y;
    }

    // Called once per frame
    void Update() 
    {
        if (transform.position.y > upperLimit ||
            lowerLimit > transform.position.y)
        {
            // If the player goes out of bounds restart the game
            Restart();
        }
    }

    // Restarts the game
    void Restart() 
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    // Called when the Player object collides
    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Trampoline")) // the player bounces higher on a trampoline
        {
            body.AddForce(Vector2.up * jumpPower);
            jumpPower *= jumpFactor;
        }
        else if (other.CompareTag("Goal")) // the player completes the game when they reach the goal. the game will reset
        {
            messageBox.Victory();
            Invoke("Restart", messageBox.duration);
        }
    }
}
