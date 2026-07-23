using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile_Building : MonoBehaviour
{
    public GameObject spawn;
    public GameObject civ;
    public float timer, threshold;
    public int pop,popMax;
    // Start is called before the first frame update
    void Start()
    {
        threshold = Random.Range(threshold / 2, threshold * 1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > threshold && pop < popMax)
        {
            timer = 0;
            Instantiate(civ,spawn.transform.position, civ.transform.rotation);
            pop++;
        }
    }
}
