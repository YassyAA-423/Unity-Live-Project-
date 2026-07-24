using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    //This method will start the game
    public void playGame()
    {
        SceneManager.LoadScene(1);
    }


    //This method will restart the game
    public void restartGame()
    {
        // Reloads the currently active scene
        SceneManager.LoadScene(0);
        FindAnyObjectByType<FroggerGameSession>().ResetGameSession();

    }


    //This method will quit the game
    public void quitGame()
    {
        Debug.Log("You have quit the game");
        Application.Quit();
    }

}
