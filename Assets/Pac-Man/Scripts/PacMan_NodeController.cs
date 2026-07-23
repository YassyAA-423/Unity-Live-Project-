using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMan_NodeController : MonoBehaviour
{
    // Bool variables that indicate what direction the player can move
    public bool canMoveLeft = false;
    public bool canMoveRight = false;
    public bool canMoveUp = false;
    public bool canMoveDown = false;

    public GameObject leftNode;
    public GameObject rightNode;
    public GameObject upNode;
    public GameObject downNode;

    // Bool variables indicating where the Warp Nodes are located
    public bool isWarpRightNode = false;
    public bool isWarpLeftNode = false;

    // Determines if the Node has a pellet when the game starts
    public bool isPelletNode = false;
    // Determines if the Node still has a pellet
    public bool hasPellet = false;

    public SpriteRenderer pelletSprite;

    public PacMan_GameManager gameManager;

    void Awake()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<PacMan_GameManager>();

        // Determines which Nodes on the game board have a pellet
        if (transform.childCount > 0)
        {
            gameManager.GotPelletFromNode();
            hasPellet = true;
            isPelletNode = true;
            pelletSprite = GetComponentInChildren<SpriteRenderer>();
        }

        RaycastHit2D[] hitDown;
        // Shoots a line downawards to detect any Node below
        hitDown = Physics2D.RaycastAll(transform.position, -Vector2.up, Mathf.Infinity, LayerMask.GetMask("Environment"));

        // Creates a for loop that cycles through all of the gameObjects that the line hits
        for (int i = 0; i < hitDown.Length; i++)
        {
            float distance = Mathf.Abs(hitDown[i].point.y - transform.position.y);

            if (distance < 0.28f)
            {
                canMoveDown = true;
                downNode = hitDown[i].collider.gameObject;
            }
        }

        RaycastHit2D[] hitUp;
        // Shoots a line upwards to detect any Node above
        hitUp = Physics2D.RaycastAll(transform.position, Vector2.up, Mathf.Infinity, LayerMask.GetMask("Environment"));

        // Creates a for loop that cycles through all of the gameObjects that the line hits
        for (int i = 0; i < hitUp.Length; i++)
        {
            float distance = Mathf.Abs(hitUp[i].point.y - transform.position.y);

            if (distance < 0.28f)
            {
                canMoveUp = true;
                upNode = hitUp[i].collider.gameObject;
            }
        }

        RaycastHit2D[] hitRight;
        // Shoots a line to the right to detect any Node to the right
        hitRight = Physics2D.RaycastAll(transform.position, Vector2.right, Mathf.Infinity, LayerMask.GetMask("Environment"));

        // Creates a for loop that cycles through all of the gameObjects that the line hits
        for (int i = 0; i < hitRight.Length; i++)
        {
            float distance = Mathf.Abs(hitRight[i].point.x - transform.position.x);

            if (distance < 0.28f)
            {
                canMoveRight = true;
                rightNode = hitRight[i].collider.gameObject;
            }
        }

        RaycastHit2D[] hitLeft;
        // Shoots a line to the left to detect any Node to the left
        hitLeft = Physics2D.RaycastAll(transform.position, -Vector2.right, Mathf.Infinity, LayerMask.GetMask("Environment"));

        // Creates a for loop that cycles through all of the gameObjects that the line hits
        for (int i = 0; i < hitLeft.Length; i++)
        {
            float distance = Mathf.Abs(hitLeft[i].point.x - transform.position.x);

            if (distance < 0.28f)
            {
                canMoveLeft = true;
                leftNode = hitLeft[i].collider.gameObject;
            }
        }
    }

    // Determines the directions that can be moved from one Node to another
    // Also determines if there are no Nodes to move towards
    public GameObject GetNodeDirection(string direction)
    {
        if (direction == "left" && canMoveLeft)
        {
            return leftNode;
        }

        else if (direction == "right" && canMoveRight)
        {
            return rightNode;
        }

        else if (direction == "up" && canMoveUp)
        {
            return upNode;
        }

        else if (direction == "down" && canMoveDown)
        {
            return downNode;
        }

        else
        {
            return null;
        }
    }

    // Makes Pac-Man "eat" the pellets
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && hasPellet)
        {
            hasPellet = false;
            pelletSprite.enabled = false;
            gameManager.CollectedPellet(this);
        }
    }
}
