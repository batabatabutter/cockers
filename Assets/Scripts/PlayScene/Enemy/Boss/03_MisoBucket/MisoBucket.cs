using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisoBucket : Boss
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

    //  �N�[���^�C��
    [SerializeField] private float coolTime;
    //  �N�[���^�C�}�[
    private float coolTimer;

    //  �U���͈�
    [SerializeField] private float attackDistance;

    //  ��������
    private Rigidbody rd;

    //  �W�����v��
    [SerializeField] private float jumpForce;

    //  �ڕW�n�_
    private Vector3 targetPos;

    //  ��������
    public override void BossStart()
    {
        attackType = AttackType.OverID;

        attack = Attack.OverID;

        coolTimer = 0f;

        rd = gameObject.GetComponent<Rigidbody>();

        targetPos = Vector3.zero;
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
        else if (attackType == AttackType.OverID)
        {
            //  �U���̎�ނ�ݒ肷��
            //attackType = (AttackType)Random.Range(0, (int)AttackType.OverID);
            attackType = AttackType.Attack01;
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
    //  �U���P
    private bool Attack01()
    {
        
        //  �U���I��
        return true;
    }
    //  �U���Q
    private bool Attack02()
    {
        //  �U�����I��
        return false;

        //  �U���I��
        return true;
    }
    //  �U���R
    private bool Attack03()
    {
        //  �U�����I��
        return false;

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
    }
}
