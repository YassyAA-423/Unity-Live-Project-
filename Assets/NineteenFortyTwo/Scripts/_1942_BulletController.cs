using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class _1942_BulletController : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject hitEffect;

    public GameObject referee;
    public _1942_GameRefereeController refereeScript;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        if (rb == null) { rb = GetComponent<Rigidbody>(); }
        if (referee == null) { referee = GameObject.Find("Game Referee"); }
        if (refereeScript == null) { refereeScript = referee.GetComponent<_1942_GameRefereeController>(); }

        transform.parent = null;
        //transform.rotation = Quaternion.LookRotation(Vector3.up);
        rb.AddForce(Vector3.up * bulletSpeed, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //this method creates hit particles and decrements the shot count before dying
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
