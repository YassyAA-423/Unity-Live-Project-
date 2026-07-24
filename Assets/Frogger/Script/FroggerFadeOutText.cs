using UnityEngine;
using System.Collections;

public class FroggerFadeOutText : MonoBehaviour
{
    public GameObject targetCanvas; // Drag your Canvas here in the Inspector
    public float delayInSeconds = 3f;

    void Start()
    {
        if (targetCanvas == null)
        {
            targetCanvas = GameObject.Find("FindHerCanvas");
        }

        targetCanvas.SetActive(true);
        StartCoroutine(DisableAfterDelay());
    }

    IEnumerator DisableAfterDelay()
    {
        yield return new WaitForSeconds(delayInSeconds);
        // Option A: Deactivate the entire GameObject
        targetCanvas.SetActive(false);

    }
}