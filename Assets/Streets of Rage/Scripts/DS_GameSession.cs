using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DS_GameSession : MonoBehaviour
{
	[SerializeField] int playerHealth = 4, score = 0;
	[SerializeField] Text scoreText;
	[SerializeField] Image[] health;

	private void Awake()
	{
		int numGameSessions = FindObjectsOfType<DS_GameSession>().Length;

		if (numGameSessions > 1)
		{
			Destroy(gameObject);
		}
		else
		{
			DontDestroyOnLoad(gameObject);
		}
	}

	public void Start()
	{
		scoreText.text = score.ToString();
		UpdateHealth();
	}

	public void AddToScore(int value)
	{
		score += value;
		scoreText.text = score.ToString();
	}

	public void AddToHealth(int value)
	{
		playerHealth += value;

		if (playerHealth >= 4)
		{
			playerHealth = 4;
		}

		UpdateHealth();
	}

	public int GetPlayerHealth()
	{
		return playerHealth;
	}

	private void TakeLife()
	{
		playerHealth--;
		UpdateHealth();
	}

	public void UpdateHealth()
	{
		for (int i = 0; i < health.Length; i++)
		{
			if (i < playerHealth)
			{
				health[i].enabled = true;
			}
			else
			{
				health[i].enabled = false;
			}
		}
	}

	public void ProcessPlayerDeath()
	{
		if (playerHealth > 1)
		{
			TakeLife();
		}
		else
		{
			StartCoroutine(FindObjectOfType<DS_Player>().Death());
		}
	}

	public void GameOver()
	{
		SceneManager.LoadScene("DS_GameOver");
		Destroy(gameObject);
	}
}
