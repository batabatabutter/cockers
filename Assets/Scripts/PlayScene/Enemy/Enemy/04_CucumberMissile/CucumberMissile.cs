using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CucumberMissile : Enemy
{
    private Vector3 vel;

    [SerializeField] private float time = 3f;
    private float timer = 0f;

    public override void EnemyStart()
    {
        nowAttack = true;
    }

    public override void EnemyUpdate()
    {
        if (timer > 0f) timer -= Time.deltaTime;
        else Destroy(gameObject);

        gameObject.GetComponent<Rigidbody>().velocity = vel;

        gameObject.transform.localEulerAngles = new Vector3(0f, 0f, -90f);
    }
    //  ’e‚ð”­ŽË
    public void Shot(Vector3 dir)
    {
        //  —Í‚ð—^‚¦‚é
        vel = dir;

        timer = time;
    }
}
