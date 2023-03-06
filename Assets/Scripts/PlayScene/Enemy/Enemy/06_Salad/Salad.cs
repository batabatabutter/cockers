using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salad : Enemy
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

    //  �W�����v��
    [SerializeField] private float jumpForce;
    //  �W�����v�^�C�~���O
    [SerializeField] private float jumpTime;
    //  �W�����v�p�^�C�}�[
    private float jumpTimer;

    //  �ŏ��Ɏ��s
    public override void EnemyStart()
    {
        timer = coolTime;
        jumpTimer = jumpTime;
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
            Vector3 createPos = head.transform.position + gameObject.transform.forward;
            //  ���˕������Z�o
            Vector3 bulletDir = (gameObject.transform.forward + Vector3.up) * bulletSpeed;

            //  �e�𐶐�
            GameObject bullet = Instantiate(bulletPrefab, createPos, Quaternion.identity);
            //  �e�̔��˕�����ݒ�
            bullet.GetComponent<Bullet>().Shot(bulletDir, statas.ATK);
        }

        if (jumpTimer > 0.0f) jumpTimer -= Time.deltaTime;
        else
        {
            jumpTimer = jumpTime;

            rb.AddForce(Vector3.up * jumpForce);
        }
    }
}
