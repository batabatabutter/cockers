using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mayonnaise : Boss
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

    //  発射するマヨネーズ
    [SerializeField] private GameObject mayoPrefab;

    //  両肩
    [SerializeField] private List<GameObject> shoulders;
    //  両手
    [SerializeField] private List<GameObject> hands;
    //  頭
    [SerializeField] private GameObject head;

    //  クールタイム
    [SerializeField] private float coolTime;
    //  クールタイマー
    private float coolTimer;

    //  攻撃範囲
    [SerializeField] private float attackDistance;
    //  ジャンプ力
    [SerializeField] private float jumpForce;
    //  ジャンプしたか
    private bool jump;
    //  目標地点
    private Vector3 targetPos;

    //  カウンター
    private int counter;
    //  タイマー
    private float timer;

    //  発射の速度
    [SerializeField] private float mayoSpeed;

    //  最初の角度
    private Vector3 startRot;
    //  目標の角度
    private Vector3 targetRot;

    //  サイズ
    private float scaY;
    //  大きさ変更用
    private bool size01;
    private bool size02;

    [SerializeField] private float speed;

    //  初期処理
    public override void BossStart()
    {
        attackType = AttackType.OverID;

        attack = Attack.OverID;

        coolTimer = 0f;

        rb = gameObject.GetComponent<Rigidbody>();

        targetPos = Vector3.zero;

        jump = false;

        counter = 0;
        timer = 0f;

        startRot = Vector3.zero;
        targetRot = Vector3.zero;

        scaY = 1f;
        size01 = false;
        size02 = false;
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
            Vector3 pToB = player.transform.position - gameObject.transform.position;
            //  向き
            rb.velocity = pToB.normalized * speed;
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
    //  攻撃１
    private bool Attack01()
    {
        switch (attack)
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
                if (rb.velocity.y > 0f) attack++;
                return false;

            //  重力を切る/空中で静止
            case Attack.Move02:
                //  落ち始めている
                if (rb.velocity.y <= 0f)
                {
                    //  重力を切る
                    rb.useGravity = false;
                    //  静止させる
                    rb.velocity = Vector3.zero;
                    //  静止位置を設定
                    targetPos = gameObject.transform.position;
                    //  肩の角度を記録しておく
                    targetRot = shoulders[1].transform.localEulerAngles;
                    //  余白の時間を作る
                    timer = 1f;
                    //  次のステップへ
                    attack++;
                }
                return false;

            //  空中からマヨビーム
            case Attack.Move03:
                timer -= Time.deltaTime;

                //  静止させる
                rb.velocity = Vector3.zero;
                //  停止位置で固定する
                gameObject.transform.position = targetPos;

                //  プレイヤーと肩の角度
                float rad = Mathf.Atan2(player.transform.position.y - shoulders[1].transform.position.y,
                    player.transform.position.x - shoulders[1].transform.position.x);
                //  肩の角度を変える
                shoulders[1].transform.localEulerAngles = new Vector3(rad * Mathf.Rad2Deg + 90f, 0f, 0f);
                
                if(timer <= 0f)
                {
                    //  飛ばす方向
                    Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0.0f).normalized;

                    //  手からマヨネーズを飛ばす
                    CreateMayo(hands[1].transform.position, dir * mayoSpeed, false);

                    //  カウンターを進める
                    counter++;
                }

                //  全て発射したら戻す
                if (counter > 120)
                {
                    //  次のステップへ
                    attack++;
                    //  重力を付ける
                    rb.useGravity = true;
                    //  タイマーの初期化
                    timer = 0f;
                    //  最初の肩の角度
                    startRot = shoulders[1].transform.localEulerAngles;
                }
                return false;

            case Attack.Move04:
                //  時間を進める
                timer += Time.deltaTime;
                //  一秒かけて戻す
                Vector3 rot = Vector3.Lerp(startRot, targetRot, timer);
                //  手を戻す
                shoulders[1].transform.localEulerAngles = rot;

                //  一秒後に攻撃終了
                if (timer >= 1f) return true;
                return false;
        }
        //  攻撃終了
        return true;
    }
    //  攻撃２
    private bool Attack02()
    {
        ////  攻撃未終了
        //return false;

        //  攻撃終了
        return true;
    }
    //  攻撃３(頭からマヨ飛ばす)
    private bool Attack03()
    {
        switch(attack)
        {
            //  上下に揺れる
            case Attack.Move01:
                //  サイズ更新
                gameObject.transform.localScale = new Vector3(1f, scaY, 1f);
                //  サイズ変更
                if (!size01)
                {
                    //  小さくする
                    scaY -= Time.deltaTime;
                    if (scaY < 0.75f) size01 = true;
                }
                else if(!size02)
                {
                    //  大きくする
                    scaY += Time.deltaTime;
                    if (scaY > 1.25f) size02 = true;
                }
                else
                {
                    //  小さくする
                    scaY -= Time.deltaTime;
                    if(scaY < 1f)
                    {
                        //  サイズを元に戻す
                        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                        //  次のステップへ
                        attack++;
                    }
                }
                return false;

            //  頭から発射
            case Attack.Move02:
                //  弾を発射（３発）
                for (int i = 0; i < 3; i++)
                {
                    //  ｘ軸方向をランダムで取得
                    float x = Random.Range(-1f, 1f);
                    //  発射方向
                    Vector3 di = new Vector3(x, 0.5f, 0f).normalized;
                    //  生成
                    CreateMayo(head.transform.position, di * mayoSpeed, true);
                }
                //  攻撃終了
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

        //  各関数を初期化
        jump = false;
        counter = 0;
        timer = 0f;
        startRot = Vector3.zero;
        targetRot = Vector3.zero;
        scaY = 1f;
        size01 = false;
        size02 = false;
    }
    //  マヨネーズを作る
    private void CreateMayo(Vector3 pos, Vector3 dir, bool useGrabity)
    {
        //  生成する
        GameObject mayo = Instantiate(mayoPrefab, pos, Quaternion.identity);
        //  飛ばす
        mayo.GetComponent<Rigidbody>().AddForce(dir);
        //  重力のON・OFF
        mayo.GetComponent<Rigidbody>().useGravity = useGrabity;
    }
}
