using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking.Types;

public class _1942_PlayerCorpseController : MonoBehaviour
{
    public AudioSource source;
    public AudioClip deathClip;
    public GameObject referee;
    public GameObject player;
    public _1942_GameRefereeController refereeScript;
    public GameObject deathParticles;
    public Material material;
    public float timer = 0;
    public _1942_SceneLoader sl;


    // Start is called before the first frame update
    void Start()
    {
        transform.parent = null; 

        if (referee == null) { referee = GameObject.Find("Game Referee"); }
        if (refereeScript == null) { refereeScript = referee.GetComponent<_1942_GameRefereeController>(); }
        if (source == null) { source = gameObject.GetComponent<AudioSource>(); }
        if (material == null) { material = gameObject.GetComponent<Renderer>().material; }
        if (sl == null) { sl = FindObjectOfType<_1942_SceneLoader>(); }

        source.clip = deathClip;
        source.volume = .2f;
        source.pitch = .6f;
        source.Play();

        GameObject part = Instantiate(deathParticles, gameObject.transform);
        part.transform.localScale = gameObject.transform.localScale;
        Time.timeScale = .7f;
    }

    // Update is called once per frame
    void Update()
    {
        material.SetFloat("Dark", 1);
        gameObject.transform.localScale += new Vector3(-.1f, -.1f, -.1f) * Time.deltaTime;
        timer += Time.deltaTime;
        if (timer > .5f)
        {
            Time.timeScale = 1f;
        }
        if (timer > 1)
        {
            if (FindObjectsOfType<_1942_PlayerController>().Length < 1 && refereeScript.lives > 0) 
            {
                refereeScript.lives -= 1;
                Instantiate(player, Vector3.zero, Quaternion.identity);
                Destroy(gameObject);
            }
            else
            {
                Debug.Log("game over");
                sl.LoadSceneName("_1942_GameOver");
            }
        }
    }
}
