using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PacMan_NodeDeleter : MonoBehaviour
{
    // This function destroys all the Nodes that collide with the box colliders placed around the obstacles and boundaries of the map
    // The result is a path of Nodes for Pac-Man to follow
    // Script won't be needed after map is set up but I'm leaving it in the project in case I have time to create more maps

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.tag == "Destroyable")
    //    {
    //        Destroy(collision.gameObject);
    //    }
    //}
}
