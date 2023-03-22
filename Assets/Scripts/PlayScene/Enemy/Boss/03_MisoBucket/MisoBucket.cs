using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisoBucket : Boss
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

    //  クールタイム
    [SerializeField] private float coolTime;
    //  クールタイマー
    private float coolTimer;

    //  攻撃範囲
    [SerializeField] private float attackDistance;

    //  物理制御
    private Rigidbody rd;

    //  ジャンプ力
    [SerializeField] private float jumpForce;

    //  目標地点
    private Vector3 targetPos;

    //  初期処理
    public override void BossStart()
    {
        attackType = AttackType.OverID;

        attack = Attack.OverID;

        coolTimer = 0f;

        rd = gameObject.GetComponent<Rigidbody>();

        targetPos = Vector3.zero;
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
        else if (attackType == AttackType.OverID)
        {
            //  攻撃の種類を設定する
            //attackType = (AttackType)Random.Range(0, (int)AttackType.OverID);
            attackType = AttackType.Attack01;
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
    //  攻撃１
    private bool Attack01()
    {
        
        //  攻撃終了
        return true;
    }
    //  攻撃２
    private bool Attack02()
    {
        //  攻撃未終了
        return false;

        //  攻撃終了
        return true;
    }
    //  攻撃３
    private bool Attack03()
    {
        //  攻撃未終了
        return false;

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
    }
}
