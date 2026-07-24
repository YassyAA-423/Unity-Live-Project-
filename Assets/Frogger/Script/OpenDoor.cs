using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public int keysRequired = 3;
    BoxCollider2D doorCollider;
    Animator doorAnimator;
    [SerializeField] AudioClip doorOpenSound; // Sound to play when the door opens

    void OnCollisionEnter2D(Collision2D collision)
    { 
        if (collision.gameObject.CompareTag("Player"))
        {
            Frogger_Inventory inventory = collision.gameObject.GetComponent<Frogger_Inventory>();
            if (inventory != null && inventory.keysCollected >= keysRequired)
            {
                openTheDoor();
            }
             else
            {
                Debug.Log("You need " + keysRequired + " keys to open this door.");
            }
        }
    }

    private void Awake()
    {
        doorCollider = GetComponent<BoxCollider2D>();
        doorAnimator = GetComponent<Animator>();
    }
    // This method will handle the logic for opening the door, such as disabling the collider and playing the opening animation.
    public void openTheDoor()
    {
        doorCollider.enabled = false; // Disable the collider to allow the player to pass through
        doorAnimator.SetTrigger("Opening"); // Trigger the opening animation
        AudioSource.PlayClipAtPoint(doorOpenSound, Camera.main.transform.position); // Play the door opening sound at the camera's position
        Debug.Log("Door opened!");
    }
}


