using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabbageTaro : Enemy
{
    //  移動速度
    [SerializeField, HeaderAttribute("個別")] float speed;

    //  切り替えしタイミング
    [SerializeField] float inversionTime;

    //  時間カウント用
    private float time;

    //  最初に実行
    public override void EnemyStart()
    {
        time = inversionTime;
    }

    //  更新
    public override void EnemyUpdate()
    {
        //  時間減少
        time -= Time.deltaTime;
        //  条件が合えば左右反転
        if (time < 0.0f)
        {
            gameObject.transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
            time = inversionTime;
        }

        //  移動
        rb.MovePosition(rb.position + speed * Time.deltaTime * gameObject.transform.right);
    }
}
