using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnStatas
{
    [SerializeField, Label("“G‚Ìí—Ş")] EnemyID enemySpawnID;
    [SerializeField, Label("“G‚ÌoŒ»Šm—§"), Range(0.0f, 1.0f)] float enemySpawnPer;

    public EnemyID GetEnemyID() { return enemySpawnID; }         //  oŒ»í—Şó“n
    public float GetEnemySpaenPer() { return enemySpawnPer; }    //  oŒ»Šm—¦ó“n
}

public class EnemySpawn : MonoBehaviour
{
    //  “G‚Ì¶¬î•ñ
    [SerializeField, Label("“G‚Ì¶¬ƒf[ƒ^")] List<EnemySpawnStatas> spawnStatas;

    //  ¶¬‚³‚ê‚½‚©Šm”F
    bool isSpawn = false;

    //  ¶¬î•ñ‚ğ“n‚·
    public List<EnemySpawnStatas> GetEnemySpawnStatas()
    {
        return spawnStatas;
    }

    //  ¶¬ó‹µó“n
    public bool GetIsSpawn()
    {
        return isSpawn;
    }

    //  ¶¬ó‹µó‚¯æ‚è
    public void SetIsSpawn(bool isSpawn)
    {
        this.isSpawn = isSpawn;
    }
}
