using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class _1942_GameRefereeController : MonoBehaviour
{
    public int score;
    public int lives;
    public int playerShotCount;
    public TextMeshProUGUI scoreBoard;
    public TextMeshProUGUI lifeBoard;
    public TextMeshProUGUI extraLifeBoard;
    public TextMeshProUGUI intensityBoard;
    public List<_1942_EnemySpawnerController> spawners;
    public float spawnTimer = 0;
    public float spawnDelay = 3;
    public int nextLife = 10000;
    public int intensity = 1;

    // Start is called before the first frame update
    void Start()
    {
        foreach (_1942_EnemySpawnerController obj in FindObjectsOfType<_1942_EnemySpawnerController>())
        {
            spawners.Add(obj);
        }
    }

    // Update is called once per frame
    void Update()
    {
        spawnTimer += Time.deltaTime;
        if(spawnTimer > 3)
        {
            for(int i = 0; i < (Convert.ToInt32(score/30000)+1); i++)
            {
                _1942_EnemySpawnerController spawner = spawners[Random.Range(0, spawners.Count)];
                StartCoroutine(spawner.spawnFormation);
                spawnTimer = 0;
            }
        }
        
        playerShotCount = FindObjectsOfType<_1942_BulletController>().Length;
        scoreBoard.text = score.ToString();
        if (score > nextLife)
        {
            lives ++;
            nextLife *= 2;
            spawnDelay *= .9f;
            intensity++;
        }
        lifeBoard.text = lives.ToString();
        extraLifeBoard.text = nextLife.ToString();
        string intense = intensity.ToString();
        for (int i = 0; i < (Convert.ToInt32(score / 30000)); i++)
        {
            intense = intense + "*";
        }
        intensityBoard.text = intense;
    }
}
