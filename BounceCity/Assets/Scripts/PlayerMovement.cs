using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    Rigidbody body; // the RigidBody of this component's GameObject

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
    }

    void FixedUpdate() //set the force and rotation based on user input (these are physics calculations)
    {
        body.AddForce(gameObject.transform.forward * moveY * speed);
        transform.Rotate(Vector2.up * moveX * rotateSpeed);
    }

    private void OnMove(InputValue movementValue) //get the value of the user input
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        moveX = movementVector.x;
        moveY = movementVector.y;
    }

    void Update() //if the player goes out of bounds restart the game
    {
        if (transform.position.y > upperLimit ||
            lowerLimit > transform.position.y)
        {
            Restart();
        }
    }

    void Restart() //restarts the game
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameObject other = collision.gameObject;
        if (other.CompareTag("Trampoline"))
        {
            body.AddForce(Vector2.up * jumpPower);
            jumpPower *= jumpFactor;
        }
    }
}
