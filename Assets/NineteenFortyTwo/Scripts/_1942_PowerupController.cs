using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _1942_PowerupController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.parent = null;
        transform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, -1f*Time.deltaTime, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
