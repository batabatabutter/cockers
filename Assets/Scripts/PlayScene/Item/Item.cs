using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField, Label("�A�C�e�����")] ItemID itemID;

    ItemManager itemManager;

    //  ���f���`��p�X�N���v�g
    ItemModel itemModel;

    //  �����蔻��
    private void OnTriggerEnter(Collider other)
    {
        //  �v���C���[���G�ꂽ��
        if (other.gameObject.CompareTag("Player"))
        {
            itemManager.GetItem(itemID, 1);
            Destroy(gameObject);
        }
    }

    public void Initialize(ItemManager itemManager)
    {
        this.itemManager = itemManager;

        //  ��ɔ�΂�
        gameObject.GetComponent<Rigidbody>().AddForce(new Vector3(0.0f, 200.0f, 0.0f));

        //  �q���擾
        itemModel = transform.Find("Model").gameObject.GetComponent<ItemModel>();
        //  ���f���`��֌W�ݒ�
        itemModel.SetModelSpeed(itemManager.GetModelSpeed());
        itemModel.SetModelRange(itemManager.GetModelRange());
    }
}
