using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class _1942_EnemyControllerStraight : _1942_EnemyController
{
    public float turnTimer;
    public float turnAngle;

    public override void EnemyMove()
    {
        base.EnemyMove();
        if (transform.position.y < maneuverPoint && !hasFired)
        {
            EnemyShoot();
            hasFired = true;
        }
    }
}



