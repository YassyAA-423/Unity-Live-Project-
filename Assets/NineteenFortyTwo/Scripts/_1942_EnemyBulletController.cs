using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class _1942_EnemyBulletController : MonoBehaviour
{
    public float bulletSpeed;
    public GameObject hitEffect;
    public GameObject player;
    public Vector3 dest;

    public GameObject referee;
    public _1942_GameRefereeController refereeScript;

    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        if (rb == null) { rb = GetComponent<Rigidbody>(); }
        if (player == null) { player = FindObjectOfType<_1942_PlayerController>().gameObject; }
        if (referee == null) { referee = GameObject.Find("Game Referee"); }
        if (refereeScript == null) { refereeScript = referee.GetComponent<_1942_GameRefereeController>(); }

        if (FindObjectsOfType<_1942_PlayerController>().Length < 1) { Destroy(gameObject); return; }
        transform.LookAt(player.transform);
        transform.Rotate(Vector3.right, 90);
        

        rb.AddForce(((player.transform.position - transform.position).normalized) * bulletSpeed, ForceMode.VelocityChange);
    }

    // Update is called once per frame
    void Update()
    {
        if (FindObjectsOfType<_1942_PlayerController>().Length < 1) { Destroy(gameObject); return; }
    }

    //this method creates hit particles and decrements the shot count before dying
    private void OnCollisionEnter(Collision collision)
    {
        Instantiate(hitEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }
}
