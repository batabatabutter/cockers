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

public class ItemManager : MonoBehaviour
{
    //  所持アイテム数
    public List<int> itemNum;

    //  敵のオブジェクトを格納しておく
    [SerializeField] List<GameObject> Items;

    //  敵とどれくらい離れていたら動作を止めるか判断
    [SerializeField] float distance;

    //  配列か仮格納用
    GameObject[] HolderArray;
    //  敵の生成を格納
    public List<ItemSpawn> itemSpawns;
    //  敵を格納
    public List<GameObject> itemObjects;

    //  プレイヤーを格納しておく
    GameObject player;

    // Start is called before the first frame update
    private void Start()
    {
        //  アイテム数格納
        for (int i = 0; i < (int)ItemID.ItemNum; i++)
        {
            itemNum.Add(0);
        }

        //  アイテムのスポーンを格納
        HolderArray = GameObject.FindGameObjectsWithTag("ItemSpawn");
        //  Listに再格納
        foreach (GameObject obj in HolderArray)
        {
            if (obj.GetComponent<ItemSpawn>() == null) Debug.Log("Error : クラス ItemSpaen が見つかりませんでした。" + obj);
            itemSpawns.Add(obj.GetComponent<ItemSpawn>());
        }

        //  プレイヤーを格納しておく
        player = GameObject.FindWithTag("Player");
        if (player == null) Debug.Log("Error : プレイヤーがみつかりません");
    }

    // Update is called once per frame
    private void Update()
    {
        //  敵の生成更新
        foreach (ItemSpawn item in itemSpawns)
        {
            //  生成されていなければ
            if (!item.GetIsSpawn())
            {
                //  生成
                itemObjects.Add(Instantiate(Items[(int)item.GetItemID()], item.GetItemDefPos(), Quaternion.identity));
                item.SetIsSpawn(true);
            }
        }

        //  敵の更新
        foreach (GameObject item in itemObjects)
        {
            //  なかったらスキップ
            if (item == null) continue;

            //  敵とプレイヤーの位置関係比較
            if (Vector3.Distance(item.transform.position, player.transform.position) > distance)
            {
                //  遠かったら実行停止
                item.SetActive(false);
            }
            else
            {
                //  近づいたら実行
                item.SetActive(true);
            }
        }
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

    //  アイテム取得数受渡
    public int GetItemNum(ItemID ID)
    {
        return itemNum[(int)ID];
    }
}
