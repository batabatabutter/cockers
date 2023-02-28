using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemySpawnStatas
{
    [SerializeField, Label("�G�̎��")] EnemyID enemySpawnID;
    [SerializeField, Label("�G�̏o���m��"), Range(0.0f, 1.0f)] float enemySpawnPer;

    public EnemyID GetEnemyID() { return enemySpawnID; }         //  �o����ގ�n
    public float GetEnemySpaenPer() { return enemySpawnPer; }    //  �o���m����n
}

public class EnemySpawn : MonoBehaviour
{
    //  �G�̐������
    [SerializeField, Label("�G�̐����f�[�^")] List<EnemySpawnStatas> spawnStatas;

    //  �������ꂽ���m�F
    bool isSpawn = false;

    //  ��������n��
    public List<EnemySpawnStatas> GetEnemySpawnStatas()
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
