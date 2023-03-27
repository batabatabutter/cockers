using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisonoOke : Boss
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
    [SerializeField, ReadOnly] private float coolTimer;

    //  �U���͈�
    [SerializeField] private float attackDistance;

    //  ��������
    private Rigidbody rd;

    //  �W�����v��
    [SerializeField] private float jumpForce;

    //  �ڕW�n�_
    private Vector3 targetPos;

    //  �ω����
    public enum MisoStatas
    {
        Mix,
        Red,
        White,

        MisoNum
    }
    private MisoStatas misoStatas;

    [SerializeField, Header("�~�b�N�X���X��"), Label("�U���͔{��")] float mixAtkRate;
    [SerializeField, Label("�U�����x�{��")] float mixSpdRate;

    [SerializeField, Header("�Ԗ��X��"), Label("�U���͔{��")] float redAtkRate;
    [SerializeField, Label("�U�����x�{��")] float redSpdRate;

    [SerializeField, Header("�����X��"), Label("�U���͔{��")] float whiteAtkRate;
    [SerializeField, Label("�U�����x�{��")] float whiteSpdRate;

    private float atkRate;
    private float spdRate;

    private int bulletCount = 0;
    private float bulletRate = 0.0f;

    //  �e1
    [SerializeField, Header("���X�e"), Label("�I�u�W�F�N�g")] GameObject bullet1Obj;
    [SerializeField, Label("�_���[�W")] int bullet1Dmg;
    [SerializeField, Label("���x")] float bullet1Speed;
    [SerializeField, Label("���ˌ�")] int bullet1Num;
    [SerializeField, Label("���ˑ��x")] float bullet1Rate;

    //  �e2
    [SerializeField, Header("���X�e(���X��)"), Label("�I�u�W�F�N�g")] GameObject bullet2Obj;
    [SerializeField, Label("�_���[�W")] int bullet2Dmg;
    [SerializeField, Label("���x")] float bullet2Speed;
    [SerializeField, Label("���ˑ��x")] float bullet2UpSpeed;
    [SerializeField, Label("���ˌ�")] int bullet2Num;
    [SerializeField, Label("���ˑ��x")] float bullet2Rate;

    //  ��������
    public override void BossStart()
    {
        attackType = AttackType.OverID;

        attack = Attack.OverID;

        coolTimer = 0f;

        rd = gameObject.GetComponent<Rigidbody>();

        targetPos = Vector3.zero;

        misoStatas = MisoStatas.Mix;
    }

    //  �X�V����
    public override void BossUpdate()
    {
        switch (misoStatas)
        {
            case MisoStatas.Mix:
                atkRate = mixAtkRate;
                spdRate = mixSpdRate;
                break;
            case MisoStatas.Red:
                atkRate = redAtkRate;
                spdRate = redSpdRate;
                break;
            case MisoStatas.White:
                atkRate = whiteAtkRate;
                spdRate = whiteSpdRate;
                break;
        }

        //  �N�[���^�C�������݂��Ă���
        if (coolTimer > 0f && attackType == AttackType.OverID)
        {
            //  �N�[�����Ԍ��炷
            coolTimer -= Time.deltaTime * spdRate;
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
    //  �U���P(���X�؂�ւ�)
    private bool Attack01()
    {
        Debug.Log(misoStatas);
        //  ���X�؂�ւ�
        while (true)
        {
            MisoStatas afterMiso = (MisoStatas)Random.Range(0, (int)MisoStatas.MisoNum);
            if (misoStatas != afterMiso)
            {
                misoStatas = afterMiso;
                break;
            }
        }

        //  �U���I��
        return true;
    }
    //  �U���Q(���X�e1����)
    private bool Attack02()
    {
        bulletRate -= Time.deltaTime;
        bulletRate = Mathf.Clamp(bulletRate, 0.0f, float.MaxValue);

        //  �e����
        if (bulletRate <= 0.0f)
        {
            GameObject obj = Instantiate(bullet1Obj, transform.position, Quaternion.identity);
            obj.GetComponent<MisoBullet>().SetBulletStatas((int)(bullet1Dmg * atkRate), bullet1Speed, -Vector3.Normalize(obj.transform.position - player.transform.position));
            bulletCount++;
            bulletRate = bullet1Rate;
        }

        //  �U�����I��
        if (bulletCount < bullet1Num) return false;
        //  �U���I��
        bulletCount = 0;
        bulletRate = 0.0f;
        return true;
    }
    //  �U���R(���X�e2����)
    private bool Attack03()
    {
        bulletRate -= Time.deltaTime;
        bulletRate = Mathf.Clamp(bulletRate, 0.0f, float.MaxValue);

        //  �e����
        if (bulletRate <= 0.0f)
        {
            GameObject obj = Instantiate(bullet2Obj, transform.position, Quaternion.identity);
            obj.GetComponent<MisoBulletCreateFloor>().SetBulletStatas((int)(bullet2Dmg * atkRate), bullet2Speed, bullet2UpSpeed * (bulletCount + 1) / 3, player.transform.position);
            bulletCount++;
            bulletRate = bullet2Rate;
        }

        //  �U�����I��
        if (bulletCount < bullet2Num) return false;
        //  �U���I��
        bulletCount = 0;
        bulletRate = 0.0f;
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
