using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    [SerializeField] private float movementDistance; // Total distance to move left/right and up/down
    [SerializeField] private float speed; // Movement speed
    [SerializeField] private bool character; // Use this to determine if it's a character
    [SerializeField] private bool moveSideways; // Control horizontal movement
    [SerializeField] private bool moveUpDown;   // Control vertical movement

    private bool movingLeft;
    private bool movingUp;

    private float leftEdge;
    private float rightEdge;
    private float topEdge;
    private float bottomEdge;

    private void Awake()
    {
        // Calculate the edges based on the initial position
        leftEdge = transform.position.x - movementDistance;
        rightEdge = transform.position.x + movementDistance;
        topEdge = transform.position.y + movementDistance;
        bottomEdge = transform.position.y - movementDistance;

        // Initialize directions
        movingLeft = true; // Start by moving left
        movingUp = true;   // Start by moving up
    }

    private void Update()
    {
        Move();
    }

    private void Move()
    {
        // Check for sideways movement
        if (moveSideways)
        {
            // Calculate the new horizontal position
            float newPositionX = transform.position.x + (movingLeft ? -speed * Time.deltaTime : speed * Time.deltaTime);
            transform.position = new Vector3(newPositionX, transform.position.y, transform.position.z);

            // Check horizontal movement edges
            if (movingLeft && transform.position.x <= leftEdge)
            {
                movingLeft = false; // Change direction to right
                FlipCharacter(); // Flip sprite to face right
            }
            else if (!movingLeft && transform.position.x >= rightEdge)
            {
                movingLeft = true; // Change direction to left
                FlipCharacter(); // Flip sprite to face left
            }
        }

        // Check for up and down movement
        if (moveUpDown)
        {
            // Calculate the new vertical position
            float newPositionY = transform.position.y + (movingUp ? speed * Time.deltaTime : -speed * Time.deltaTime);
            transform.position = new Vector3(transform.position.x, newPositionY, transform.position.z);

            // Check vertical movement edges
            if (movingUp && transform.position.y >= topEdge)
            {
                movingUp = false; // Change direction to down
            }
            else if (!movingUp && transform.position.y <= bottomEdge)
            {
                movingUp = true; // Change direction to up
            }
        }
    }

    private void FlipCharacter()
    {
        if (character)
        {
            // Flip the sprite based on horizontal movement direction
            float newScaleX = movingLeft ? -Mathf.Abs(transform.localScale.x) : Mathf.Abs(transform.localScale.x);
            transform.localScale = new Vector3(newScaleX, transform.localScale.y, transform.localScale.z);
        }
    }
}
