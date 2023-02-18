using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  アイテムの名前
public enum ItemID
{
    [InspectorName("キャベツ")] Cabbage,
    [InspectorName("ニンジン")] Carrot,
    [InspectorName("豚肉")] Pork,

    [InspectorName("")] ItemNum
}

public class ItemManger : MonoBehaviour
{
    //  所持アイテム数
    public List<int> itemNum;

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < (int)ItemID.ItemNum; i++)
        {
            itemNum.Add(0);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    //  アイテム取得処理
    public void GetItem(ItemID ID, int num)
    {
        itemNum[(int)ID] += num;
    }

    //  アイテム消費処理
    public bool SpendItem(ItemID ID, int num)
    {
        //  数が足りなかったら失敗
        if (itemNum[(int)ID] - num < 0) return false;
        itemNum[(int)ID] -= num;
        return true;
    }
}
