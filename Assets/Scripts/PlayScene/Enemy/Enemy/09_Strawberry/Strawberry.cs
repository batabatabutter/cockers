using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : Enemy
{
    //  移動速度
    [SerializeField, HeaderAttribute("個別")] private float moveSpeed;
    //  回転時間
    [SerializeField] private float rotTime;
    //  停止時間
    [SerializeField] private float coolTime;
    //  体
    [SerializeField] private GameObject body;

    //  行動する距離
    [SerializeField] private float distance;

    //  時間カウント用
    private float coolTimer;
    private float rotTimer;
    //  動いてる状況
    private bool moving = false;

    //  移動量
    private Vector3 vel;

    //  最初に実行
    public override void EnemyStart()
    {
        coolTimer = 0.0f;
        rotTimer = 0.0f;

        vel = Vector3.zero;
    }

    //  更新
    public override void EnemyUpdate()
    {
        if(coolTimer > 0.0f)
        {
            coolTimer -= Time.deltaTime;
            return;
        }

        if(!moving)
        {
            //  プレイヤーが一定の距離内に入ったら動き出す
            float pDistance = Vector3.Distance(player.transform.position, body.transform.position);
            Debug.Log(pDistance);
            if(pDistance < distance)
            {
                //  移動状態にする
                moving = true;
                //  タイマーを設定
                rotTimer = rotTime;
                //  攻撃状態にする
                nowAttack = true;

                return;
            }

            //  移動

        }
        else
        {
            //  時間を減らす
            rotTimer -= Time.deltaTime;

            if(rotTimer < 0.0f)
            {
                //  止める
                moving = false;
                //  タイマーを設定
                coolTimer = coolTime;
                //  攻撃状態にする
                nowAttack = false;
                //  止める
                rb.velocity = Vector3.zero;

                return;
            }

            //  プレイヤーの方向に移動する
            float x = player.transform.position.x - body.transform.position.x;
            //  移動量を更新
            vel.x += (x * moveSpeed);
            rb.velocity = vel;
        }
    }
}
