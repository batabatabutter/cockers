using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarrotSpear : Enemy
{
    //  �������
    [SerializeField, HeaderAttribute("��")] float shotSpeed;

    //  �Ďn���^�C�~���O
    [SerializeField] float coolTime;

    //  ���ł��鎞��
    [SerializeField] float flyTime;

    //  �s�����Ă��鋗��
    [SerializeField] float distance;

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
        if (time < 0.0f) time = 0.0f;

        //  �G�ƃv���C���[�̈ʒu���߂�������U��
        if (Vector3.Distance(this.transform.position, player.transform.position) < distance && time <= 0.0f)
        {
            Vector3 angle = (player.transform.position - transform.position);
            transform.rotation = Quaternion.Euler(new Vector3(-angle.normalized.x, angle.normalized.z, angle.normalized.y) * 180.0f);
            rb.velocity += angle.normalized * shotSpeed;
            rb.velocity += Vector3.up * shotSpeed * 0.1f;
            time = coolTime + flyTime;
            move = true;
        }
        else if (time <= coolTime && move)
        {
            transform.rotation = Quaternion.identity;
            rb.velocity = Vector3.zero;
            move = false;
        }
    }
}
