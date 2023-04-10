using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltRockMan : Boss
{
    enum Attack
    {
        rush,
        throwSalt,
        arm,

        attackNum,
        none
    };

    [Header("個別")]
    [SerializeField, Label("攻撃間隔")] float coolTime;
    [SerializeField, Label("近接行動距離")] float clossDistance;
    [SerializeField, Label("超近接距離")] float veryCloseDistance;
    [SerializeField, Label("移動速度")] float moveSpeed;

    [Header("突進")]
    [SerializeField, Label("突進時間")] float rushTime;
    [SerializeField, Label("突進速度")] float rushSpeed;
    [SerializeField, Label("突進前隙")] float rushForwordTime;
    [SerializeField, Label("突進後隙")] float rushAfterTime;

    [Header("塩投げ")]
    [SerializeField, Label("投げ時間")] float throwSaltTime;
    [SerializeField, Label("塩投げ前隙")] float throwSaltForwordTime;
    [SerializeField, Label("塩投げ後隙")] float throwSaltAfterTime;

    [SerializeField, Label("塩投げ速度")] float throwSaltSpeed;
    [SerializeField, Label("塩投げ物")] GameObject throwSaltObject;

    [Header("腕を振る")]
    [SerializeField, Label("腕振り時間")] float armTime;
    [SerializeField, Label("腕振り前隙")] float armForwordTime;
    [SerializeField, Label("腕振り後隙")] float armAfterTime;

    [Header("その他")]
    [SerializeField, Label("体のヒットボックス")] Collider bodyHitBox;
    [SerializeField, Label("腕のヒットボックス")] Collider armHitBox;
    
    // デバッグ用振り下ろす手
    [SerializeField] Transform arm;
    //  指先
    [SerializeField] GameObject hand;

    //  時間カウント用
    private float time;

    //  各状態
    private bool move;
    private Attack attack;

    private Vector3 angle;

    //  最初に実行
    public override void BossStart()
    {
        time = coolTime;
        bodyHitBox.enabled = false;
        armHitBox.enabled = false;
    }

    //  更新
    public override void BossUpdate()
    {
        //  時間減少
        time -= Time.deltaTime;
        //  条件が合えば左右反転
        if (time < 0.0f) time = 0.0f;

        if (Vector3.Distance(transform.position, player.transform.position) < clossDistance && time <= 0.0f)
        {
            do
            {
                attack = (Attack)Random.Range(0, (int)Attack.attackNum);
            } while (attack == Attack.throwSalt);
            move = true;
            rb.velocity = Vector3.zero;
        }
        else if(time <= 0.0f)
        {
            do
            {
                attack = (Attack)Random.Range(0, (int)Attack.attackNum);
            } while (attack == Attack.arm);
            move = true;
            rb.velocity = Vector3.zero;
        }

        //  動き
        if (move)
        {
            switch (attack)
            {
                case Attack.rush:
                    Rush();
                    break;
                case Attack.throwSalt:
                    ThrrowSalt();
                    break;
                case Attack.arm:
                    Arm();
                    break;
            }
        }
        else
        {
            //  方向転換
            float distance = (player.transform.position.x - gameObject.transform.position.x);
            if (distance < 0.0f) transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0.0f, transform.rotation.z));
            else transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 180.0f, transform.rotation.z));

            //  近づきマン
            angle = new Vector3(player.transform.position.x - transform.position.x, 0.0f, 0.0f);
            rb.velocity = angle.normalized * moveSpeed;

            //  近かったら停止
            if (veryCloseDistance >= distance && -veryCloseDistance <= distance) rb.velocity = Vector3.zero;            
        }
    }

    //  突進攻撃
    private void Rush()
    {
        //  最初
        if (time <= 0.0f)
        {
            time = coolTime + rushAfterTime + rushTime + rushForwordTime;
        }
        //  前隙
        else if(time >= coolTime + rushAfterTime + rushTime)
        {
            arm.Rotate(new Vector3(0.0f, 0.0f, 270.0f) * Time.deltaTime / rushForwordTime);
        }
        //  実行動
        else if(time >= coolTime + rushAfterTime)
        {
            //  最初だけ実行される
            if (!nowAttack)
            {
                angle = new Vector3(player.transform.position.x - transform.position.x, 0.0f, 0.0f);
                rb.velocity = angle.normalized * rushSpeed;
            }
            bodyHitBox.enabled = true;
            nowAttack = true;
        }
        //  後隙
        else if(time >= coolTime)
        {
            arm.Rotate(new Vector3(0.0f, 0.0f, -270.0f) * Time.deltaTime / rushAfterTime);
            bodyHitBox.enabled = false;
            nowAttack = false;
            rb.velocity = Vector3.zero;
        }
        //  最後
        else
        {
            move = false;
        }
    }

    //  塩投げ
    private void ThrrowSalt()
    {
        //  攻撃
        if (time <= 0.0f)
        {
            time = coolTime + throwSaltAfterTime + throwSaltTime + throwSaltForwordTime;
            move = true;
        }
        //  前隙
        else if (time >= coolTime + throwSaltAfterTime + throwSaltTime)
        {
            arm.Rotate(new Vector3(0.0f, 0.0f, -45.0f) * Time.deltaTime / throwSaltForwordTime);
        }
        //  実行動
        else if (time >= coolTime + throwSaltAfterTime)
        {
            //  最初だけ実行
            if (!nowAttack)
                CreateSalt();
            armHitBox.enabled = true;
            nowAttack = true;
            arm.Rotate(new Vector3(0.0f, 0.0f, 165.0f) * Time.deltaTime / throwSaltTime);
        }
        //  後隙
        else if(time >= coolTime)
        {
            //  最初だけ実行
            if (nowAttack)
                CreateSalt();
            armHitBox.enabled = false;
            nowAttack = false;
            arm.Rotate(new Vector3(0.0f, 0.0f, -120.0f) * Time.deltaTime / throwSaltAfterTime);

        }
        //  最後
        else
        {
            move = false;
        }
    }

    //  塩生成
    private void CreateSalt()
    {
        angle = (player.transform.position - hand.transform.position);
        Vector3 pos = hand.transform.position;
        pos.z = 0.0f;
        GameObject obj = Instantiate(throwSaltObject, pos, Quaternion.identity);
        obj.GetComponent<Rigidbody>().velocity = angle.normalized * throwSaltSpeed;
    }

    //  腕ブンブン
    private void Arm()
    {
        //  攻撃
        if (time <= 0.0f)
        {
            time = coolTime + armAfterTime + armTime + armForwordTime;
            move = true;
        }
        //  前隙
        else if (time >= coolTime + armAfterTime + armTime)
        {
            arm.Rotate(new Vector3(0.0f, 0.0f, -45.0f) * Time.deltaTime / armForwordTime);
        }
        //  実行動
        else if (time >= coolTime + armAfterTime)
        {
            armHitBox.enabled = true;
            nowAttack = true;
            arm.Rotate(new Vector3(0.0f, 0.0f, 165.0f) * Time.deltaTime / armTime);
        }
        //  後隙
        else if (time >= coolTime)
        {
            armHitBox.enabled = false;
            nowAttack = false;
            arm.Rotate(new Vector3(0.0f, 0.0f, -120.0f) * Time.deltaTime / armAfterTime);

        }
        //  最後
        else
        {
            move = false;
        }
    }
}
