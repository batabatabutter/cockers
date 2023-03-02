using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemSpaenStatas
{
    [SerializeField, Label("���")] ItemID itemSpawnID;
    [SerializeField, Label("�o���m��"), Range(0.0f, 1.0f)] float itemSpawnPer;

    public ItemID GetItemID() { return itemSpawnID; }         //  �o����ގ�n
    public float GetItemSpaenPer() { return itemSpawnPer; }    //  �o���m����n
}

public class ItemSpawn : MonoBehaviour
{
    //  �G�̎�ނ�ݒ�
    [SerializeField, Label("�A�C�e���̐����f�[�^")] List<ItemSpaenStatas> spawnStatas;

    //  �������ꂽ���m�F
    bool isSpawn = false;

    //  ��������n��
    public List<ItemSpaenStatas> GetItemSpawnStatas()
    {
        return spawnStatas;
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
