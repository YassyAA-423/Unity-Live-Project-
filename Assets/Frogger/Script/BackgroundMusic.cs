using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[System.Serializable]
public class SceneMusic
{
    public string sceneName;
    public AudioClip music;
}


public class BackgroundMusic : MonoBehaviour
{
    public AudioClip testClip;

    private static BackgroundMusic instance;
    private AudioSource audioSource;

    [Header("Scene Music Setup")]
    public List<SceneMusic> sceneMusicList;

    private Dictionary<string, AudioClip> musicDictionary;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);

            audioSource = GetComponent<AudioSource>();

            // Convert list → dictionary for fast lookup
            musicDictionary = new Dictionary<string, AudioClip>();
            foreach (var item in sceneMusicList)
            {
                if (!musicDictionary.ContainsKey(item.sceneName))
                {
                    musicDictionary.Add(item.sceneName, item.music);
                }
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (musicDictionary.TryGetValue(scene.name, out AudioClip newClip))
        {
            if (audioSource.clip != newClip)
            {
                audioSource.clip = newClip;
                audioSource.Play();
            }
            else if (!audioSource.isPlaying)
            {
                audioSource.Play();
            }
        }
        else
        {
            // No entry = no music (menu, etc.)
            audioSource.Stop();
        }

    }
}