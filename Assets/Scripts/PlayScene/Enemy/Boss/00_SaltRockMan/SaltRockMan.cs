using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltRockMan : Enemy
{
    //  �ړ����x
    [SerializeField, HeaderAttribute("��")] float shotSaltDmgRate;

    //  �U��^�C�~���O
    [SerializeField] float coolTime;

    //  ���ԃJ�E���g�p
    private float time;

    //  �ŏ��Ɏ��s
    public override void EnemyStart()
    {
        time = coolTime;
    }

    //  �X�V
    public override void EnemyUpdate()
    {
        //  ���Ԍ���
        time -= Time.deltaTime;
        //  �����������΍��E���]
        if (time < 0.0f)
        {
            time = coolTime;
        }
    }
}
