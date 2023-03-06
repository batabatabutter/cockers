using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onion : Enemy
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

    //  最初に実行
    public override void EnemyStart()
    {
        timer = coolTime;
    }

    //  更新
    public override void EnemyUpdate()
    {
        if (timer > 0.0f)
        {
            //  時間を減らす
            timer -= Time.deltaTime;
            return;
        }

        //  クールタイムを設定
        timer = coolTime;

        //  生成位置を算出
        Vector3 createPos = head.transform.position + gameObject.transform.forward;
        //  発射方向を算出
        Vector3 bulletDir = gameObject.transform.forward * bulletSpeed;

        //  弾を生成
        GameObject bullet = Instantiate(bulletPrefab, createPos, Quaternion.identity);
        //  弾の発射方向を設定
        bullet.GetComponent<Bullet>().Shot(bulletDir, statas.ATK);
    }
}
