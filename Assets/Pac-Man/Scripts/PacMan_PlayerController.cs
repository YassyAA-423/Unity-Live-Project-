using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PacMan_PlayerController : MonoBehaviour
{
    PacMan_MovementController movementController;

    public PacMan_GameManager gameManager;

    public SpriteRenderer sprite;
    public Animator animator;

    void Awake()
    {
        
        gameManager = GameObject.Find("Game Manager").GetComponent<PacMan_GameManager>();

        animator = GetComponentInChildren<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        animator.SetBool("dead", false);
        
        movementController = GetComponent<PacMan_MovementController>();
        movementController.lastMovingDirection = "left";
    }

    void Update()
    {
        // Sets moving bool pararamter in Animator to true and activates Pac-Man animations
        animator.SetBool ("moving", true);

        if (Input.GetKey(KeyCode.A))
        {
            movementController.SetDirection("left");
        }

        if (Input.GetKey(KeyCode.D))
        {
            movementController.SetDirection("right");
        }

        if (Input.GetKey(KeyCode.W))
        {
            movementController.SetDirection("up");
        }

        if (Input.GetKey(KeyCode.S))
        {
            movementController.SetDirection("down");
        }

        bool flipX = false;
        bool flipY = false;

        // These if/else if statements determine whether or not to flip the animation based on the direction parameter in the Animator
        if (movementController.lastMovingDirection == "left")
        {
            animator.SetInteger("direction", 0);
        }

        else if (movementController.lastMovingDirection == "right")
        {
            animator.SetInteger("direction", 0);
            flipX = true;
        }

        else if (movementController.lastMovingDirection == "up")
        {
            animator.SetInteger("direction", 1);
        }

        else if (movementController.lastMovingDirection == "down")
        {
            animator.SetInteger("direction", 1);
            flipY = true;
        }

        sprite.flipY = flipY;
        sprite.flipX = flipX;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Blinky" || collision.gameObject.name == "Pinky" || collision.gameObject.name == "Inky")
        {
            SceneManager.LoadScene("PacMan_EndScene");
        }
    }
}
