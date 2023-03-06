using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Enemy
{
    //  弾のオブジェクト
    [SerializeField, HeaderAttribute("個別")] private GameObject bulletPrefab;
    //  弾の方向
    [SerializeField] private float bulletSpeed;
    //  頭のオブジェクト
    [SerializeField] private GameObject head;
    //  撃つタイミング
    [SerializeField] private float coolTime;
    //  タイマー
    private float timer;

    //  プレイヤーの位置
    private Transform pTransform;

    private Vector3 vel;

    //  最初に実行
    public override void EnemyStart()
    {
        timer = coolTime;

        pTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //  更新
    public override void EnemyUpdate()
    {
        //  時間を減らす
        if (timer > 0.0f) timer -= Time.deltaTime;
        else
        {
            //  クールタイムを設定
            timer = coolTime;

            //  生成位置を算出
            Vector3 createPos = head.transform.position + gameObject.transform.forward + Vector3.up;
            //  発射方向を算出
            Vector3 bulletDir = (gameObject.transform.forward + Vector3.up) * bulletSpeed;

            //  弾を生成
            GameObject bullet = Instantiate(bulletPrefab, createPos, Quaternion.identity);
            //  弾の発射方向を設定
            bullet.GetComponent<Bullet>().Shot(bulletDir, statas.ATK);
        }

        //  移動量
        float x = pTransform.position.x - gameObject.transform.position.x;
        vel.x += x * Time.deltaTime;
        rb.velocity = vel;
    }
}
