using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FroggerGameSession : MonoBehaviour
{
    //This class manages the game session, including player lives
    [SerializeField] int playerLives = 3;

    [SerializeField] int keys = 0;
    //Reference to the UI text component that displays the player's lives
    [SerializeField] Text livesText;

    [SerializeField] Text keysText;

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

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Start()
    {
        ResetValues();
        UpdateUI();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        GameObject livesObj = GameObject.Find("LivesText");
        GameObject keysObj = GameObject.Find("KeysText");

        if (livesObj != null)
            livesText = livesObj.GetComponent<Text>();

        if (keysObj != null)
            keysText = keysObj.GetComponent<Text>();

        UpdateUI();
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
            SceneManager.LoadScene(5);
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

    public void AddKey()
    {
        keys++;
        keysText.text = keys.ToString();
    }

    //This method ensures that the player's lives do not exceed the maximum limit (3 in this case)
    public void MaxLives()
    {
        if (playerLives > 3)
        {
            playerLives = 3;
        }
    }


    public void MaxKeys()
    {
        if (keys > 6)
        {
            keys = 6;
        }
    }

    public void ResetValues()
    {
        playerLives = 3;
        keys = 0;
    }

    private void UpdateUI()
    {
        if (livesText != null)
            livesText.text = playerLives.ToString();

        if (keysText != null)
            keysText.text = keys.ToString();
    }

    public void ResetGameSession()
    {
        ResetValues();
        SceneManager.LoadScene(0);
    }

    public void NextLevel()
    {
        //Load the next scene in the build index
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
