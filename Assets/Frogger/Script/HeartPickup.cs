using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    [SerializeField] AudioClip pickupHeart; // Sound to play when the heart is collected
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player") // Check if the player collides with the heart pickup
        {
            AudioSource.PlayClipAtPoint(pickupHeart, Camera.main.transform.position); // Play the pickup sound at the camera's position
            FindAnyObjectByType<FroggerGameSession>().AddPlayerLives(); // Add a life to the player when they collide with the heart pickup
        Destroy(gameObject); // Destroy the heart pickup when the player collides with it
        }
    }
}

