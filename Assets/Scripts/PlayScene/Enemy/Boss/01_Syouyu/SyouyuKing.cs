using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyouyuKing : Boss
{
    //  攻撃の種類
    private enum AttackType
    {
        Attack01,
        Attack02,
        Attack03,

        OverID
    };
    //  攻撃の手順
    private enum Attack
    {
        Move01,
        Move02,
        Move03,
        Move04,
        Move05,

        OverID
    };

    //  攻撃種類
    private AttackType attackType;
    //  攻撃手順
    private Attack attack;

    //  醤油prefab
    [SerializeField] private List<GameObject> syouyuPrefabs;
    //  醤油を飛ばす頭
    [SerializeField] private GameObject head;

    //  クールタイム
    [SerializeField] private float coolTime;
    //  クールタイマー
    private float coolTimer;

    //  攻撃範囲
    [SerializeField] private float attackDistance;

    //  ジャンプ力
    [SerializeField] private float jumpForce;
    //  ジャンプ判定
    private bool jump;

    //  目標地点
    private Vector3 targetPos;

    //  角度
    private int rotX;
    //  醤油の発射速度
    [SerializeField] private float syouyuSpeed;
    //  汎用タイマー
    private float timer;
    //  発射タイム
    [SerializeField] private float shotTime;

    //  初期処理
    public override void BossStart()
    {
        attackType = AttackType.OverID;

        attack = Attack.OverID;

        coolTimer = 0f;

        rb = gameObject.GetComponent<Rigidbody>();

        targetPos = Vector3.zero;

        rotX = 0;
        jump = false;
        timer = 0f;
    }

    //  更新処理
    public override void BossUpdate()
    {
        //  クールタイムが存在している
        if (coolTimer > 0f)
        {
            //  クール時間減らす
            coolTimer -= Time.deltaTime;
            //  処理終了
            return;
        }

        //  プレイヤーが攻撃範囲内にいない・攻撃状態ではない
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) > attackDistance &&
            attackType == AttackType.OverID)
        {
            //  プレイヤーに向かって移動する

            //  処理終了
            return;
        }
        //  プレイヤーが攻撃範囲内にいる・攻撃状態ではない
        else if(attackType == AttackType.OverID)
        {
            //  攻撃の種類を設定する
            attackType = (AttackType)Random.Range(0, (int)AttackType.OverID);
            
            //  攻撃手順を１へ
            attack = Attack.Move01;
            //  処理終了
            return;
        }

        //  攻撃の処理
        switch (attackType)
        {
            case AttackType.Attack01:
                //  攻撃終了判定
                if (Attack01()) FinishAttack();
                break;
            case AttackType.Attack02:
                //  攻撃終了判定
                if (Attack02()) FinishAttack();
                break;
            case AttackType.Attack03:
                //  攻撃終了判定
                if (Attack03()) FinishAttack();
                break;
        }
    }
    //  攻撃１（醤油を八方向に発射）
    private bool Attack01()
    {
        switch(attack)
        {
            //  ジャンプ
            case Attack.Move01:
                //  上向きに力を加える
                if (!jump)
                {
                    rb.AddForce(Vector3.up * jumpForce);
                    jump = true;
                }
                //  次のステップへ
                if(rb.velocity.y > 0f) attack++;
                return false;

            //  重力を切る/空中で静止
            case Attack.Move02:
                //  落ち始めている
                if(rb.velocity.y <= 0f)
                {
                    //  重力を切る
                    rb.useGravity = false;
                    //  静止させる
                    rb.velocity = Vector3.zero;
                    //  静止位置を設定
                    targetPos = gameObject.transform.position;
                    //  次のステップへ
                    attack++;
                }
                return false;

            //  回転しながら頭から醤油を飛ばす
            case Attack.Move03:
                //  静止させる
                rb.velocity = Vector3.zero;
                //  停止位置で固定する
                gameObject.transform.position = targetPos;
                //  回転させる
                rotX += 1;
                gameObject.transform.localEulerAngles = new Vector3((float)rotX, -90f, 0f);

                //  各角度で弾を発射
                if (rotX % 45 == 0)
                {
                    //  角度を算出
                    float rad = (rotX + 90) * Mathf.Deg2Rad;
                    //  進行方向を算出
                    Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f).normalized;
                    //  弾を発射
                    CreateSyouyu(syouyuPrefabs[0], dir * syouyuSpeed);
                }
                //  一周したら攻撃終了
                if(rotX >= 360)
                {
                    //  重力を戻す
                    rb.useGravity = true;
                    //  本体の角度を戻す
                    gameObject.transform.localEulerAngles = new Vector3(0f, -90f, 0f);

                    //  攻撃終了
                    return true;
                }
                return false;
        }
        //  攻撃終了
        return true;
    }
    //  攻撃２（醤油をプレイヤーに向かって発射）
    private bool Attack02()
    {
        switch(attack)
        {
            //  予備動作用(とりあえず後ろにジャンプ)
            case Attack.Move01:
                //  プレイヤーと反対方向を算出
                float pDir = (player.transform.position.x - gameObject.transform.position.x) * -1;
                //  飛ぶ方向
                Vector3 d = new Vector3(pDir, 0f, 0f).normalized + Vector3.up;
                //  飛ばす
                rb.AddForce(d * jumpForce / 1.2f);
                //  攻撃までの余白タイム
                timer = -1.0f;
                //  次のステップへ
                attack++;
                return false;

            //  プレイヤーに向かって撃つ
            case Attack.Move02:
                //  発射時間をカウント
                timer += Time.deltaTime;
                //  発射時間を超える
                if(timer > shotTime)
                {
                    //  カウントを０
                    timer = 0f;
                    //  プレイヤー方向
                    Vector3 dir = (player.transform.position - head.transform.position).normalized;
                    //  生成
                    CreateSyouyu(syouyuPrefabs[0], dir * syouyuSpeed);
                    //  カウントする
                    rotX++;
                    //  攻撃終了
                    if (rotX > 10) return true;
                }
                return false;
        }

        //  攻撃終了
        return true;
    }
    //  攻撃３
    private bool Attack03()
    {
        switch(attack)
        {
            //  予備動作（ジャンプ）
            case Attack.Move01:
                //  上向きに力を加える
                if (!jump)
                {
                    rb.AddForce(Vector3.up * jumpForce);
                    jump = true;
                }
                //  次のステップへ
                if (rb.velocity.y > 0f) attack++;
                return false;

            //  予備動作（とりあえず震える）
            case Attack.Move02:
                //  落ち始めている
                if (rb.velocity.y <= 0f)
                {
                    //  震える
                    gameObject.GetComponent<ShakeByPerlinNoise>().StartShake(2f, 0.1f, 0.1f);

                    //  一秒震える
                    timer += Time.deltaTime;
                }
                
                if(timer > 1f)
                {
                    //  リセット
                    timer = 0f;
                    //  次のステップへ
                    attack++;
                }
                return false;

            //  醤油を発射(震えながら)
            case Attack.Move03:
                //  弾を発射（３発）
                for (int i = 0; i < 3; i++)
                {
                    //  ｘ軸方向をランダムで取得
                    float x = Random.Range(-1f, 1f);
                    //  発射方向
                    Vector3 di = new Vector3(x, 1f, 0f).normalized;
                    //  生成
                    CreateSyouyu(syouyuPrefabs[1], di * syouyuSpeed);
                }
                //  終了
                return true;
        }

        //  攻撃終了
        return true;
    }
    //  攻撃終了処理
    private void FinishAttack()
    {
        //  クールタイムを設定
        coolTimer = coolTime;

        //  攻撃の種類を消す
        attackType = AttackType.OverID;
        //  攻撃を初期状態へ
        attack = Attack.OverID;

        //  各変数の初期化
        targetPos = Vector3.zero; 
        rotX = 0;
        jump = false;
        timer = 0f;
    }
    //  醤油を作る
    private void CreateSyouyu(GameObject syouyuP, Vector3 dir)
    {
        //  生成
        GameObject syouyu = Instantiate(syouyuP, head.transform.position, Quaternion.identity);
        //  方向に飛ばす
        syouyu.GetComponent<Rigidbody>().AddForce(dir);
    }
}
