using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemSpaenStatas
{
    [SerializeField, Label("種類")] ItemID itemSpawnID;
    [SerializeField, Label("出現確立"), Range(0.0f, 1.0f)] float itemSpawnPer;

    public ItemID GetItemID() { return itemSpawnID; }         //  出現種類受渡
    public float GetItemSpaenPer() { return itemSpawnPer; }    //  出現確率受渡
}

public class ItemSpawn : MonoBehaviour
{
    //  敵の種類を設定
    [SerializeField, Label("アイテムの生成データ")] List<ItemSpaenStatas> spawnStatas;

    //  生成されたか確認
    bool isSpawn = false;

    //  生成情報を渡す
    public List<ItemSpaenStatas> GetItemSpawnStatas()
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
