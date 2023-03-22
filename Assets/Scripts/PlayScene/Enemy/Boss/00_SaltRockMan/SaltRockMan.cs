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

        attackNum
    };

    //  タイミング
    [SerializeField, HeaderAttribute("個別")] float coolTime;
    //  行動してくる距離
    [SerializeField] float distance;

    //  突進時間
    [SerializeField] float rushTime;
    //  突進速度
    [SerializeField] float rushSpeed;

    //  腕を振る時間
    [SerializeField] float throwSaltTime;
    //  前隙
    [SerializeField] float throwSaltForwordTime;
    //  塩を振るダメージ
    [SerializeField] float throwSaltDmgRate;
    //  塩を振るSpeed
    [SerializeField] float throwSaltSpeed;
    //  振るオブジェクト
    [SerializeField] GameObject throwSaltObject;

    //  腕を振る時間
    [SerializeField] float armTime;
    //  前隙
    [SerializeField] float armForwordTime;

    // デバッグ用振り下ろす手
    [SerializeField] Transform arm;
    //  指先
    [SerializeField] GameObject hand;

    //  時間カウント用
    private float time;

    //  
    private bool move;
    private Attack attack;

    private Vector3 angle;

    //  最初に実行
    public override void BossStart()
    {
        time = coolTime;
    }

    //  更新
    public override void BossUpdate()
    {
        //  時間減少
        time -= Time.deltaTime;
        //  条件が合えば左右反転
        if (time < 0.0f) time = 0.0f;

        if (Vector3.Distance(transform.position, player.transform.position) < distance && time <= 0.0f)
        {
            attack = (Attack)Random.Range(0, (int)Attack.attackNum);
            move = true;
        }

        if (move)
        {
            switch (attack)
            {
                case Attack.rush:
                    Debug.Log("Rush");
                    Rush();
                    break;
                case Attack.throwSalt:
                    Debug.Log("ThrowSalt");
                    ThrrowSalt();
                    break;
                case Attack.arm:
                    Debug.Log("Arm");
                    Arm();
                    break;
            }
        }
        else
        {
            float distance = (player.transform.position.x - gameObject.transform.position.x);
            if (distance < 0.0f) transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0.0f, transform.rotation.z));
            else transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 180.0f, transform.rotation.z));
        }
    }

    //  突進攻撃
    private void Rush()
    {
        if (time <= 0.0f)
        {
            nowAttack = true;
            angle = new Vector3(player.transform.position.x - transform.position.x, 0.0f, 0.0f);
            rb.AddForce(angle.normalized * rushSpeed);
            time = coolTime + rushTime;
        }
        else if (time >= coolTime)
        {
            nowAttack = false;
            rb.velocity = Vector3.zero;
            move = false;
        }
    }

    //  塩投げ
    private void ThrrowSalt()
    {
        //  攻撃
        if (time <= 0.0f)
        {
            time = coolTime + throwSaltTime + throwSaltForwordTime;
            move = true;
            Invoke(nameof(CreateSalt), 3.0f);
        }
        //  (デバッグ)前隙中
        else if (time >= coolTime + throwSaltTime)
        {
            arm.Rotate(new Vector3(0.0f, 0.0f, -45.0f) * Time.deltaTime / throwSaltForwordTime);
        }
        //  (デバッグ)振っているとき
        else if (time >= coolTime)
        {
            nowAttack = true;
            arm.Rotate(new Vector3(0.0f, 0.0f, 165.0f) * Time.deltaTime / throwSaltTime);
        }
        //  振り終わった時
        else
        {
            nowAttack = false;
            move = false;
            arm.Rotate(new Vector3(0.0f, 0.0f, -120.0f));
            rb.velocity = Vector3.zero;
        }
    }

    private void CreateSalt()
    {
        angle = (player.transform.position - transform.position);
        GameObject obj = Instantiate(throwSaltObject, hand.transform.position, Quaternion.identity);
        obj.GetComponent<Rigidbody>().velocity = angle.normalized * throwSaltSpeed;
        obj.GetComponent<Salt>().SetDmg((int)(throwSaltDmgRate * statas.ATK));
    }

    //  腕ブンブン
    private void Arm()
    {
        //  攻撃
        if (time <= 0.0f)
        {
            time = coolTime + armTime + armForwordTime;
            move = true;
        }
        //  (デバッグ)前隙中
        else if (time >= coolTime + armTime)
        {
            arm.Rotate(new Vector3(0.0f, 0.0f, -45.0f) * Time.deltaTime / armForwordTime);
        }
        //  (デバッグ)振っているとき
        else if (time >= coolTime)
        {
            nowAttack = true;
            arm.Rotate(new Vector3(0.0f, 0.0f, 165.0f) * Time.deltaTime / armTime);
        }
        //  振り終わった時
        else
        {
            nowAttack = false;
            move = false;
            arm.Rotate(new Vector3(0.0f, 0.0f, -120.0f));
            rb.velocity = Vector3.zero;
        }
    }
}
