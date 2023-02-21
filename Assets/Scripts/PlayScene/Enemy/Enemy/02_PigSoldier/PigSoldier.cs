using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigSoldier : Enemy
{
    //  �ړ����x
    [SerializeField, HeaderAttribute("��")] float swordDmgRate;

    //  �U��^�C�~���O
    [SerializeField] float coolTime;

    //  ����U�鎞��
    [SerializeField] float swordTime;

    //  �O��
    [SerializeField] float forwordTime;

    //  �s�����Ă��鋗��
    [SerializeField] float distance;

    // �f�o�b�O�p�U�艺�낷��
    [SerializeField] Transform arm;

    //  ���ԃJ�E���g�p
    private float time;

    private bool move = false;

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
        if (time < 0.0f) time = 0.0f;

        if (!move)
        {
            float distance = (player.transform.position.x - gameObject.transform.position.x);
            if(distance < 0.0f) transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0.0f, transform.rotation.z));
            else transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 180.0f, transform.rotation.z));
        }

        //  �G�ƃv���C���[�̈ʒu���߂�������U��
        if (Vector3.Distance(this.transform.position, player.transform.position) < distance && time <= 0.0f)
        {
            time = coolTime + swordTime + forwordTime;
            move = true;
        }
        //  (�f�o�b�O)�O����
        else if(time >= coolTime + swordTime)
        {
            arm.Rotate(new Vector3(0.0f, 0.0f, -90.0f) * Time.deltaTime / forwordTime);
        }
        //  (�f�o�b�O)�U���Ă���Ƃ�
        else if(time >= coolTime && move)
        {
            nowAttack = true;
            arm.Rotate(new Vector3(0.0f, 0.0f, 180.0f) * Time.deltaTime / swordTime);
        }
        //  �U��I�������
        else if (time <= coolTime && move)
        {
            nowAttack = false;
            move = false;
            arm.Rotate(new Vector3(0.0f, 0.0f, -90.0f));
            rb.velocity = Vector3.zero;
        }
    }
}
