using System.Collections;
using UnityEngine;

public class Frogger_Player : MonoBehaviour
{
    //This variable will hold a reference to the player's Rigidbody2D component, which is used for physics-based movement and collision detection.
    Rigidbody2D playerRigidBody2D;

    //This variable will store the initial position of the player, which will be used as the spawn point for respawning after taking damage.
    private Vector3 spawnPosition;

    Animator Froggeranimator;

    AudioSource PlayerAudioSource;

    [SerializeField] AudioClip damageSoundSFX, jumpSoundSFX; // Sound to play when the player takes damage

    private void Start()
    {
        //Get the Rigidbody2D component attached to the player game object and store it in the playerRigidBody2D variable for later use.
        playerRigidBody2D = GetComponent<Rigidbody2D>();
        Froggeranimator = GetComponent<Animator>();
        PlayerAudioSource = GetComponent<AudioSource>();
    }

    private void Awake()
    {
        spawnPosition = transform.position; // Store the initial position as the spawn point
    }

    private void Update()
    {
        playerMovement();

        ChangingtoJumping();
    }

    private void ChangingtoJumping()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        Vector3 movement = new Vector3(horizontalInput, verticalInput, 0f);
        bool isMoving = movement.magnitude > 0;
        Froggeranimator.SetBool("Jumping", isMoving);
    }

    private void playerMovement()
    {
        //This if statement will check if the player has pressed the up arrow key or W key, and if so, the player sprite will move up/vertical.
        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Vector3.up);
        }
        //This else if statement will check if the player has pressed the down arrow key or S key, and if so, the player sprite will move down/vertical.
        else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector3.down);
        }
        //This else if statement will check if the play has pressed the left arrow key or A key, and if so, the player sprite will move left/horizontal.
        else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f); // Rotate the player sprite to face left
            Move(Vector3.left);
        }
        //This else if statement will check if the player has pressed the right arrow key or D key, and if so, the player sprite will move right/horizontal.
        else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f); // Rotate the player sprite to face right
            Move(Vector3.right);
        }
    }

    //This method will handle the logic for moving the player in a specified direction, checking for barriers, platforms, enemies, and water hazards at the destination point before allowing the movement.
    private void Move(Vector3 direction)
    {
        // Compute the intended destination
        Vector3 destination = transform.position + direction;
        // Check for barriers at the destination point
        Collider2D platform = Physics2D.OverlapBox(destination, Vector2.one * 0.1f, 0f, LayerMask.GetMask("Environment"));
        Collider2D enemy = Physics2D.OverlapBox(destination, Vector2.one * 0.3f, 0f, LayerMask.GetMask("Enemy"));
        Collider2D barrier = Physics2D.OverlapBox(destination, Vector2.one * 0.1f, 0f, LayerMask.GetMask("Barrier"));

        if (barrier != null)
        {
            OpenDoor door = barrier.GetComponent<OpenDoor>();

            if (door != null)
            {
                Frogger_Inventory inventory = GetComponent<Frogger_Inventory>();

                if (inventory != null && inventory.keysCollected >= door.keysRequired)
                {
                    door.openTheDoor(); // open it
                }
                else
                {
                    Debug.Log("Door is locked!");
                    return; // block movement
                }
            }
            else
            {
                return; // normal barrier
            }
        }

        if (platform != null)
        {
            transform.SetParent(platform.transform); // Move with the platform
        }
        else
        {
            transform.SetParent(null); // Detach from any platform
        }

        if (enemy != null)
        {
            playerDamage(); // Handle player damage if touching an enemy
        }
        //Will check if the player is touching the Abyss (aka Water layer), and if so, the player will take damage. However, if the player is on a platform, they will not take damage from the Abyss (water layer.)
        bool willBeOnPlatform = (platform != null);

        Collider2D water = Physics2D.OverlapBox(destination, Vector2.one * 0.1f, 0f, LayerMask.GetMask("Water"));

        if (water != null && !willBeOnPlatform)
        {
            playerDamage();
            return;
        }

        // No barrier, move to the destination
        transform.position = destination;

        if (PlayerAudioSource != null && jumpSoundSFX != null) 
        { 
            PlayerAudioSource.PlayOneShot(jumpSoundSFX); 
        } else {
            Debug.LogWarning("Missing AudioSource or jumpSoundSFX!"); 
        }
    }





    //This method will handle the logic for when the player takes damage, such as losing a life or respawning.
    private void playerDamage()
    {

        if (PlayerAudioSource != null && damageSoundSFX != null)
        {
            PlayerAudioSource.PlayOneShot(damageSoundSFX);
        }
        else
        {
            Debug.LogWarning("Missing AudioSource or damageSoundSFX!");
        }
        Debug.Log("Player hit by enemy!");
        Froggeranimator.SetTrigger("TakingDamage"); // Trigger the damage animation)
        FindAnyObjectByType<FroggerGameSession>().ProcessPlayerDeath(); // Process player death in the game session
        Invoke("Respawn", 0f); // Call the Respawn method after a delay of 0 seconds
    }

    //This method will handle the logic for respawning the player
    private void Respawn()
    {
        transform.rotation = Quaternion.identity;
        transform.position = spawnPosition; // Reset the player's position to the spawn point
        // Implement respawn logic here, such as resetting the player's position to a starting point
        enabled = true;
    }
}