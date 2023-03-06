using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chicken : Enemy
{
    //  �e�̃I�u�W�F�N�g
    [SerializeField, HeaderAttribute("��")] private GameObject bulletPrefab;
    //  �e�̕���
    [SerializeField] private float bulletSpeed;
    //  ���̃I�u�W�F�N�g
    [SerializeField] private GameObject head;
    //  ���^�C�~���O
    [SerializeField] private float coolTime;
    //  �^�C�}�[
    private float timer;

    //  �v���C���[�̈ʒu
    private Transform pTransform;

    private Vector3 vel;

    //  �ŏ��Ɏ��s
    public override void EnemyStart()
    {
        timer = coolTime;

        pTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //  �X�V
    public override void EnemyUpdate()
    {
        //  ���Ԃ����炷
        if (timer > 0.0f) timer -= Time.deltaTime;
        else
        {
            //  �N�[���^�C����ݒ�
            timer = coolTime;

            //  �����ʒu���Z�o
            Vector3 createPos = head.transform.position + gameObject.transform.forward + Vector3.up;
            //  ���˕������Z�o
            Vector3 bulletDir = (gameObject.transform.forward + Vector3.up) * bulletSpeed;

            //  �e�𐶐�
            GameObject bullet = Instantiate(bulletPrefab, createPos, Quaternion.identity);
            //  �e�̔��˕�����ݒ�
            bullet.GetComponent<Bullet>().Shot(bulletDir, statas.ATK);
        }

        //  �ړ���
        float x = pTransform.position.x - gameObject.transform.position.x;
        vel.x += x * Time.deltaTime;
        rb.velocity = vel;
    }
}
