using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawn : MonoBehaviour
{
    //  �G�̎�ނ�ݒ�
    [SerializeField] EnemyID enemyID;
    //  ���W�i�[
    Vector3 enemyDefPos;
    //  �������ꂽ���m�F
    bool isSpawn = false;

    //  �ŏ��Ɏ��s
    private void Start()
    {
        //  �������W�i�[
        enemyDefPos = transform.position;
    }

    //   �������W��n
    public Vector3 GetEnemyDefPos()
    {
        return enemyDefPos;
    }

    //  ID��n��
    public EnemyID GetEnemyID()
    {
        return enemyID;
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
