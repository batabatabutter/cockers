using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BossSpawnStatas
{
    [SerializeField, Label("�{�X�̎��")] BossID bossSpawnID;
    [SerializeField, Label("�{�X�̏o���m��"), Range(0.0f, 1.0f)] float bossSpawnPer;

    public BossID GetBossID() { return bossSpawnID; }           //  �o����ގ�n
    public float GetBossSpaenPer() { return bossSpawnPer; }     //  �o���m����n
}

public class BossSpawn : MonoBehaviour
{
    //  �{�X�̎�ނ�ݒ�
    [SerializeField, Label("�G�̐����f�[�^")] List<BossSpawnStatas> spawnStatas;

    //  �������ꂽ���m�F
    bool isSpawn = false;


    //  ��������n��
    public List<BossSpawnStatas> GetBossSpawnStatas()
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
