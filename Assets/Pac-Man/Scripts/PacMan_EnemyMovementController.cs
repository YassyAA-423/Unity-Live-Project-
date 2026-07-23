using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMan_EnemyMovementController : PacMan_MovementController
{
    public enum GhostNames
    {
        blinky,
        inky,
        pinky,
        clyde
    }

    public GhostNames ghostName;

    public GameObject pacman;

    public GameObject ghostNodeLeft;
    public GameObject ghostNodeRight;
    public GameObject ghostNodeCenter;
    public GameObject ghostNodeStart;

    public PacMan_MovementController movementController;

    public GameObject startingNode;

    public bool readyToLeaveHome = false;
    public bool inGameSpace = false;
    public bool leftHomeBefore = false;
    public bool isVisible = true;

    public SpriteRenderer ghostSprite;

    void Awake()
    {
        //ghostSprite = GetComponent<SpriteRenderer>();
        
        gameManager = GameObject.Find("Game Manager").GetComponent<PacMan_GameManager>();
        movementController = this;

        if (ghostName == GhostNames.blinky)
        {
            startingNode = ghostNodeStart;
            inGameSpace = true;
        }

        else if (ghostName == GhostNames.inky)
        {
            startingNode = ghostNodeLeft;
            
        }

        else if (ghostName == GhostNames.pinky)
        {
            startingNode = ghostNodeCenter;
        }

        else if (ghostName == GhostNames.clyde)
        {
            startingNode = ghostNodeRight;

        }

        transform.position = startingNode.transform.position;
        movementController.currentNode = startingNode;
    }

    public override void DetermineNextNode() 
    {
        // References the PacMan_NodeController script attached to the Node gameObject in order to identify the directions it is possible to move
        PacMan_NodeController currentNodeController = currentNode.GetComponent<PacMan_NodeController>();
        GameObject newNode = null;

        if (!inGameSpace) 
        {
            if (!readyToLeaveHome) 
            {
                // Continue along current direction while nodes are available
                newNode = currentNodeController.GetNodeDirection(direction);

                // If no nodes in direction, reverse direction
                if (newNode == null) 
                {
                    if (direction == "right") 
                    {
                        direction = "left";
                    } 
                    
                    else 
                    {
                        direction = "right";
                    }
                }

                newNode = currentNodeController.GetNodeDirection(direction);
                currentNode = newNode;
                lastMovingDirection = direction;
            } 

            // We are ready to leave home and move into the game space
            else 
            {
                // Start moving towards the center node if not on the center node
                if (currentNode != ghostNodeCenter)
                {
                    newNode = ghostNodeCenter;
                }

                // If we *are* on the center node, move to start node
                // If we *are* on the start node, set inGameSpace = true, thus ignoring this logic for determining next node
                else
                {
                    newNode = ghostNodeStart;
                    inGameSpace = true;
                }
            }
        }

        // If we *are* in the game space, seek toward the player
        // Check each node direction (up/down/left/right) to determine if we can
        // a) move in that direction
        // and b) whether its a shorter distance to the player
        else
        {
            if(transform.position.x != currentNode.transform.position.x || transform.position.y != currentNode.transform.position.y) return;
            float shortestDistance = Mathf.Infinity;

            if (currentNodeController.canMoveUp)
            {
                float upDistance = Vector2.Distance(gameManager.pacman.transform.position, currentNodeController.GetNodeDirection("up").transform.position);

                if (upDistance < shortestDistance)
                {
                    shortestDistance = upDistance;
                    direction = "up";
                }
            }

            if (currentNodeController.canMoveDown)
            {
                float downDistance = Vector2.Distance(gameManager.pacman.transform.position, currentNodeController.GetNodeDirection("down").transform.position);

                if (downDistance < shortestDistance)
                {
                    shortestDistance = downDistance;
                    direction = "down";
                }
            }

            if (currentNodeController.canMoveLeft)
            {
                float leftDistance = Vector2.Distance(gameManager.pacman.transform.position, currentNodeController.GetNodeDirection("left").transform.position);

                if (leftDistance < shortestDistance)
                {
                    shortestDistance = leftDistance;
                    direction = "left";
                }
            }

            if (currentNodeController.canMoveRight)
            {
                float rightDistance = Vector2.Distance(gameManager.pacman.transform.position, currentNodeController.GetNodeDirection("right").transform.position);

                if (rightDistance < shortestDistance)
                {
                    shortestDistance = rightDistance;
                    direction = "right";
                }
            }

            newNode = currentNodeController.GetNodeDirection(direction);

        }

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
}
