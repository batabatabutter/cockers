using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    //  “G‚Ìí—Ş‚ğİ’è
    [SerializeField] BossID bossID;
    //  À•WŠi”[
    Vector3 bossDefPos;
    //  ¶¬‚³‚ê‚½‚©Šm”F
    bool isSpawn = false;

    //  Å‰‚ÉÀs
    private void Start()
    {
        //  ‰ŠúÀ•WŠi”[
        bossDefPos = transform.position;
    }

    //   ‰ŠúÀ•Wó“n
    public Vector3 GetEnemyDefPos()
    {
        return bossDefPos;
    }

    //  ID‚ğ“n‚·
    public BossID GetBossID()
    {
        return bossID;
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
