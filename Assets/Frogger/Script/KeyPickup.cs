using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
    [SerializeField] AudioClip pickupSound; // Sound to play when the key is collected
    public void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the object that entered the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Get the Frogger_Inventory component from the player
            Frogger_Inventory inventory = other.GetComponent<Frogger_Inventory>();
            if (inventory != null)
            {
                // Increment the keysCollected variable in the player's inventory
                inventory.keysCollected++;
            }
            // Play the pickup sound at the position of the key
            AudioSource.PlayClipAtPoint(pickupSound, Camera.main.transform.position);

            // Add a key to the player's inventory when they collide with the key pickup
            FindAnyObjectByType<FroggerGameSession>().AddKey();
            FindAnyObjectByType<FroggerGameSession>().MaxKeys();
            // Destroy the key object after it has been collected
            Destroy(gameObject);
        }
    }
}
