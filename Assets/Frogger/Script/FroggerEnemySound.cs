using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class FroggerEnemySound : MonoBehaviour
{
    [SerializeField] AudioClip Moving;

    private void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>(); 
        audioSource.clip = Moving;
        audioSource.Play();
    }

}



