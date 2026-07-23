using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile_Explosion : MonoBehaviour
{
    public float growthRate = 1f; // rate at which the sphere grows
    public float maxScale = 5f; // maximum size of the sphere
    public float lifetime = 2f; // duration of the effect in seconds

    public int missileReward;
    public int civPenalty;

    private float timeElapsed = 0f; // time elapsed since the effect started

    public GameObject explosionPrefab;
    public GameObject craterPrefab;
    public GameObject poof;


    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log(collision.gameObject.tag);
        if(collision.gameObject.tag == "Projectile")
        {
            Instantiate(explosionPrefab, collision.gameObject.transform.position, collision.gameObject.transform.rotation);

            Missile_Score scoreboard = FindObjectOfType<Missile_Score>();

            scoreboard.ScoreMissile(missileReward);

            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "Building")
        {
            Instantiate(craterPrefab, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "NPC")
        {
            Missile_Score scoreboard = FindObjectOfType<Missile_Score>();
            scoreboard.ScoreMissile(civPenalty);
            Instantiate(poof, collision.gameObject.transform.position, collision.gameObject.transform.rotation);
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<Missile_Player>().Explode();
            Destroy(collision.gameObject);
        }

    }

    void Start()
    {
        maxScale = Random.Range(maxScale * .5f, maxScale);
        lifetime = Random.Range(lifetime * .5f, lifetime);
    }

    void Update()
    {
        //increase the size of the sphere over time
        transform.localScale += Vector3.one * growthRate * Time.deltaTime;

        //clamp the size of the sphere to the maximum size
        transform.localScale = Vector3.Min(transform.localScale, Vector3.one * maxScale);

        timeElapsed += Time.deltaTime;

        if (timeElapsed >= lifetime)
        {
            Destroy(gameObject);
        }
    }
}