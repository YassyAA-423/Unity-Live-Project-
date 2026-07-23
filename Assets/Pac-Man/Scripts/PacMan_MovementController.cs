using UnityEngine;

public class PacMan_MovementController : MonoBehaviour
{
    public PacMan_GameManager gameManager;

    public GameObject currentNode;
    public float speed = 4f;

    public string direction = "";
    public string lastMovingDirection = "";

    public bool canWarp = true;

    public bool isGhost = false;

    void Awake()
    {
        lastMovingDirection = "left";
        gameManager = GameObject.Find("Game Manager").GetComponent<PacMan_GameManager>();
    }

    void Update()
    {
        // References the PacMan_NodeController script attached to the Node gameObject in order to identify the directions it is possible to move
        PacMan_NodeController currentNodeController = currentNode.GetComponent<PacMan_NodeController>();

        transform.position = Vector2.MoveTowards(transform.position, currentNode.transform.position, speed * Time.deltaTime);

        // Accounts for the slight stuttering between movement transition if the player hits direction keys back and forth quickly
        bool reverseDirection = false;
        if (
            (direction == "left" && lastMovingDirection == "right")
            || (direction == "right" && lastMovingDirection == "left")
            || (direction == "up" && lastMovingDirection == "down")
            || (direction == "down" && lastMovingDirection == "up")
            )
        {
            reverseDirection = true;
        }

        // Determines whether the player is at the center of the current Node
        if ((transform.position.x == currentNode.transform.position.x && transform.position.y == currentNode.transform.position.y) || reverseDirection)
        {

            // If the player reaches the center of the Left Warp Node, warp to the Right Warp Node
            if (currentNodeController.isWarpLeftNode && canWarp)
            {
                currentNode = gameManager.rightWarpNode;
                direction = "left";
                lastMovingDirection = "left";
                transform.position = currentNode.transform.position;
                canWarp = false;
            }
            // If the player reaches the center of the Right Warp Node, warp to the Left Warp Node
            else if (currentNodeController.isWarpRightNode && canWarp)
            {
                currentNode = gameManager.leftWarpNode;
                direction = "right";
                lastMovingDirection = "right";
                transform.position = currentNode.transform.position;
                canWarp = false;
            }
            // Otherwise, find the next Node the player can move towards
            else
            {
                DetermineNextNode();
            }
        }
        // Player is not in the center of a node
        else
        {
            canWarp = true;
        }
    }

    virtual public void DetermineNextNode()
    {
        // References the PacMan_NodeController script attached to the Node gameObject in order to identify the directions it is possible to move
        PacMan_NodeController currentNodeController = currentNode.GetComponent<PacMan_NodeController>();

        // Gets the next Node from the Node Controller script function GetNodeDirection using the current player direction
        GameObject newNode = currentNodeController.GetNodeDirection(direction);

        // If it is possible to move in the desired direction
        if (newNode != null)
        {
            currentNode = newNode;
            lastMovingDirection = direction;
        }

        // If it is not possible to move in the desired direction attempt to keep going in the last moving direction
        else
        {
            direction = lastMovingDirection;
            newNode = currentNodeController.GetNodeDirection(direction);

            if (newNode != null)
            {
                currentNode = newNode;
            }
        }
    }

    public void SetDirection(string newDirection)
    {
        direction = newDirection;
    }
}
