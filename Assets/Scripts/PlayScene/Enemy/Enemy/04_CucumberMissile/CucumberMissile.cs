using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CucumberMissile : Enemy
{
    private Vector3 vel;
    public override void EnemyStart()
    {
        nowAttack = true;
    }

    public override void EnemyUpdate()
    {
        gameObject.GetComponent<Rigidbody>().velocity = vel;
    }
    //  �e�𔭎�
    public void Shot(Vector3 dir)
    {
        //  �͂�^����
        vel = dir;
    }
}
