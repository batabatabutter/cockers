using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigSoldier : Enemy
{
    //  移動速度
    [SerializeField, HeaderAttribute("個別")] float swordDmgRate;

    //  振るタイミング
    [SerializeField] float coolTime;

    //  剣を振る時間
    [SerializeField] float swordTime;

    //  前隙
    [SerializeField] float forwordTime;

    //  行動してくる距離
    [SerializeField] float distance;

    // デバッグ用振り下ろす手
    [SerializeField] Transform arm;

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
        //  条件が合えば左右反転
        if (time < 0.0f) time = 0.0f;

        if (!move)
        {
            float distance = (player.transform.position.x - gameObject.transform.position.x);
            if(distance < 0.0f) transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0.0f, transform.rotation.z));
            else transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 180.0f, transform.rotation.z));
        }

        //  敵とプレイヤーの位置が近かったら攻撃
        if (Vector3.Distance(this.transform.position, player.transform.position) < distance && time <= 0.0f)
        {
            time = coolTime + swordTime + forwordTime;
            move = true;
        }
        //  (デバッグ)前隙中
        else if(time >= coolTime + swordTime)
        {
            arm.Rotate(new Vector3(0.0f, 0.0f, -90.0f) * Time.deltaTime / forwordTime);
        }
        //  (デバッグ)振っているとき
        else if(time >= coolTime && move)
        {
            nowAttack = true;
            arm.Rotate(new Vector3(0.0f, 0.0f, 180.0f) * Time.deltaTime / swordTime);
        }
        //  振り終わった時
        else if (time <= coolTime && move)
        {
            nowAttack = false;
            move = false;
            arm.Rotate(new Vector3(0.0f, 0.0f, -90.0f));
            rb.velocity = Vector3.zero;
        }
    }
}
