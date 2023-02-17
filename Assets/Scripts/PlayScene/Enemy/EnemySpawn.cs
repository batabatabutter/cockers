using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //  敵の種類を設定
    [SerializeField] EnemyID enemyID;
    //  座標格納
    Vector3 enemyDefPos;
    //  生成されたか確認
    bool isSpawn = false;

    //  最初に実行
    private void Start()
    {
        //  初期座標格納
        enemyDefPos = transform.position;
    }

    //   初期座標受渡
    public Vector3 GetEnemyDefPos()
    {
        return enemyDefPos;
    }

    //  IDを渡す
    public EnemyID GetEnemyID()
    {
        return enemyID;
    }

    //  生成状況受渡
    public bool GetIsSpawn()
    {
        return isSpawn;
    }

    //  生成状況受け取り
    public void SetIsSpawn(bool isSpawn)
    {
        this.isSpawn = isSpawn;
    }
}
