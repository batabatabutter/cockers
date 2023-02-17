using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabbageTaro : MonoBehaviour
{
    //  ステータス用
    [SerializeField] EnemyStatas statas;

    //  移動速度
    [SerializeField] float speed;

    //  切り替えしタイミング
    [SerializeField] float inversionTime;

    //  ぶつかった時のダメージ倍率
    [SerializeField] float collisionDMG;

    //  時間カウント用
    private float time;

    //  最初に実行
    private void Start()
    {
        time = inversionTime;
    }


    //  更新
    void Update()
    {
        //  時間減少
        time -= Time.deltaTime;
        //  条件が合えば左右反転
        if (time < 0.0f)
        {
            this.gameObject.transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
            time = inversionTime;
        }

        //  移動
        this.gameObject.transform.position += speed * Time.deltaTime * this.gameObject.transform.right;
    }
}
