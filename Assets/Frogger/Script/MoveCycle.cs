using UnityEngine;

public class MoveCycle : MonoBehaviour
{
    public Vector2 moveDirection = Vector2.right; // Direction of movement
    public float moveSpeed = 1f; // Speed of movement
    public int size = 1; // Size of the cycle (number of segments)

    private Vector3 leftEdge;// Left edge of the screen in world coordinates
    private Vector3 rightEdge; // Right edge of the screen in world coordinates

    private void Start()
    {
        // Calculate the left and right edges of the screen in world coordinates
        leftEdge = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0, 0));
        rightEdge = Camera.main.ViewportToWorldPoint(new Vector3(1f, 0, 0));
    }

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
        else if (moveDirection.x < 0 && (transform.position.x + size) <  leftEdge.x)
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
