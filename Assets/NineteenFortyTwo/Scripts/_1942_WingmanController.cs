using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _1942_WingmanController : MonoBehaviour
{

    private _1942_PlayerController playerScript;
    
    // Start is called before the first frame update
    void Start()
    {
        if (playerScript == null) { playerScript = FindObjectOfType<_1942_PlayerController>(); }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            playerScript.hardpoints.Remove(gameObject);
            Destroy(gameObject);
        }
        else if (collision.gameObject.layer == 7)
        {
            _1942_EnemyController enemyScript = collision.gameObject.GetComponent<_1942_EnemyController>();
            if (enemyScript != null) { enemyScript.hp = 0; }
            playerScript.hardpoints.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
