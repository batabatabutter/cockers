using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  敵の名前
public enum EnemyID
{
    [InspectorName("キャベツ太郎")]       CabbageTaro,
    [InspectorName("ニンジン槍")]         CarrotSpear,
    [InspectorName("豚兵士")]             PigSoldier,
    [InspectorName("玉根")]               Onionnu,
    [InspectorName("きゅうりミサイル")]   CucumberMissile,
    [InspectorName("アップルツリー")]     AppleTree,
    [InspectorName("サラディン")]         Saladhin,
    [InspectorName("チキチキバー")]       ChikiChiki,
    [InspectorName("ポテツ")]             Potetu,
    [InspectorName("ストロールベリー")]   Strawberry,
    [InspectorName("牛鬼")]               CowDemon,
    [InspectorName("ビーン")]             Beans,

    [InspectorName("")]                EnemyNum
}

//  ボスの名前
public enum BossID
{
    [InspectorName("岩塩マン")] SaltRockMan,
    [InspectorName("醤油殿下")] SoySaucePrince,
    [InspectorName("マヨラー")] Mayonnaiser,
    [InspectorName("味噌ノ桶")] MisoBucket,
    [InspectorName("角砂糖マン")] SugerCubesMan,

    [InspectorName("")] BossNum
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
    [SerializeField, Label("敵")] List<GameObject> Enemys;

    //  敵のオブジェクトを格納しておく
    [SerializeField, Label("ボス")] List<GameObject> Bosses;

    //  敵とどれくらい離れていたら動作を止めるか判断
    [SerializeField, Label("描画距離")] float distance;

    //  敵の無敵時間設定
    [SerializeField, HeaderAttribute("一律ステータス"), Label("無敵時間")] float invincibilityTime;
    //  重力設定
    [SerializeField, Label("重力")] float gravity;

    //  エフェクトを格納
    [SerializeField, Label("敵死亡エフェクト")] GameObject enemyDeathEffect; public GameObject GetEnemyDeathEffect() { return enemyDeathEffect; }

    //  ボス
    [SerializeField, Label("ボス死亡エフェクト")] GameObject bossDeathEffect; public GameObject GetBossDeathEffect() { return bossDeathEffect; }

    //  ボスヒットストップ
    [SerializeField, Label("ボス死亡ヒットストップ秒数")] float hitStopSec;
    [SerializeField, Label("ボス死亡ヒットストップ倍率")] float hitStopRate;
    private bool nowHitStop = false;
    private float hitStopTimer = 0.0f;

    //  配列か仮格納用
    GameObject[] HolderArray;
    //  敵の生成を格納
    [HideInInspector] public List<EnemySpawn> enemySpawns;
    //  敵を格納
    [HideInInspector] public List<GameObject> enemyObjects;

    //  敵の生成を格納
    [HideInInspector] public List<BossSpawn> bossSpawns;
    //  敵を格納
    [HideInInspector] public List<GameObject> bossObjects;

    //  プレイヤーを格納しておく
    GameObject player;

