using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FroggerGameSession : MonoBehaviour
{
    //This class manages the game session, including player lives
    [SerializeField] int playerLives = 3;
    //Reference to the UI text component that displays the player's lives
    [SerializeField] Text livesText;
    private int currentLives;

    private void Awake()
    {
        //Check if there are multiple instances of the game session in the scene
        int numGameSessions = FindObjectsOfType<FroggerGameSession>().Length;

        if (numGameSessions > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }


    private void Start()
    {
        //Initialize the lives text
        livesText.text = playerLives.ToString();
    }
    //This method is called when the player dies, and it checks if the player has remaining lives
    public void ProcessPlayerDeath()
    {
        if (playerLives > 1)
        {
            TakeLife();
        }
        else
        {
            //If the player has no more lives, reset the game session and load the first scene
            ResetGameSession();
            SceneManager.LoadScene(4);
        }
    }

    //This method reduces the player's lives by one and updates the UI text
    private void TakeLife()
    {
        playerLives--;
        livesText.text = playerLives.ToString();

    }
    //This method adds a life to the player and updates the UI text
    public void AddPlayerLives()
    {
        playerLives++;
        MaxLives();
        livesText.text = playerLives.ToString();
    }


    //This method ensures that the player's lives do not exceed the maximum limit (3 in this case)
    public void MaxLives()
    {
        if (playerLives > 3)
        {
            playerLives = 3;
        }
    }

    public void ResetGameSession()
    {
        //Reset the player's lives to the initial value and load the first scene
        SceneManager.LoadScene(0);
        //this will destroy the game session object, allowing a new one to be created when the first scene is loaded
        Destroy(gameObject);
    }

    public void NextLevel()
    {
        //Load the next scene in the build index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    }
