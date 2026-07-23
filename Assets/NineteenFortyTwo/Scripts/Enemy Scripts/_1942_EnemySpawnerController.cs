using System;
using System.CodeDom.Compiler;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using Random = UnityEngine.Random;

public class _1942_EnemySpawnerController : MonoBehaviour
{
    public float spawnInterval;
    public float maneuverPoint;
    public Vector3 spawnDirection;
    public GameObject enemy;
    public int minWaveSize;
    public int maxWaveSize;
    public bool active = false;
    public float spawnTimer = 0;
    public IEnumerator spawnFormation;

    // Start is called before the first frame update
    void Start()
    {
        spawnFormation = SpawnFormation(Random.Range(minWaveSize, maxWaveSize));
    }

    // Update is called once per frame
    void Update()
    {
        //whenever the spawn interval passes, initiate a spawning wave
        if (active)
        {
            spawnTimer += Time.deltaTime;
            if (spawnTimer >= spawnInterval)
            {
                spawnTimer = 0;
                StartCoroutine(spawnFormation);
            }
        }
    }

    public GameObject SpawnEnemy(Vector3 spawnPosition)
    {
        GameObject spawn = Instantiate(enemy, spawnPosition, Quaternion.identity);
        return spawn;
    }

    //this coroutine creates a swarm of enemies with the same trajectory
    //then it activates them in sequence
    public IEnumerator SpawnFormation(int number)
    {
        //create a line of enemies at a random location along this object's y position
        Vector3 spawnPosition = new Vector3(Random.Range(-6f, 6f), transform.position.y, 0);
        GameObject[] formation = new GameObject[number];
        for(int i=0; i < number; i++)
        {
            formation[i] = SpawnEnemy(spawnPosition);
            spawnPosition.x += enemy.transform.localScale.x;
        }

        //generate a trajectory for the formation based on the spawn direction
        Vector3 movePath = spawnDirection;
        movePath.x = Random.Range(-.5f, .5f);
        movePath = movePath.normalized;
        //apply the trajectory to each enemy in the formation
        foreach(GameObject enemy in formation)
        {
            //this coroutine delays each enemy by a set amount to create a nice formation
            IEnumerator activate = ActivateEnemy(enemy, movePath, maneuverPoint);
            yield return StartCoroutine(activate);
        }
        //finally, reload the spawner with a fresh coroutine
        spawnFormation = SpawnFormation(Random.Range(minWaveSize,maxWaveSize));
    }

    //this coroutine simply waits for a set period then feeds the input enemy with a movement vector
    public IEnumerator ActivateEnemy(GameObject enemy, Vector3 movePath, float maneuverPoint)
    {
        yield return new WaitForSeconds(.3f);
        _1942_EnemyController enemyController = enemy.GetComponent<_1942_EnemyController>();
        enemyController.movePath = movePath;
        enemyController.maneuverPoint = maneuverPoint;
    }
}
