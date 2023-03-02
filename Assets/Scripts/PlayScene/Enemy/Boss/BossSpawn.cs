using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossSpawnStatas
{
    [SerializeField, Label("種類")] BossID bossSpawnID;
    [SerializeField, Label("出現確立"), Range(0.0f, 1.0f)] float bossSpawnPer;

    public BossID GetBossID() { return bossSpawnID; }           //  出現種類受渡
    public float GetBossSpaenPer() { return bossSpawnPer; }     //  出現確率受渡
}

public class BossSpawn : MonoBehaviour
{
    //  ボスの種類を設定
    [SerializeField, Label("ボスの生成データ")] List<BossSpawnStatas> spawnStatas;

    //  生成されたか確認
    bool isSpawn = false;


    //  生成情報を渡す
    public List<BossSpawnStatas> GetBossSpawnStatas()
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
