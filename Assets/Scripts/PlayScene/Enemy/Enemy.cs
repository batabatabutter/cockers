using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

///////////////////////
/// 敵の継承元クラス///
///////////////////////

public class Enemy : MonoBehaviour
{
    //  ステータス格納用
    protected EnemyStatas statas;

    //  敵の種類を設定
    [SerializeField, HeaderAttribute("敵種類")] EnemyID enemyID;

    [SerializeField, HeaderAttribute("ステータス")] int hp;
    [SerializeField] int atk;

    //  ドロップアイテム
    [SerializeField] List<GameObject> dropItem;

    // Start is called before the first frame update
    void Start()
    {
        //  ステータス格納
        statas.HP = hp;
        statas.ATK = atk;
        statas.death = false;
        EnemyStart();
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

        //  敵更新
        EnemyUpdate();

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

        // Aキーが押された瞬間かどうか
        if (aKey.wasPressedThisFrame)
        {
            statas.HP -= 5;
            Debug.Log(statas.HP);
        }
    }

    //  敵の初期設定
    public virtual void EnemyStart()
    {

    }

    //  敵の更新
    public virtual void EnemyUpdate()
    {

    }

    //  ダメージ処理
    public void Damage(int dmg)
    {
        statas.HP -= dmg;
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
        foreach(GameObject obj in dropItem)
        {
            Instantiate(obj, gameObject.transform.position, Quaternion.identity);
        }
    }
}
