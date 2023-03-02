using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnStatas
{
    [SerializeField, Label("種類")] EnemyID enemySpawnID;
    [SerializeField, Label("出現確立"), Range(0.0f, 1.0f)] float enemySpawnPer;

    public EnemyID GetEnemyID() { return enemySpawnID; }         //  出現種類受渡
    public float GetEnemySpaenPer() { return enemySpawnPer; }    //  出現確率受渡
}

public class EnemySpawn : MonoBehaviour
{
    //  敵の生成情報
    [SerializeField, Label("敵の生成データ")] List<EnemySpawnStatas> spawnStatas;

    //  生成されたか確認
    bool isSpawn = false;

    //  生成情報を渡す
    public List<EnemySpawnStatas> GetEnemySpawnStatas()
    {
        return spawnStatas;
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
