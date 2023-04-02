using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisonoOke : Boss
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
    [SerializeField, ReadOnly] private float coolTimer;

    //  攻撃範囲
    [SerializeField] private float attackDistance;

    //  物理制御
    private Rigidbody rd;

    //  ジャンプ力
    [SerializeField] private float jumpForce;

    //  目標地点
    private Vector3 targetPos;

    //  変化状態
    public enum MisoStatas
    {
        Mix,
        Red,
        White,

        MisoNum
    }
    private MisoStatas misoStatas;

    [SerializeField, Header("ミックス味噌時"), Label("攻撃力倍率")] float mixAtkRate;
    [SerializeField, Label("攻撃速度倍率")] float mixSpdRate;

    [SerializeField, Header("赤味噌時"), Label("攻撃力倍率")] float redAtkRate;
    [SerializeField, Label("攻撃速度倍率")] float redSpdRate;

    [SerializeField, Header("白味噌時"), Label("攻撃力倍率")] float whiteAtkRate;
    [SerializeField, Label("攻撃速度倍率")] float whiteSpdRate;

    private float atkRate;
    private float spdRate;

    //  弾1
    [SerializeField, Header("味噌弾"), Label("オブジェクト")] GameObject bullet1Obj;
    [SerializeField, Label("ダメージ")] int bullet1Dmg;
    [SerializeField, Label("速度")] float bullet1Speed;

    //  弾2
    [SerializeField, Header("味噌弾(味噌床)"), Label("オブジェクト")] GameObject bullet2Obj;
    [SerializeField, Label("ダメージ")] int bullet2Dmg;
    [SerializeField, Label("速度")] float bullet2Speed;

    //  初期処理
    public override void BossStart()
    {
        attackType = AttackType.OverID;

        attack = Attack.OverID;

        coolTimer = 0f;

        rd = gameObject.GetComponent<Rigidbody>();

        targetPos = Vector3.zero;

        misoStatas = MisoStatas.Mix;
    }

    //  更新処理
    public override void BossUpdate()
    {
        switch (misoStatas)
        {
            case MisoStatas.Mix:
                atkRate = mixAtkRate;
                spdRate = mixSpdRate;
                break;
            case MisoStatas.Red:
                atkRate = redAtkRate;
                spdRate = redSpdRate;
                break;
            case MisoStatas.White:
                atkRate = whiteAtkRate;
                spdRate = whiteSpdRate;
                break;
        }

        //  クールタイムが存在している
        if (coolTimer > 0f)
        {
            //  クール時間減らす
            coolTimer -= Time.deltaTime * spdRate;
            //  処理終了
            return;
        }

        //  プレイヤーが攻撃範囲内にいない・攻撃状態ではない
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) > attackDistance &&
            attackType == AttackType.OverID)
        {
            //  プレイヤーに向かって移動する
            Vector3 pToB = player.transform.position - gameObject.transform.position;
            //  向き
            rb.velocity = pToB.normalized;
            //  処理終了
            return;
        }
        //  プレイヤーが攻撃範囲内にいる・攻撃状態ではない
        else if (attackType == AttackType.OverID)
        {
            //  攻撃の種類を設定する
            attackType = (AttackType)Random.Range(0, (int)AttackType.OverID);
            //  攻撃手順を１へ
            attack = Attack.Move01;
            rb.velocity = Vector3.zero;
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
    //  攻撃１(味噌切り替え)
    private bool Attack01()
    {
        Debug.Log(misoStatas);
        //  味噌切り替え
        misoStatas = (MisoStatas)Random.Range(0, (int)MisoStatas.MisoNum);

        //  攻撃終了
        return true;
    }
    //  攻撃２(味噌弾1発射)
    private bool Attack02()
    {
        Debug.Log(misoStatas + "2");
        ////  攻撃未終了
        //return false;

        //  弾生成
        GameObject obj = Instantiate(bullet1Obj, transform.position, Quaternion.identity);
        obj.GetComponent<MisoBullet>().SetBulletStatas((int)(bullet1Dmg * atkRate), bullet1Speed, -Vector3.Normalize(obj.transform.position - player.transform.position));

        //  攻撃終了
        return true;
    }
    //  攻撃３(味噌弾2発射)
    private bool Attack03()
    {
        Debug.Log(misoStatas + "3");
        ////  攻撃未終了
        //return false;

        //  弾生成
        GameObject obj = Instantiate(bullet2Obj, transform.position, Quaternion.identity);
        obj.GetComponent<MisoBulletCreateFloor>().SetBulletStatas((int)(bullet2Dmg * atkRate), bullet2Speed, (obj.transform.position - player.transform.position));

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
