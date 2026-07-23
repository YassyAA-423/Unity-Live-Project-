using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using UnityEditorInternal;
using UnityEngine;

public class _1942_PlayerController : MonoBehaviour
{
    public float flightSpeed;
    
    public GameObject bullet;
    public GameObject wingman;
    public GameObject corpse;
    public List<GameObject> hardpoints;
    public GameObject shotSpark;
    public float shotDelay;
    private Material material;
    private float invulnTimer = 0;

    public GameObject referee;
    public _1942_GameRefereeController refereeScript;
    private string state = "invulnerable";
    private Rigidbody rb;
    private Animator animator;

    //variables for shooting
    private float shotTimer;
    private int shotLimit = 3;
    
    //variables for roll behavior
    private float rolltimer = 0;
    // player position and rotation at start of roll
    private Quaternion rot;
    private Vector3 pos;

    public AudioSource shotSource;
    public AudioSource powerupSource;

    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null;

        if (rb == null) {  rb = this.GetComponent<Rigidbody>(); }
        if (referee == null) { referee = GameObject.Find("Game Referee"); }
        if (refereeScript == null) { refereeScript = referee.GetComponent<_1942_GameRefereeController>(); }
        if (shotSource == null) { shotSource = gameObject.GetComponents<AudioSource>()[0]; }
        if (powerupSource == null) { powerupSource = gameObject.GetComponents<AudioSource>()[1]; }
        if (material == null) { material = gameObject.GetComponentInChildren<Renderer>().material; }
        if (animator == null) { animator = gameObject.GetComponentInChildren<Animator>(); }

