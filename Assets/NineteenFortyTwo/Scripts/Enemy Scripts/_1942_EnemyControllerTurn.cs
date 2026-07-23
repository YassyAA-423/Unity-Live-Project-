using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class _1942_EnemyControllerTurn : _1942_EnemyController
{
    public float turnTimer;
    public float turnAngle;
    public bool hasTurned = false;
    public Animator animator;
    public float roll;

    public override void Start()
    {
        base.Start();
        animator = gameObject.GetComponentInChildren<Animator>();
    }

    public override void EnemyMove()
    {
        base.EnemyMove();
        //once below a certain y value, initiate a uturn
        if (transform.position.y < maneuverPoint && !hasTurned)
        {
            //store rotation and position for uturn calculations
            hasTurned = true;
            state = "maneuver";
            if (transform.position.x < 0) { turnAngle = turnAngle * -1; }
        }
    }

    public override void EnemyManeuver()
    {

        float angle = turnAngle * Time.deltaTime;
        animator.SetFloat("Horiz", roll);
        turnTimer += Time.deltaTime;
        transform.rotation *= Quaternion.AngleAxis(angle, Vector3.back);
        base.EnemyMove();

        if (turnTimer >= 1 && !hasFired)
        {
            EnemyShoot();
            hasFired = true;
        }
        if (turnTimer < 1)
        {
            roll -= Time.deltaTime * (Math.Abs(angle) / angle);
        }
        else
        {
            roll += Time.deltaTime * (Math.Abs(angle) / angle);
        }
        if (turnTimer >= 2)
        {
            //set the player back to their starting y and z position and rotation
            turnTimer = 0;
            roll = 0;
            animator.SetFloat("Horiz", roll);
            state = "idle";
        }
    }
}



