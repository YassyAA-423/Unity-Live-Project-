using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPickup : MonoBehaviour
{
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
            // Destroy the key object after it has been collected
            Destroy(gameObject);
        }
    }
}
