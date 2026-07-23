using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class _1942_EnemyController : MonoBehaviour
{
    public string state;
    public int hp;
    public float flightSpeed;
    public int pointValue;
    public Vector3 movePath;
    public Material deathMaterial;

    public bool dropPowerup;
    public GameObject powerUp;

    public GameObject deathParticles;
    public GameObject splashParticles;

    public GameObject bullet;
    public GameObject[] hardpoints;
    public bool hasFired = false;

    public GameObject referee;
    public _1942_GameRefereeController refereeScript;

    public float maneuverPoint;

    public Material material;
    public float tint = 0;
    public float tintTimer = 0f;

    public AudioSource source;
    public AudioClip deathClip;

    private int debug = 0;

    // Start is called before the first frame update
    public virtual void Start()
    {
        if (referee == null) { referee = GameObject.Find("Game Referee"); }
        if (refereeScript == null) { refereeScript = referee.GetComponent<_1942_GameRefereeController>(); }
        if (material == null) { material = gameObject.GetComponentInChildren<Renderer>().material; }
        if (source == null) { source = gameObject.GetComponent<AudioSource>(); }
        state = "idle";

        
    }

    // Update is called once per frame
    public virtual void Update()
    {

        UpdateHitShader();
        switch (state)
        {
            case "idle":
                EnemyMove();
                break;
            case "maneuver":
                EnemyManeuver();
                break;
            case "dying":
                EnemyCrash();
                Invoke("Die", .8f);
                break;
        }
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (state == "dying") { return; }
        if (collision.gameObject.layer == 8) //player projectile layer
        {
            //ignore projectile hit if offscreen
            if (transform.position.y > 5 + transform.localScale.x ||
                transform.position.y < -5 - transform.localScale.x ||
                transform.position.x > 9 + transform.localScale.x ||
                transform.position.x < -9 - transform.localScale.x)
            {
                return;
            }
            //otherwise, take damage, initiate hitflash, and play hit sound
            hp -= 1;
            tint = 1;
            tintTimer = 0;
            source.Play();
        }

        if (collision.gameObject.layer == 6)
        {
            hp = 0;
        }

        //if out of hp, play death sound, add points, and die off
        if ((hp <= 0) && (collision.gameObject.layer == 8 || collision.gameObject.layer == 6)) //collision with player or player shot
        {
            Debug.Log(debug);
            debug += 1;
            source.clip = deathClip;
            source.volume = .2f;
            source.pitch = .6f;
            source.Play();
            refereeScript.score += pointValue;
            state = "dying";
            //create death particles that scale with enemy scale
            GameObject part = Instantiate(deathParticles, transform);
            //part.transform.localScale = gameObject.transform.localScale;
            
            if (dropPowerup) 
            { 
                Instantiate(powerUp, gameObject.transform);  
            }
            gameObject.layer = 19; //move to effects layer
        }

        //cull the object once it hits the death plane of enemy barriers
        else if (collision.gameObject.layer == 13) //enemy barrier layer
        {
            Destroy(gameObject);
        }
    }

    public virtual void EnemyMove()
    {
        
        transform.Translate(movePath * flightSpeed * Time.deltaTime);
    }

    public virtual void EnemyManeuver()
    {
        return;
    }

    //this method darkens the shader and causes the object to shrink and fall.
    public virtual void EnemyCrash()
    {
        material.SetFloat("Dark", 1);
        float angle = -90 * Time.deltaTime;
        transform.rotation *= Quaternion.AngleAxis(angle, Vector3.right);
        transform.Translate(movePath * flightSpeed * Time.deltaTime);
        gameObject.transform.localScale += new Vector3(-.3f,-.3f,-.3f)*Time.deltaTime;
        //gameObject.GetComponent<Rigidbody>().isKinematic = false;
    }

    public virtual void Die()
    {
        Instantiate(splashParticles, transform.position, transform.rotation);
        Destroy(gameObject);
        GetComponentInChildren<Renderer>().material = deathMaterial;
    }

    //this method updates the tint of the shader once hitflash is over
    public virtual void UpdateHitShader()
    {
        //material.SetFloat("Tint", tint);
        tintTimer += Time.deltaTime;
        if (tintTimer > .05f)
        {
            tintTimer = 0;
            tint = 0;
        }
    }

    public virtual void EnemyShoot()
    {
        foreach (GameObject hardpoint in hardpoints)
        {
            GameObject shot = Instantiate(bullet, hardpoint.transform.position, hardpoint.transform.rotation);
        }
    }
}


