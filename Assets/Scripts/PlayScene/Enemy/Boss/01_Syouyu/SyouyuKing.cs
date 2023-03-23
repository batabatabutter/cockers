using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyouyuKing : Boss
{
    //  �U���̎��
    private enum AttackType
    {
        Attack01,
        Attack02,
        Attack03,

        OverID
    };
    //  �U���̎菇
    private enum Attack
    {
        Move01,
        Move02,
        Move03,
        Move04,
        Move05,

        OverID
    };

    //  �U�����
    private AttackType attackType;
    //  �U���菇
    private Attack attack;

    //  �ݖ�prefab
    [SerializeField] private List<GameObject> syouyuPrefabs;
    //  �ݖ����΂���
    [SerializeField] private GameObject head;

    //  �N�[���^�C��
    [SerializeField] private float coolTime;
    //  �N�[���^�C�}�[
    private float coolTimer;

    //  �U���͈�
    [SerializeField] private float attackDistance;

    //  �W�����v��
    [SerializeField] private float jumpForce;
    //  �W�����v����
    private bool jump;

    //  �ڕW�n�_
    private Vector3 targetPos;

    //  �p�x
    private int rotX;
    //  �ݖ��̔��ˑ��x
    [SerializeField] private float syouyuSpeed;
    //  �ėp�^�C�}�[
    private float timer;
    //  ���˃^�C��
    [SerializeField] private float shotTime;

    //  ��������
    public override void BossStart()
    {
        attackType = AttackType.OverID;

        attack = Attack.OverID;

        coolTimer = 0f;

        rb = gameObject.GetComponent<Rigidbody>();

        targetPos = Vector3.zero;

        rotX = 0;
        jump = false;
        timer = 0f;
    }

    //  �X�V����
    public override void BossUpdate()
    {
        //  �N�[���^�C�������݂��Ă���
        if (coolTimer > 0f)
        {
            //  �N�[�����Ԍ��炷
            coolTimer -= Time.deltaTime;
            //  �����I��
            return;
        }

        //  �v���C���[���U���͈͓��ɂ��Ȃ��E�U����Ԃł͂Ȃ�
        if (Vector3.Distance(gameObject.transform.position, player.transform.position) > attackDistance &&
            attackType == AttackType.OverID)
        {
            //  �v���C���[�Ɍ������Ĉړ�����

            //  �����I��
            return;
        }
        //  �v���C���[���U���͈͓��ɂ���E�U����Ԃł͂Ȃ�
        else if(attackType == AttackType.OverID)
        {
            //  �U���̎�ނ�ݒ肷��
            attackType = (AttackType)Random.Range(0, (int)AttackType.OverID);
            
            //  �U���菇���P��
            attack = Attack.Move01;
            //  �����I��
            return;
        }

        //  �U���̏���
        switch (attackType)
        {
            case AttackType.Attack01:
                //  �U���I������
                if (Attack01()) FinishAttack();
                break;
            case AttackType.Attack02:
                //  �U���I������
                if (Attack02()) FinishAttack();
                break;
            case AttackType.Attack03:
                //  �U���I������
                if (Attack03()) FinishAttack();
                break;
        }
    }
    //  �U���P�i�ݖ��𔪕����ɔ��ˁj
    private bool Attack01()
    {
        switch(attack)
        {
            //  �W�����v
            case Attack.Move01:
                //  ������ɗ͂�������
                if (!jump)
                {
                    rb.AddForce(Vector3.up * jumpForce);
                    jump = true;
                }
                //  ���̃X�e�b�v��
                if(rb.velocity.y > 0f) attack++;
                return false;

            //  �d�͂�؂�/�󒆂ŐÎ~
            case Attack.Move02:
                //  �����n�߂Ă���
                if(rb.velocity.y <= 0f)
                {
                    //  �d�͂�؂�
                    rb.useGravity = false;
                    //  �Î~������
                    rb.velocity = Vector3.zero;
                    //  �Î~�ʒu��ݒ�
                    targetPos = gameObject.transform.position;
                    //  ���̃X�e�b�v��
                    attack++;
                }
                return false;

            //  ��]���Ȃ��瓪����ݖ����΂�
            case Attack.Move03:
                //  �Î~������
                rb.velocity = Vector3.zero;
                //  ��~�ʒu�ŌŒ肷��
                gameObject.transform.position = targetPos;
                //  ��]������
                rotX += 1;
                gameObject.transform.localEulerAngles = new Vector3((float)rotX, -90f, 0f);

                //  �e�p�x�Œe�𔭎�
                if (rotX % 45 == 0)
                {
                    //  �p�x���Z�o
                    float rad = (rotX + 90) * Mathf.Deg2Rad;
                    //  �i�s�������Z�o
                    Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0f).normalized;
                    //  �e�𔭎�
                    CreateSyouyu(syouyuPrefabs[0], dir * syouyuSpeed);
                }
                //  ���������U���I��
                if(rotX >= 360)
                {
                    //  �d�͂�߂�
                    rb.useGravity = true;
                    //  �{�̂̊p�x��߂�
                    gameObject.transform.localEulerAngles = new Vector3(0f, -90f, 0f);

                    //  �U���I��
                    return true;
                }
                return false;
        }
        //  �U���I��
        return true;
    }
    //  �U���Q�i�ݖ����v���C���[�Ɍ������Ĕ��ˁj
    private bool Attack02()
    {
        switch(attack)
        {
            //  �\������p(�Ƃ肠�������ɃW�����v)
            case Attack.Move01:
                //  �v���C���[�Ɣ��Ε������Z�o
                float pDir = (player.transform.position.x - gameObject.transform.position.x) * -1;
                //  ��ԕ���
                Vector3 d = new Vector3(pDir, 0f, 0f).normalized + Vector3.up;
                //  ��΂�
                rb.AddForce(d * jumpForce / 1.2f);
                //  �U���܂ł̗]���^�C��
                timer = -1.0f;
                //  ���̃X�e�b�v��
                attack++;
                return false;

            //  �v���C���[�Ɍ������Č���
            case Attack.Move02:
                //  ���ˎ��Ԃ��J�E���g
                timer += Time.deltaTime;
                //  ���ˎ��Ԃ𒴂���
                if(timer > shotTime)
                {
                    //  �J�E���g���O
                    timer = 0f;
                    //  �v���C���[����
                    Vector3 dir = (player.transform.position - head.transform.position).normalized;
                    //  ����
                    CreateSyouyu(syouyuPrefabs[0], dir * syouyuSpeed);
                    //  �J�E���g����
                    rotX++;
                    //  �U���I��
                    if (rotX > 10) return true;
                }
                return false;
        }

        //  �U���I��
        return true;
    }
    //  �U���R
    private bool Attack03()
    {
        switch(attack)
        {
            //  �\������i�W�����v�j
            case Attack.Move01:
                //  ������ɗ͂�������
                if (!jump)
                {
                    rb.AddForce(Vector3.up * jumpForce);
                    jump = true;
                }
                //  ���̃X�e�b�v��
                if (rb.velocity.y > 0f) attack++;
                return false;

            //  �\������i�Ƃ肠�����k����j
            case Attack.Move02:
                //  �����n�߂Ă���
                if (rb.velocity.y <= 0f)
                {
                    //  �k����
                    gameObject.GetComponent<ShakeByPerlinNoise>().StartShake(2f, 0.1f, 0.1f);

                    //  ��b�k����
                    timer += Time.deltaTime;
                }
                
                if(timer > 1f)
                {
                    //  ���Z�b�g
                    timer = 0f;
                    //  ���̃X�e�b�v��
                    attack++;
                }
                return false;

            //  �ݖ��𔭎�(�k���Ȃ���)
            case Attack.Move03:
                //  �e�𔭎ˁi�R���j
                for (int i = 0; i < 3; i++)
                {
                    //  ���������������_���Ŏ擾
                    float x = Random.Range(-1f, 1f);
                    //  ���˕���
                    Vector3 di = new Vector3(x, 1f, 0f).normalized;
                    //  ����
                    CreateSyouyu(syouyuPrefabs[1], di * syouyuSpeed);
                }
                //  �I��
                return true;
        }

        //  �U���I��
        return true;
    }
    //  �U���I������
    private void FinishAttack()
    {
        //  �N�[���^�C����ݒ�
        coolTimer = coolTime;

        //  �U���̎�ނ�����
        attackType = AttackType.OverID;
        //  �U����������Ԃ�
        attack = Attack.OverID;

        //  �e�ϐ��̏�����
        targetPos = Vector3.zero; 
        rotX = 0;
        jump = false;
        timer = 0f;
    }
    //  �ݖ������
    private void CreateSyouyu(GameObject syouyuP, Vector3 dir)
    {
        //  ����
        GameObject syouyu = Instantiate(syouyuP, head.transform.position, Quaternion.identity);
        //  �����ɔ�΂�
        syouyu.GetComponent<Rigidbody>().AddForce(dir);
    }
}
