using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _1942_OceanController : MonoBehaviour
{
    public float scrollSpeed;
    public GameObject oceanTile;
    private bool spawned = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, 0, scrollSpeed*Time.deltaTime);
        if (transform.position.y <= 0 && !spawned)
        {
            Instantiate(oceanTile, new Vector3(0,transform.position.x + 49.8f, 4), transform.rotation);
            spawned = true;
        }
        if (transform.position.y <= -50) 
        {
            Destroy(gameObject);
        }
    }
}
