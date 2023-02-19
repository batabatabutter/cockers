using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSpawn : MonoBehaviour
{
    //  �G�̎�ނ�ݒ�
    [SerializeField] ItemID itemID;
    //  ���W�i�[
    Vector3 itemDefPos;
    //  �������ꂽ���m�F
    bool isSpawn = false;

    //  �ŏ��Ɏ��s
    private void Start()
    {
        //  �������W�i�[
        itemDefPos = transform.position;
    }

    //   �������W��n
    public Vector3 GetItemDefPos()
    {
        return itemDefPos;
    }

    //  ID��n��
    public ItemID GetItemID()
    {
        return itemID;
    }

    //  �����󋵎�n
    public bool GetIsSpawn()
    {
        return isSpawn;
    }

    //  �����󋵎󂯎��
    public void SetIsSpawn(bool isSpawn)
    {
        this.isSpawn = isSpawn;
    }
}
