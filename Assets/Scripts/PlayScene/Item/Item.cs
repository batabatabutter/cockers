using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField, HeaderAttribute("アイテム種類")] ItemID itemID;

    ItemManger itemManger;

    // Start is called before the first frame update
    void Start()
    {
        itemManger = GameObject.Find("ItemManager").GetComponent<ItemManger>();
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 10.0f, 0.0f));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision other)
    {
        //  プレイヤーが触れたら
        if (other.gameObject.tag == "Player")
        {
            itemManger.GetItem(itemID, 1);
            Destroy(gameObject);
        }
    }
}
