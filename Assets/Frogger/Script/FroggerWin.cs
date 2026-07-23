using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FroggerWin : MonoBehaviour


{

    //This method will be called when another collider enter the trigger collider attached to the goal object, and it will check if the collider belongs to the player. If it does, it will call the WinGame method to load the next level.
    private void OnTriggerEnter2D(Collider2D other)
    {
        GetComponent<Collider2D>().enabled = false; // disable trigger
        Debug.Log("Level Complete! Player reached the goal.");
        if (other.CompareTag("Player"))
        {
            WinGame();
        }
    }
    //This method will be called when the player reaches the goal, and it will find the FroggerGameSession and call the NextLevel method to load the next level.
    private void WinGame()
    {
        var session = FindObjectOfType<FroggerGameSession>();
        if (session != null)
        {
            session.NextLevel();
        }
        else
        {
            Debug.LogWarning("FroggerGameSession not found.");
        }
    }
}


