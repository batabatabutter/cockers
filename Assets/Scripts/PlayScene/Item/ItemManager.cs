using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  アイテムの名前
public enum ItemID
{
    [InspectorName("キャベツ")] 　Cabbage,
    [InspectorName("ニンジン")] 　Carrot,
    [InspectorName("豚肉")]     　Pork,
    [InspectorName("鶏肉")] 　　　Chicken,
    [InspectorName("牛肉")]       Beaf,
    [InspectorName("玉ねぎ")]   　Onion,
    [InspectorName("きゅうり")] 　Cucumber,
    [InspectorName("大豆")]       Been,
    [InspectorName("卵")] 　　　　Egg,
    [InspectorName("牛乳")]       Milk,
    [InspectorName("ジャガイモ")] Potato,
    [InspectorName("リンゴ")] 　　Apple,
    [InspectorName("イチゴ")] 　　Strawberry,



    [InspectorName("")] ItemNum
}

public class ItemManager : MonoBehaviour
{
    //  敵のオブジェクトを格納しておく
    [SerializeField, Label("アイテム")] List<GameObject> Items;

    //  敵とどれくらい離れていたら動作を止めるか判断
    [SerializeField, Label("描画距離")] float distance;

    //  モデル描画関係
    [SerializeField, Header("モデル描画関係"), Label("揺れ速度")] float modelSpeed;
    [SerializeField, Label("揺れ幅")] float modelRange;

    //  所持アイテム数
    [SerializeField, /*ReadOnly,*/ Label("アイテム数")] List<int> itemNum;

    //  配列か仮格納用
    GameObject[] HolderArray;
    //  敵の生成を格納
    [HideInInspector] public List<ItemSpawn> itemSpawns;
    //  敵を格納
    [HideInInspector] public List<GameObject> itemObjects;

    //  プレイヤーを格納しておく
    GameObject player;

    //  更新
    private void Update()
    {
        //  敵の生成更新
        foreach (ItemSpawn item in itemSpawns)
        {
            //  生成されていなければ
            if (!item.GetIsSpawn())
            {
                //  乱数取得
                float rand = Random.Range(0.0f, 1.0f);

                float totalPer = 0.0f;
                int ID = 0;

                //  生成
                foreach (ItemSpaenStatas statas in item.GetItemSpawnStatas())
                {
                    totalPer += statas.GetItemSpaenPer();
                    if (rand <= totalPer)
                    {
                        //  生成
                        GenerateItem(statas.GetItemID(), item.transform.position);
                        break;
                    }
                    ID++;
                }
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

    //  初期実行
    public void ItemStart()
    {
        //  アイテム数格納
        for (int i = 0; i < (int)ItemID.ItemNum; i++)
        {
            itemNum.Add(0);
        }
    }

    //  リセット
    public void ItemReset()
    {
        //  変数リセット
        HolderArray = null;
        itemSpawns.Clear();
        itemObjects.Clear();

        //  アイテムのスポーンを格納
        HolderArray = GameObject.FindGameObjectsWithTag("ItemSpawn");
        //  Listに再格納
        foreach (GameObject obj in HolderArray)
        {
            if (obj.GetComponent<ItemSpawn>() == null) Debug.Log("Error : クラス ItemSpaen が見つかりませんでした。" + obj);
            itemSpawns.Add(obj.GetComponent<ItemSpawn>());
        }

        //  プレイヤーを格納しておく
        player = GameObject.FindGameObjectWithTag("Player");
        if (player == null) Debug.Log("Error : プレイヤーがみつかりません");
    }

    //  アイテムオブジェクト受渡
    public GameObject GetItemObject(ItemID ID)
    {
        return Items[(int)ID];
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

    //  モデル移動速度受渡
    public float GetModelSpeed()
    {
        return modelSpeed;
    }

    //  モデル移動幅受渡
    public float GetModelRange()
    {
        return modelRange;
    }

    //  アイテム生成
    public void GenerateItem(ItemID ID, Vector3 pos)
    {
        GameObject item = Instantiate(Items[(int)ID], pos, Quaternion.identity);
        item.GetComponent<Item>().Initialize(this);
        itemObjects.Add(item);
    }
}
