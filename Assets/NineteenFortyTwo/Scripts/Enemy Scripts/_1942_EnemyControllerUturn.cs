using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class _1942_EnemyControllerUturn : _1942_EnemyController
{
    public float uturnTimer;
    public Quaternion rot;
    public Vector3 pos;

    public override void EnemyMove()
    {
        base.EnemyMove();
        //once below a certain y value, initiate a uturn
        if (transform.position.y < maneuverPoint)
        {
            //store rotation and position for uturn calculations
            rot = transform.rotation;
            pos = transform.position;
            state = "maneuver";
        }
    }

    public override void EnemyManeuver()
    {
        //the object rotates at a fixed speed over a fixed duration
        float angle = 360 * Time.deltaTime;
        uturnTimer += Time.deltaTime;
        transform.rotation *= Quaternion.AngleAxis(angle, Vector3.right);
        //continue to move laterally as normal, but its z coordinate is
        //locked to its value before the turn
        transform.Translate(movePath * flightSpeed * Time.deltaTime);
        pos.x = transform.position.x;
        pos.y = transform.position.y;
        transform.position = pos;
        //once the timer reaches its limit, lock down the final rotation and position
        if (uturnTimer >= .25 && !hasFired)
        {
            EnemyShoot();
            hasFired = true;
        }
        if (uturnTimer >= .5)
        {
            rot.x = 180;
            transform.rotation = rot;
            transform.position = pos;
            state = "idle";
        }
    }
}



