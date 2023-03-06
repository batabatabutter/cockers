using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Strawberry : Enemy
{
    //  �ړ����x
    [SerializeField, HeaderAttribute("��")] private float moveSpeed;
    //  ��]����
    [SerializeField] private float rotTime;
    //  ��~����
    [SerializeField] private float coolTime;
    //  ��
    [SerializeField] private GameObject body;

    //  �s�����鋗��
    [SerializeField] private float distance;

    //  ���ԃJ�E���g�p
    private float coolTimer;
    private float rotTimer;
    //  �����Ă��
    private bool moving = false;

    //  �ړ���
    private Vector3 vel;

    //  �ŏ��Ɏ��s
    public override void EnemyStart()
    {
        coolTimer = 0.0f;
        rotTimer = 0.0f;

        vel = Vector3.zero;
    }

    //  �X�V
    public override void EnemyUpdate()
    {
        if(coolTimer > 0.0f)
        {
            coolTimer -= Time.deltaTime;
            return;
        }

        if(!moving)
        {
            //  �v���C���[�����̋������ɓ������瓮���o��
            float pDistance = Vector3.Distance(player.transform.position, body.transform.position);
            Debug.Log(pDistance);
            if(pDistance < distance)
            {
                //  �ړ���Ԃɂ���
                moving = true;
                //  �^�C�}�[��ݒ�
                rotTimer = rotTime;
                //  �U����Ԃɂ���
                nowAttack = true;

                return;
            }

            //  �ړ�

        }
        else
        {
            //  ���Ԃ����炷
            rotTimer -= Time.deltaTime;

            if(rotTimer < 0.0f)
            {
                //  �~�߂�
                moving = false;
                //  �^�C�}�[��ݒ�
                coolTimer = coolTime;
                //  �U����Ԃɂ���
                nowAttack = false;
                //  �~�߂�
                rb.velocity = Vector3.zero;

                return;
            }

            //  �v���C���[�̕����Ɉړ�����
            float x = player.transform.position.x - body.transform.position.x;
            //  �ړ��ʂ��X�V
            vel.x += (x * moveSpeed);
            rb.velocity = vel;
        }
    }
}
