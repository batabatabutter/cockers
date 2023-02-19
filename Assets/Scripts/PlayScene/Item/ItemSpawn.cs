using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    //  敵の種類を設定
    [SerializeField] ItemID itemID;
    //  座標格納
    Vector3 itemDefPos;
    //  生成されたか確認
    bool isSpawn = false;

    //  最初に実行
    private void Start()
    {
        //  初期座標格納
        itemDefPos = transform.position;
    }

    //   初期座標受渡
    public Vector3 GetItemDefPos()
    {
        return itemDefPos;
    }

    //  IDを渡す
    public ItemID GetItemID()
    {
        return itemID;
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
