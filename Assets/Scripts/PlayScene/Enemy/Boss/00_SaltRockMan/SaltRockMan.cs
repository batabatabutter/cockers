using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltRockMan : Enemy
{
    //  移動速度
    [SerializeField, HeaderAttribute("個別")] float shotSaltDmgRate;

    //  振るタイミング
    [SerializeField] float coolTime;

    //  時間カウント用
    private float time;

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
        if (time < 0.0f)
        {
            time = coolTime;
        }
    }
}
