using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField, HeaderAttribute("アイテム種類")] ItemID itemID;

    ItemManager itemManager;

    //  モデル描画用スクリプト
    ItemModel itemModel;

    // Start is called before the first frame update
    void Start()
    {
        itemManager = GameObject.Find("ItemManager").GetComponent<ItemManager>();
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 200.0f, 0.0f));
        itemModel = transform.Find("Model").gameObject.GetComponent<ItemModel>();
        //  モデル描画関係設定
        itemModel.SetModelSpeed(itemManager.GetModelSpeed());
        itemModel.SetModelRange(itemManager.GetModelRange());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
    }

    private void OnTriggerEnter(Collider other)
    {
        //  プレイヤーが触れたら
        if (other.gameObject.tag == "Player")
        {
            itemManager.GetItem(itemID, 1);
            Destroy(gameObject);
        }
    }
}
