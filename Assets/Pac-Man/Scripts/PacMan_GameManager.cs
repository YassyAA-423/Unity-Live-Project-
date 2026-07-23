using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PacMan_GameManager : MonoBehaviour
{
    public GameObject pacman;

    public GameObject leftWarpNode;
    public GameObject rightWarpNode;

    public AudioSource siren;
    public AudioSource munch1;
    public AudioSource munch2;

    public int currentMunch = 0;

    public int score;
    public TextMeshProUGUI scoreText;

    public GameObject ghostNodeLeft;
    public GameObject ghostNodeRight;
    public GameObject ghostNodeCenter;
    public GameObject ghostNodeStart;

    public GameObject Blinky;
    public GameObject Pinky;
    public GameObject Inky;

    public SpriteRenderer sprite;
    public Animator animator;

    public int totalPellets;
    public int pelletsLeft;
    public int pelletsCollectedOnCurrentLife;

    public bool gameIsRunning;
    public bool hadDeathOnThisLevel = false;
    public bool clearedLevel;

    void Awake()
    {
        score = 0;
        StartGame();
    }

    void StartGame()
    {
        gameIsRunning = true;
        siren.Play();
    }

    public void GotPelletFromNode()
    {
        totalPellets++;
        pelletsLeft++;
    }

    public void AddToScore (int amount)
    {
        score += amount;
        scoreText.text = score.ToString();
    }

    public void CollectedPellet (PacMan_NodeController nodeController)
    {
        if (currentMunch == 0)
        {
            munch1.Play();
        }

        else if (currentMunch == 1)
        {
            munch2.Play();
            currentMunch = 0;
        }

        pelletsLeft--;
        pelletsCollectedOnCurrentLife++;

        int requiredPinkPellets = 0;
        int requiredBluePellets = 0;

        if (hadDeathOnThisLevel)
        {
            requiredPinkPellets = 12;
            requiredBluePellets = 32;
        }

        else
        {
            requiredPinkPellets = 50;
            requiredBluePellets = 100;
        }

        if (pelletsCollectedOnCurrentLife >= requiredPinkPellets && !Pinky.GetComponent<PacMan_EnemyMovementController>().leftHomeBefore)
        {
            Pinky.GetComponent<PacMan_EnemyMovementController>().readyToLeaveHome = true;
        }

        if (pelletsCollectedOnCurrentLife >= requiredBluePellets && !Inky.GetComponent<PacMan_EnemyMovementController>().leftHomeBefore)
        {
            Inky.GetComponent<PacMan_EnemyMovementController>().readyToLeaveHome = true;
        }

        AddToScore(10);

        // Checks to see if there are any pellets left
        if (pelletsLeft == 0)
        {
            clearedLevel = true;

            SceneManager.LoadScene("PacMan_TitleScreen");
        }
    }
}
