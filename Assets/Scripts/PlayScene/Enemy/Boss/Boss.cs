using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

///////////////////////
/// ボスの継承元クラス///
///////////////////////

public class Boss : MonoBehaviour
{
    //  ステータス格納用
    protected EnemyStatas statas;

    //  無敵時間
    protected float invincibilityTime;

    //  プレイヤー
    protected GameObject player;

    //  物理移動
    protected Vector3 vec;

    //  物理制御
    protected Rigidbody rb;

    //  攻撃フラグ
    protected bool nowAttack;

    //  敵の種類を設定
    [SerializeField, Label("ボス種類")] BossID bossID;

    [SerializeField, HeaderAttribute("ステータス")] int hp;
    [SerializeField] int atk;

    //  ドロップアイテム
    [SerializeField] List<ItemID> dropItem;
    [SerializeField] List<int> dropItemNum;

    ItemManager itemManager;
    EnemyManager enemyManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        itemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        nowAttack = false;
        rb = gameObject.GetComponent<Rigidbody>();
        //  ステータス格納
        statas.HP = hp;
        statas.ATK = atk;
        statas.death = false;
        vec = Vector3.zero;
        BossStart();
    }

    // Update is called once per frame
    void Update()
    {
        //  死んでいるなら戻す
        if (statas.death) return;

        //  体力が0ならDeath!！！
        if (statas.HP <= 0)
        {
            Death();
        }

        //  無敵時間更新
        invincibilityTime -= Time.deltaTime;
        if (invincibilityTime < 0.0f) invincibilityTime = 0.0f;

        //  物理制御
        rb.MovePosition(rb.position + (Vector3.down * Time.deltaTime * enemyManager.GetGravity()));

        //  敵更新
        BossUpdate();

        ///////////////////////////////////////////////////////////////////////////

        //  デバッグ用
        // 現在のキーボード情報
        var current = Keyboard.current;

        // キーボード接続チェック
        if (current == null)
        {
            // キーボードが接続されていないと
            // Keyboard.currentがnullになる
            Debug.Log("キーボードなし");
            return;
        }

        // Aキーの入力状態取得
        var aKey = current.aKey;
        var bKey = current.bKey;

        // Aキーが押された瞬間かどうか
        if (aKey.wasPressedThisFrame)
        {
            Damage(5);
        }
        if(bKey.wasPressedThisFrame)
        {
            rb.velocity = Vector3.zero;
        }
    }

    //  当たり判定
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && nowAttack)
        {
            collision.gameObject.GetComponent<PlayerStatus>().Damage(statas.ATK);
        }
        rb.velocity = Vector3.zero;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //rb.velocity = Vector3.zero;
    }

    //  敵の初期設定
    public virtual void BossStart()
    {

    }

    //  敵の更新
    public virtual void BossUpdate()
    {

    }

    //  ダメージ処理
    public void Damage(int dmg)
    {
        if (invincibilityTime > 0.0f) return;
        statas.HP -= dmg;
        invincibilityTime = enemyManager.GetInvincibilityTime();
    }

    //  死んでいる確認
    public bool GetIsDath()
    {
        return statas.death;
    }

    //  死んだ処理
    public virtual void Death()
    {
        statas.death = true;
        Destroy(gameObject, 0.3f);
        
        //  アイテム生成
        for (int i = 0; i < dropItem.Count; i++)
        {
            for (int j = 0; j < dropItemNum[i]; j++)
            {
                Instantiate(itemManager.GetItemObject(dropItem[i]), gameObject.transform.position, Quaternion.identity);
            }
        }
    }
}
