using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class FroggerEnemyMovement : MonoBehaviour
{
    public Vector2 moveDirection = Vector2.right; // Direction of movement
    public float moveSpeed = 1f; // Speed of movement
    public int size = 1; // Size of the enemy (number of segments)

    Vector3 leftEdge; // Left edge of the screen in world coordinates
    Vector3 rightEdge; // Right edge of the screen in world coordinates 
    // Start is called before the first frame update
    void Start()
    {
        leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, 0));
        rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0, 0));
    }

    // Update is called once per frame
    private void Update()
    {
        // Check if the cycle is moving right and has passed the right edge
        if (moveDirection.x > 0 && (transform.position.x - size) > rightEdge.x)
        {
            // Wrap around to the left edge
            Vector3 position = transform.position;
            position.x = leftEdge.x - size;
            transform.position = position;
        }
        // Check if the cycle is moving left and has passed the left edge
        else if (moveDirection.x < 0 && (transform.position.x + size) < leftEdge.x)
        {
            // Wrap around to the right edge
            Vector3 position = transform.position;
            position.x = rightEdge.x + size;
            transform.position = position;
        }
        // Move the cycle in the specified direction
        else
        {
            transform.Translate(moveDirection * moveSpeed * Time.deltaTime);
        }
    }
}



