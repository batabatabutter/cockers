using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotSpear : Enemy
{
    //  吹っ飛び
    [SerializeField, HeaderAttribute("個別")] float shotSpeed;

    //  再始動タイミング
    [SerializeField] float coolTime;

    //  飛んでいる時間
    [SerializeField] float flyTime;

    //  行動してくる距離
    [SerializeField] float distance;

    //  時間カウント用
    private float time;

    private bool move = false;

    //  最初に実行
    public override void EnemyStart()
    {
        time = coolTime;
    }

    //  更新
    public override void EnemyUpdate()
    {
        //  時間減少
        time -= Time.deltaTime;
        if (time < 0.0f) time = 0.0f;

        //  敵とプレイヤーの位置が近かったら攻撃
        if (Vector3.Distance(this.transform.position, player.transform.position) < distance && time <= 0.0f)
        {
            Vector3 angle = (player.transform.position - transform.position);
            transform.rotation = Quaternion.Euler(new Vector3(-angle.normalized.x, angle.normalized.z, angle.normalized.y) * 180.0f);
            rb.velocity += angle.normalized * shotSpeed;
            rb.velocity += Vector3.up * shotSpeed * 0.1f;
            time = coolTime + flyTime;
            move = true;
        }
        else if (time <= coolTime && move)
        {
            transform.rotation = Quaternion.identity;
            rb.velocity = Vector3.zero;
            move = false;
        }
    }
}
