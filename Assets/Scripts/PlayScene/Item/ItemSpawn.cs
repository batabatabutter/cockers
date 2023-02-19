using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    //  “G‚Ìí—Ş‚ğİ’è
    [SerializeField] ItemID itemID;
    //  À•WŠi”[
    Vector3 itemDefPos;
    //  ¶¬‚³‚ê‚½‚©Šm”F
    bool isSpawn = false;

    //  Å‰‚ÉÀs
    private void Start()
    {
        //  ‰ŠúÀ•WŠi”[
        itemDefPos = transform.position;
    }

    //   ‰ŠúÀ•Wó“n
    public Vector3 GetItemDefPos()
    {
        return itemDefPos;
    }

    //  ID‚ğ“n‚·
    public ItemID GetItemID()
    {
        return itemID;
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
