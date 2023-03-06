using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salad : Enemy
{
    //  弾の速度
    [SerializeField, HeaderAttribute("個別")] private float bulletSpeed;
    //  弾のオブジェクト
    [SerializeField] private GameObject bulletPrefab;
    //  頭のオブジェクト
    [SerializeField] private GameObject head;
    //  撃つタイミング
    [SerializeField] private float coolTime;
    //  タイマー
    private float timer;

    //  ジャンプ力
    [SerializeField] private float jumpForce;
    //  ジャンプタイミング
    [SerializeField] private float jumpTime;
    //  ジャンプ用タイマー
    private float jumpTimer;

    //  最初に実行
    public override void EnemyStart()
    {
        timer = coolTime;
        jumpTimer = jumpTime;
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
            Vector3 createPos = head.transform.position + gameObject.transform.forward;
            //  発射方向を算出
            Vector3 bulletDir = (gameObject.transform.forward + Vector3.up) * bulletSpeed;

            //  弾を生成
            GameObject bullet = Instantiate(bulletPrefab, createPos, Quaternion.identity);
            //  弾の発射方向を設定
            bullet.GetComponent<Bullet>().Shot(bulletDir, statas.ATK);
        }

        if (jumpTimer > 0.0f) jumpTimer -= Time.deltaTime;
        else
        {
            jumpTimer = jumpTime;

            rb.AddForce(Vector3.up * jumpForce);
        }
    }
}