    //  更新
    private void Update()
    {
        //  ヒットストップ計算
        if (nowHitStop)
        {
            hitStopTimer -= Time.deltaTime / hitStopRate;
            if (hitStopTimer <= 0.0f)
            {
                nowHitStop = false;
                Time.timeScale = 1.0f;
            }
        }

        //  敵の生成更新
        foreach (EnemySpawn enemy in enemySpawns)
        {
            //  生成されていなければ
            if (!enemy.GetIsSpawn())
            {
                //  乱数取得
                float rand = Random.Range(0.0f, 1.0f);

                float totalPer = 0.0f;
                int ID = 0;

                //  生成
                foreach (EnemySpawnStatas statas in enemy.GetEnemySpawnStatas())
                {
                    totalPer += statas.GetEnemySpaenPer();
                    if (rand <= totalPer)
                    {
                        GenerateEnemy(statas.GetEnemyID(), enemy.transform.position);
                        break;
                    }
                    ID++;
                }
                enemy.SetIsSpawn(true);
            }
        }

        //  ボスの生成更新
        foreach (BossSpawn boss in bossSpawns)
        {
            //  生成されていなければ
            if (!boss.GetIsSpawn())
            {
                //  乱数取得
                float rand = Random.Range(0.0f, 1.0f);

                float totalPer = 0.0f;
                int ID = 0;

                //  生成
                foreach (BossSpawnStatas statas in boss.GetBossSpawnStatas())
                {
                    totalPer += statas.GetBossSpaenPer();
                    if (rand <= totalPer)
                    {
                        GenerateBoss(statas.GetBossID(), boss.transform.position);
                        break;
                    }
                    ID++;
                }
                boss.SetIsSpawn(true);
            }
        }

        //  敵の更新
        foreach (GameObject enemy in enemyObjects)
        {
            //  なかったらスキップ
            if (enemy == null) continue;

            //  敵とプレイヤーの位置関係比較
            if (Vector3.Distance(enemy.transform.position, player.transform.position) > distance)
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

        //  ボスの更新
        foreach (GameObject boss in bossObjects)
        {
            //  なかったらスキップ
            if (boss == null) continue;

            //  敵とプレイヤーの位置関係比較
            if (Vector3.Distance(boss.transform.position, player.transform.position) > distance)
            {
                //  遠かったら実行停止
                boss.SetActive(false);
            }
            else
            {
                //  近づいたら実行
                boss.SetActive(true);
            }
        }
    }

    //  リセット
    public void EnemyReset()
    {
        //  変数リセット
        HolderArray = null;
        enemySpawns.Clear();
        bossSpawns.Clear();
        foreach (GameObject enemy in enemyObjects)
        {
            Destroy(enemy);
        }
        foreach (GameObject boss in bossObjects)
        {
            Destroy(boss);
        }
        enemyObjects.Clear();
        bossObjects.Clear();

        //  敵のスポーンを格納
        HolderArray = GameObject.FindGameObjectsWithTag("EnemySpawn");
        //  Listに再格納
        foreach (GameObject obj in HolderArray)
        {
            if (obj.GetComponent<EnemySpawn>() == null) Debug.Log("Error : クラス EnemySpaen が見つかりませんでした。" + obj);
            enemySpawns.Add(obj.GetComponent<EnemySpawn>());
        }

        //  ボスのスポーンを格納
        HolderArray = GameObject.FindGameObjectsWithTag("BossSpawn");
        //  Listに再格納
        foreach (GameObject obj in HolderArray)
        {
            if (obj.GetComponent<BossSpawn>() == null) Debug.Log("Error : クラス BossSpaen が見つかりませんでした。" + obj);
            bossSpawns.Add(obj.GetComponent<BossSpawn>());
        }

        //  プレイヤーを格納しておく
        player = GameObject.FindWithTag("Player");
        if (player == null) Debug.Log("Error : プレイヤーがみつかりません");
    }


    //  無敵時間取得
    public float GetInvincibilityTime()
    {
        return invincibilityTime;
    }

    //  重力取得
    public float GetGravity()
    {
        return gravity;
    }

    //  プレイヤー取得
    public GameObject GetPlayer()
    {
        return player;
    }

    //  敵生成
    public void GenerateEnemy(EnemyID ID, Vector3 pos)
    {
        GameObject enemy = Instantiate(Enemys[(int)ID], pos, Quaternion.identity);
        enemyObjects.Add(enemy);
    }

    //  敵生成返り値付き
    public GameObject GenerateEnemyReturn(EnemyID ID, Vector3 pos)
    {
        GameObject enemy = Instantiate(Enemys[(int)ID], pos, Quaternion.identity);
        enemyObjects.Add(enemy);
        return enemy;
    }

    //  ボス生成
    public void GenerateBoss(BossID ID, Vector3 pos)
    {
        GameObject boss = Instantiate(Bosses[(int)ID], pos, Quaternion.identity);
        enemyObjects.Add(boss);
    }

    //  ヒットストップ生成
    public void HitStop()
    {
        nowHitStop = true;
        Time.timeScale = hitStopRate;
        hitStopTimer = hitStopSec;
    }
}
