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

        attackNum
    };

    //  �^�C�~���O
    [SerializeField, HeaderAttribute("��")] float coolTime;
    //  �s�����Ă��鋗��
    [SerializeField] float distance;

    //  �ːi����
    [SerializeField] float rushTime;
    //  �ːi���x
    [SerializeField] float rushSpeed;

    //  �r��U�鎞��
    [SerializeField] float throwSaltTime;
    //  �O��
    [SerializeField] float throwSaltForwordTime;
    //  ����U��_���[�W
    [SerializeField] float throwSaltDmgRate;
    //  ����U��Speed
    [SerializeField] float throwSaltSpeed;
    //  �U��I�u�W�F�N�g
    [SerializeField] GameObject throwSaltObject;

    //  �r��U�鎞��
    [SerializeField] float armTime;
    //  �O��
    [SerializeField] float armForwordTime;

    // �f�o�b�O�p�U�艺�낷��
    [SerializeField] Transform arm;
    //  �w��
    [SerializeField] GameObject hand;

    //  ���ԃJ�E���g�p
    private float time;

    //  
    private bool move;
    private Attack attack;

    private Vector3 angle;

    //  �ŏ��Ɏ��s
    public override void BossStart()
    {
        time = coolTime;
    }

    //  �X�V
    public override void BossUpdate()
    {
        //  ���Ԍ���
        time -= Time.deltaTime;
        //  �����������΍��E���]
        if (time < 0.0f) time = 0.0f;

        if (Vector3.Distance(transform.position, player.transform.position) < distance && time <= 0.0f)
        {
            attack = (Attack)Random.Range(0, (int)Attack.attackNum);
            move = true;
        }

        if (move)
        {
            switch (attack)
            {
                case Attack.rush:
                    Debug.Log("Rush");
                    Rush();
                    break;
                case Attack.throwSalt:
                    Debug.Log("ThrowSalt");
                    ThrrowSalt();
                    break;
                case Attack.arm:
                    Debug.Log("Arm");
                    Arm();
                    break;
            }
        }
        else
        {
            float distance = (player.transform.position.x - gameObject.transform.position.x);
            if (distance < 0.0f) transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 0.0f, transform.rotation.z));
            else transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, 180.0f, transform.rotation.z));
        }
    }

    //  �ːi�U��
    private void Rush()
    {
        if (time <= 0.0f)
        {
            nowAttack = true;
            angle = new Vector3(player.transform.position.x - transform.position.x, 0.0f, 0.0f);
            rb.AddForce(angle.normalized * rushSpeed);
            time = coolTime + rushTime;
        }
        else if (time >= coolTime)
        {
            nowAttack = false;
            rb.velocity = Vector3.zero;
            move = false;
        }
    }

    //  ������
    private void ThrrowSalt()
    {
        //  �U��
        if (time <= 0.0f)
        {
            time = coolTime + throwSaltTime + throwSaltForwordTime;
            move = true;
            Invoke(nameof(CreateSalt), 3.0f);
        }
        //  (�f�o�b�O)�O����
        else if (time >= coolTime + throwSaltTime)
        {
            arm.Rotate(new Vector3(0.0f, 0.0f, -45.0f) * Time.deltaTime / throwSaltForwordTime);
        }
        //  (�f�o�b�O)�U���Ă���Ƃ�
        else if (time >= coolTime)
        {
            nowAttack = true;
            arm.Rotate(new Vector3(0.0f, 0.0f, 165.0f) * Time.deltaTime / throwSaltTime);
        }
        //  �U��I�������
        else
        {
            nowAttack = false;
            move = false;
            arm.Rotate(new Vector3(0.0f, 0.0f, -120.0f));
            rb.velocity = Vector3.zero;
        }
    }

    private void CreateSalt()
    {
        angle = (player.transform.position - transform.position);
        GameObject obj = Instantiate(throwSaltObject, hand.transform.position, Quaternion.identity);
        obj.GetComponent<Rigidbody>().velocity = angle.normalized * throwSaltSpeed;
        obj.GetComponent<Salt>().SetDmg((int)(throwSaltDmgRate * statas.ATK));
    }

    //  �r�u���u��
    private void Arm()
    {
        //  �U��
        if (time <= 0.0f)
        {
            time = coolTime + armTime + armForwordTime;
            move = true;
        }
        //  (�f�o�b�O)�O����
        else if (time >= coolTime + armTime)
        {
            arm.Rotate(new Vector3(0.0f, 0.0f, -45.0f) * Time.deltaTime / armForwordTime);
        }
        //  (�f�o�b�O)�U���Ă���Ƃ�
        else if (time >= coolTime)
        {
            nowAttack = true;
            arm.Rotate(new Vector3(0.0f, 0.0f, 165.0f) * Time.deltaTime / armTime);
        }
        //  �U��I�������
        else
        {
            nowAttack = false;
            move = false;
            arm.Rotate(new Vector3(0.0f, 0.0f, -120.0f));
            rb.velocity = Vector3.zero;
        }
    }
}
