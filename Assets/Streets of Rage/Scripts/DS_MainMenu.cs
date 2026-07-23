using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

public class DS_MainMenu : MonoBehaviour
{
	public void LoadMainMenu()
	{
		SceneManager.LoadScene("DS_MainMenu");
	}

	public void LoadDemoLevel()
	{
		SceneManager.LoadScene("DS_Stage1");
	}

	public void OnCollisionEnter2D(Collision2D collision)
	{
		SceneManager.LoadScene("DS_GameOver");
	}

	public void ExitGame()
	{
        SceneManager.LoadScene("MainMenu");
    }
}
