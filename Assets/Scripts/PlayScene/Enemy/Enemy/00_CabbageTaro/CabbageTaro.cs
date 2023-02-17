using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabbageTaro : Enemy
{
    //  �ړ����x
    [SerializeField, HeaderAttribute("��")] float speed;

    //  �؂�ւ����^�C�~���O
    [SerializeField] float inversionTime;

    //  �Ԃ��������̃_���[�W�{��
    [SerializeField] float collisionDMG;

    //  ���ԃJ�E���g�p
    private float time;

    //  �ŏ��Ɏ��s
    public override void EnemyStart()
    {
        time = inversionTime;
    }


    //  �X�V
    public override void EnemyUpdate()
    {
        //  ���Ԍ���
        time -= Time.deltaTime;
        //  �����������΍��E���]
        if (time < 0.0f)
        {
            this.gameObject.transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
            time = inversionTime;
        }

        //  �ړ�
        this.gameObject.transform.position += speed * Time.deltaTime * this.gameObject.transform.right;
    }
}
