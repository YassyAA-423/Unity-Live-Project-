using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _1942_TrackBackgroundScript : MonoBehaviour
{
    public float movement;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(0, movement * Time.deltaTime, 0), Space.World);
        transform.position.Set(transform.position.x, transform.position.y, 10);
    }
}
