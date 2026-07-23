using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMan_NodeSpawner : MonoBehaviour
{
    // This is a script I needed to run once on transform.position.x and transform.position.y (+ currentSpawnOffset) to create clones of the Node gameObject
    // This kept me from having to individually place each Node by hand
    // Script won't be needed after map is set up but I'm leaving it in the project in case I have time to create more maps


    // Variable controlling the number of cloned Nodes to spwan
    //int numSpawn = 29;
    // Variable that takes the current position of a cloned Node
    //float currentSpawnOffset;
    // Variable controlling the amount of space between each cloned Node
    //public float spawnOffset = 0.3f;

    //void Awake()
    //{
    //    if (gameObject.name == "Node")
    //    {
    //        // For loop that instantiates a clone of the Node gameObject for the value of the numSpawn variable and sets the distance between them
    //        for (int i = 0; i < numSpawn; i++)
    //        {
    //            GameObject clone = Instantiate(gameObject, new Vector3(transform.position.x, transform.position.y + currentSpawnOffset, 0), Quaternion.identity);
    //            currentSpawnOffset += spawnOffset;
    //        }
    //    }
    //}
}
