using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltRockMan : Boss
{
    enum Attack
    {
        rush,
        throwSalt,
        arm,

        attackNum,
        none
    };

    [Header("��")]
    [SerializeField, Label("�U���Ԋu")] float coolTime;
    [SerializeField, Label("�ߐڍs������")] float clossDistance;
    [SerializeField, Label("���ߐڋ���")] float veryCloseDistance;
    [SerializeField, Label("�ړ����x")] float moveSpeed;

    [Header("�ːi")]
    [SerializeField, Label("�ːi����")] float rushTime;
    [SerializeField, Label("�ːi���x")] float rushSpeed;
    [SerializeField, Label("�ːi�O��")] float rushForwordTime;
    [SerializeField, Label("�ːi�㌄")] float rushAfterTime;

    [Header("������")]
    [SerializeField, Label("��������")] float throwSaltTime;
    [SerializeField, Label("�������O��")] float throwSaltForwordTime;
    [SerializeField, Label("�������㌄")] float throwSaltAfterTime;

    [SerializeField, Label("���������x")] float throwSaltSpeed;
    [SerializeField, Label("��������")] GameObject throwSaltObject;

    [Header("�r��U��")]
    [SerializeField, Label("�r�U�莞��")] float armTime;
    [SerializeField, Label("�r�U��O��")] float armForwordTime;
    [SerializeField, Label("�r�U��㌄")] float armAfterTime;

    [Header("���̑�")]
    [SerializeField, Label("�̂̃q�b�g�{�b�N�X")] Collider bodyHitBox;
    [SerializeField, Label("�r�̃q�b�g�{�b�N�X")] Collider armHitBox;
    
    // �f�o�b�O�p�U�艺�낷��
    [SerializeField] Transform arm;
    //  �w��
    [SerializeField] GameObject hand;

    //  ���ԃJ�E���g�p
    private float time;

    //  �e���
    private bool move;
    private Attack attack;

    private Vector3 angle;

    //  �ŏ��Ɏ��s
    public override void BossStart()
    {
        time = coolTime;
        bodyHitBox.enabled = false;
        armHitBox.enabled = false;
    }

    //  �X�V
    public override void BossUpdate()
    {
        //  ���Ԍ���
        time -= Time.deltaTime;
        //  �����������΍��E���]
        if (time < 0.0f) time = 0.0f;

        if (Vector3.Distance(transform.position, player.transform.position) < clossDistance && time <= 0.0f)
        {
            do
            {
                attack = (Attack)Random.Range(0, (int)Attack.attackNum);
            } while (attack == Attack.throwSalt);
            move = true;
            rb.velocity = Vector3.zero;
        }
        else if(time <= 0.0f)
        {
            do
            {
                attack = (Attack)Random.Range(0, (int)Attack.attackNum);
            } while (attack == Attack.arm);
            move = true;
            rb.velocity = Vector3.zero;
        }

        //  ����
        if (move)
        {
            switch (attack)
            {
                case Attack.rush:
                    Rush();
                    break;
                case Attack.throwSalt:
                    ThrrowSalt();
                    break;
                case Attack.arm:
                    Arm();
                    break;
            }
        }
        else
        {
            //  �����]��
            float distance = (player.transform.position.x - gameObject.transform.position.x);
            if (distance < 0.0f) transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0.0f, transform.rotation.z));
            else transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 180.0f, transform.rotation.z));

            //  �߂Â��}��
            angle = new Vector3(player.transform.position.x - transform.position.x, 0.0f, 0.0f);
            rb.velocity = angle.normalized * moveSpeed;

            //  �߂��������~
            if (veryCloseDistance >= distance && -veryCloseDistance <= distance) rb.velocity = Vector3.zero;            
        }
    }

    //  �ːi�U��
    private void Rush()
    {
        //  �ŏ�
        if (time <= 0.0f)
        {
            time = coolTime + rushAfterTime + rushTime + rushForwordTime;
        }
        //  �O��
        else if(time >= coolTime + rushAfterTime + rushTime)
        {
            arm.Rotate(new Vector3(0.0f, 0.0f, 270.0f) * Time.deltaTime / rushForwordTime);
        }
        //  ���s��
        else if(time >= coolTime + rushAfterTime)
        {
            //  �ŏ��������s�����
            if (!nowAttack)
            {
                angle = new Vector3(player.transform.position.x - transform.position.x, 0.0f, 0.0f);
                rb.velocity = angle.normalized * rushSpeed;
            }
            bodyHitBox.enabled = true;
            nowAttack = true;
        }
        //  �㌄
        else if(time >= coolTime)
        {
            arm.Rotate(new Vector3(0.0f, 0.0f, -270.0f) * Time.deltaTime / rushAfterTime);
            bodyHitBox.enabled = false;
            nowAttack = false;
            rb.velocity = Vector3.zero;
        }
        //  �Ō�
        else
        {
            move = false;
        }
    }

    //  ������
    private void ThrrowSalt()
    {
        //  �U��
        if (time <= 0.0f)
        {
            time = coolTime + throwSaltAfterTime + throwSaltTime + throwSaltForwordTime;
            move = true;
        }
        //  �O��
        else if (time >= coolTime + throwSaltAfterTime + throwSaltTime)
        {
            arm.Rotate(new Vector3(0.0f, 0.0f, -45.0f) * Time.deltaTime / throwSaltForwordTime);
        }
        //  ���s��
        else if (time >= coolTime + throwSaltAfterTime)
        {
            //  �ŏ��������s
            if (!nowAttack)
                CreateSalt();
            armHitBox.enabled = true;
            nowAttack = true;
            arm.Rotate(new Vector3(0.0f, 0.0f, 165.0f) * Time.deltaTime / throwSaltTime);
        }
        //  �㌄
        else if(time >= coolTime)
        {
            //  �ŏ��������s
            if (nowAttack)
                CreateSalt();
            armHitBox.enabled = false;
            nowAttack = false;
            arm.Rotate(new Vector3(0.0f, 0.0f, -120.0f) * Time.deltaTime / throwSaltAfterTime);

        }
        //  �Ō�
        else
        {
            move = false;
        }
    }

    //  ������
    private void CreateSalt()
    {
        angle = (player.transform.position - hand.transform.position);
        Vector3 pos = hand.transform.position;
        pos.z = 0.0f;
        GameObject obj = Instantiate(throwSaltObject, pos, Quaternion.identity);
        obj.GetComponent<Rigidbody>().velocity = angle.normalized * throwSaltSpeed;
    }

    //  �r�u���u��
    private void Arm()
    {
        //  �U��
        if (time <= 0.0f)
        {
            time = coolTime + armAfterTime + armTime + armForwordTime;
            move = true;
        }
        //  �O��
        else if (time >= coolTime + armAfterTime + armTime)
        {
            arm.Rotate(new Vector3(0.0f, 0.0f, -45.0f) * Time.deltaTime / armForwordTime);
        }
        //  ���s��
        else if (time >= coolTime + armAfterTime)
        {
            armHitBox.enabled = true;
            nowAttack = true;
            arm.Rotate(new Vector3(0.0f, 0.0f, 165.0f) * Time.deltaTime / armTime);
        }
        //  �㌄
        else if (time >= coolTime)
        {
            armHitBox.enabled = false;
            nowAttack = false;
            arm.Rotate(new Vector3(0.0f, 0.0f, -120.0f) * Time.deltaTime / armAfterTime);

        }
        //  �Ō�
        else
        {
            move = false;
        }
    }
}