        shotTimer = shotDelay;
    }

    // Update is called once per frame
    void Update()
    {
        shotTimer += Time.deltaTime;
        switch(state) 
        {
            case "idle":
                PlayerMove();
                PlayerShoot();
                break;

            case "rolling":
                PlayerRoll();
                PlayerDrift();
                break;

            case "invulnerable":
                PlayerMove();
                PlayerFlash();
                break;

            case "dying":
                break;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        //ignore collisions while performing a roll
        if (state == "rolling") { return; }
        //if an enemy is hit, die off
        if ((collision.gameObject.layer == 7 || collision.gameObject.layer == 9) && state != "invulnerable") //collision with enemy or enemy projectile
        {
            Die();
            if (refereeScript.lives < 1)
            { 
                state = "dying";
            }
            
        }
        else if (collision.gameObject.layer == 17) // collectible layer
        {
            Destroy(collision.gameObject);
            PlayerPowerUp();
        }
    }

    //this method controls player movement while idle
    private void PlayerMove()
    {
        //fire2 initiates a roll, during which the player is invincible
        if (Input.GetButtonDown("Fire2") && state != "invulnerable")
        {
            pos = transform.position;
            rot = transform.rotation;
            gameObject.layer = 11; //move to ignore projectiles layer
            foreach (_1942_WingmanController wingman in FindObjectsOfType<_1942_WingmanController>())
            {
                wingman.gameObject.layer = 11;
            }
            state = "rolling";
        } 

        else
        {
            float horiz = Input.GetAxis("Horizontal");
            animator.SetFloat("Horiz", horiz);
            float vert = Input.GetAxis("Vertical");

            transform.Translate(horiz * flightSpeed * Time.deltaTime, vert * flightSpeed * Time.deltaTime, 0);
            //clamp player position to play area
            Vector3 newpos = transform.position;
            newpos.x = Mathf.Clamp(newpos.x, -8.75f, 8.75f);
            newpos.y = Mathf.Clamp(newpos.y, -4.5f, 4.5f);
            transform.position = newpos;
        }
    }

    //this method allows slight movement during a roll
    private void PlayerDrift()
    {
        float horiz = Input.GetAxis("Horizontal");
        transform.Translate(horiz * flightSpeed *.5f * Time.deltaTime, 0, 0);
        //clamp player position to play area
        Vector3 newpos = transform.position;
        newpos.x = Mathf.Clamp(newpos.x, -9.75f, 9.75f);
        transform.position = newpos;
    }

    //this method sends the player on a loop-the-loop for a set duration
    private void PlayerRoll()
    {
        float angle = -360 * Time.deltaTime;
        rolltimer += Time.deltaTime;
        transform.rotation *= Quaternion.AngleAxis(angle, Vector3.right);
        transform.Translate(0, flightSpeed * Time.deltaTime, 0);
        //once the duration is up, end the roll
        if (rolltimer >= 1)
        {
            //set the player back to their starting y and z position and rotation
            transform.position = new Vector3(transform.position.x, pos.y, pos.z);
            transform.rotation = rot;
            rolltimer = 0;
            gameObject.layer = 6; //return to player layer
            foreach (_1942_WingmanController wingman in FindObjectsOfType<_1942_WingmanController>())
            {
                wingman.gameObject.layer = 6;
            }
            state = "idle";
        }
    }

    private void PlayerShoot()
    {
        //fire a volley when fire is pressed, as long as bullets and timing allow
        if (Input.GetButton("Fire1") 
            && shotTimer >= shotDelay 
            && refereeScript.playerShotCount < shotLimit*hardpoints.Count)
        {
            //fire a bullet from each hardpoint in the hardpoint array
            foreach (GameObject hardpoint in hardpoints)
            {
                //produce a bullet, create particles, increment bullet count, and play a shot sound
                Instantiate(shotSpark, hardpoint.transform.position, Quaternion.LookRotation(Vector3.up));
                GameObject shot = Instantiate(bullet, hardpoint.transform.position, hardpoint.transform.rotation);

                shotSource.Play(); 
            } 
            shotTimer = 0;
        }
        
    }

    private void PlayerPowerUp()
    {
        Debug.Log("Power up!");
        powerupSource.Play();
        refereeScript.score += 200;
        if (hardpoints.Count == 2)
        {
            GameObject hardpoint = new GameObject();
            GameObject newHardpoint = Instantiate(hardpoint, transform);
            newHardpoint.transform.Translate(-.1f, .3f, 0);
            hardpoints.Add(newHardpoint);

            GameObject newHardpoint2 = Instantiate(hardpoint, transform);
            newHardpoint2.transform.Translate(.1f, .3f, 0);
            hardpoints.Add(newHardpoint2);
        }
        else if (hardpoints.Count == 4)
        {
            GameObject newHardpoint = Instantiate(wingman, transform);
            newHardpoint.transform.Translate(-1f, -.1f, 0);
            hardpoints.Add(newHardpoint);

            GameObject newHardpoint2 = Instantiate(wingman, transform);
            newHardpoint2.transform.Translate(1f, -.1f, 0);
            hardpoints.Add(newHardpoint2);
        }
        else if (hardpoints.Count == 5)
        {
            GameObject lonely = FindObjectOfType<_1942_WingmanController>().gameObject;
            hardpoints.Remove(lonely);
            Destroy(lonely);

            GameObject newHardpoint = Instantiate(wingman, transform);
            newHardpoint.transform.Translate(-1f, -.1f, 0);
            hardpoints.Add(newHardpoint);

            GameObject newHardpoint2 = Instantiate(wingman, transform);
            newHardpoint2.transform.Translate(1f, -.1f, 0);
            hardpoints.Add(newHardpoint2);
        }
        else { refereeScript.score += 800; }
    }

    private void PlayerFlash()
    {
        invulnTimer += Time.deltaTime;
        
        if (invulnTimer > 1)
        {
            material.SetFloat("Tint", 0);
            state = "idle";
        }
        else { material.SetFloat("Tint", 1); }
    }

    private void Die()
    {
        GameObject wreck = Instantiate(corpse, transform.position, Quaternion.identity);
        wreck.transform.parent = null;
        Destroy(gameObject);
    }
}   
            
