using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Onion : Enemy
{
    //  �e�̑��x
    [SerializeField, HeaderAttribute("��")] private float bulletSpeed;
    //  �e�̃I�u�W�F�N�g
    [SerializeField] private GameObject bulletPrefab;
    //  ���̃I�u�W�F�N�g
    [SerializeField] private GameObject head;
    //  ���^�C�~���O
    [SerializeField] private float coolTime;
    //  �^�C�}�[
    private float timer;

    //  �ŏ��Ɏ��s
    public override void EnemyStart()
    {
        timer = coolTime;
    }

    //  �X�V
    public override void EnemyUpdate()
    {
        if (timer > 0.0f)
        {
            //  ���Ԃ����炷
            timer -= Time.deltaTime;
            return;
        }

        //  �N�[���^�C����ݒ�
        timer = coolTime;

        //  �����ʒu���Z�o
        Vector3 createPos = head.transform.position + gameObject.transform.forward;
        //  ���˕������Z�o
        Vector3 bulletDir = gameObject.transform.forward * bulletSpeed;

        //  �e�𐶐�
        GameObject bullet = Instantiate(bulletPrefab, createPos, Quaternion.identity);
        //  �e�̔��˕�����ݒ�
        bullet.GetComponent<Bullet>().Shot(bulletDir, statas.ATK);
    }
}
