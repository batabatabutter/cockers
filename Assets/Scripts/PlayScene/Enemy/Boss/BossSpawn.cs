using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSpawn : MonoBehaviour
{
    //  �G�̎�ނ�ݒ�
    [SerializeField] BossID bossID;
    //  ���W�i�[
    Vector3 bossDefPos;
    //  �������ꂽ���m�F
    bool isSpawn = false;

    //  �ŏ��Ɏ��s
    private void Start()
    {
        //  �������W�i�[
        bossDefPos = transform.position;
    }

    //   �������W��n
    public Vector3 GetEnemyDefPos()
    {
        return bossDefPos;
    }

    //  ID��n��
    public BossID GetBossID()
    {
        return bossID;
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
