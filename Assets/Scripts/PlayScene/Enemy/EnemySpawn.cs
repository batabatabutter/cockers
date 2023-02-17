using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //  “G‚Ìí—Ş‚ğİ’è
    [SerializeField] EnemyID enemyID;
    //  À•WŠi”[
    Vector3 enemyDefPos;
    //  ¶¬‚³‚ê‚½‚©Šm”F
    bool isSpawn = false;

    //  Å‰‚ÉÀs
    private void Start()
    {
        //  ‰ŠúÀ•WŠi”[
        enemyDefPos = transform.position;
    }

    //   ‰ŠúÀ•Wó“n
    public Vector3 GetEnemyDefPos()
    {
        return enemyDefPos;
    }

    //  ID‚ğ“n‚·
    public EnemyID GetEnemyID()
    {
        return enemyID;
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
