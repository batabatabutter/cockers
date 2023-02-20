using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  敵の名前
public enum EnemyID
{
    [InspectorName("キャベツ太郎")]    CabbageTaro,
    [InspectorName("ニンジン槍")]      CarrotSpear,
    [InspectorName("豚兵士")]          PigSoldier,

    [InspectorName("")]                EnemyNum
}

//  敵のステータス
public struct EnemyStatas
{
    public int HP;
    public int ATK;
    public bool death;
}

public class EnemyManager : MonoBehaviour
{
    //  敵のオブジェクトを格納しておく
    [SerializeField] List<GameObject> Enemys;

    //  敵とどれくらい離れていたら動作を止めるか判断
    [SerializeField] float distance;

    //  敵の無敵時間設定
    [SerializeField, HeaderAttribute("一律ステータス")] float invincibilityTime;

    //  配列か仮格納用
    GameObject[] HolderArray;
    //  敵の生成を格納
    public List<EnemySpawn> enemySpawns;
    //  敵を格納
    public List<GameObject> enemyObjects;

    //  プレイヤーを格納しておく
    GameObject player;

    //  最初に実行される
    private void Start()
    {
        //  敵のスポーンを格納
        HolderArray = GameObject.FindGameObjectsWithTag("EnemySpawn");
        //  Listに再格納
        foreach (GameObject obj in HolderArray)
        {
            if (obj.GetComponent<EnemySpawn>() == null) Debug.Log("Error : クラス EnemySpaen が見つかりませんでした。" + obj);
            enemySpawns.Add(obj.GetComponent<EnemySpawn>());
        }

        //  プレイヤーを格納しておく
        player = GameObject.FindWithTag("Player");
        if (player == null) Debug.Log("Error : プレイヤーがみつかりません");
    }

    //  更新
    private void Update()
    {
        //  敵の生成更新
        foreach (EnemySpawn enemy in enemySpawns)
        {
            //  生成されていなければ
            if (!enemy.GetIsSpawn())
            {
                //  生成
                enemyObjects.Add(Instantiate(Enemys[(int)enemy.GetEnemyID()], enemy.GetEnemyDefPos(), Quaternion.identity));
                enemy.SetIsSpawn(true);
            }
        }

        //  敵の更新
        foreach(GameObject enemy in enemyObjects)
        {
            //  なかったらスキップ
            if (enemy == null) continue;

            //  敵とプレイヤーの位置関係比較
            if(Vector3.Distance(enemy.transform.position,player.transform.position) > distance)
            {
                //  遠かったら実行停止
                enemy.SetActive(false);
            }
            else
            {
                //  近づいたら実行
                enemy.SetActive(true);
            }
        }
    }

    //  無敵時間取得
    public float GetInvincibilityTime()
    {
        return invincibilityTime;
    }
}
