using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        FindAnyObjectByType<FroggerGameSession>().AddPlayerLives(); // Add a life to the player when they collide with the heart pickup
        Destroy(gameObject); // Destroy the heart pickup when the player collides with it
    }
}

