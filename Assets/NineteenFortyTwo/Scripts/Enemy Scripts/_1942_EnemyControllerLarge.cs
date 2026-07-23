using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using static UnityEditor.PlayerSettings;
using static UnityEngine.Rendering.DebugUI.Table;

public class _1942_EnemyControllerLarge : _1942_EnemyController
{
    public bool hasTurned = false;
    public float shotTimer = 0;

    public override void EnemyMove()
    {
        base.EnemyMove();
        if (transform.position.y < maneuverPoint && !hasTurned)
        {
            //store rotation and position for uturn calculations
            hasTurned = true;
            state = "maneuver";
            if (transform.position.x < 0) { movePath = Vector3.right; }
            else { movePath = Vector3.left; }
        }
        if (hasTurned)
        {
            shotTimer += Time.deltaTime;
            if (shotTimer > .6)
            {
                EnemyShoot();
                shotTimer = 0;
            }
        }
    }

    public override void EnemyManeuver()
    {
        EnemyMove();
    }

    public override void Die()
    {
        GameObject splash =  Instantiate(splashParticles, transform.position, transform.rotation);
        splash.transform.localScale = Vector3.one*2;
        Destroy(gameObject);
    }
}


