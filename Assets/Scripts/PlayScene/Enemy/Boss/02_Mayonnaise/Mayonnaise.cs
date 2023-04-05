using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mayonnaise : Boss
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

    //  ���˂���}���l�[�Y
    [SerializeField] private GameObject mayoPrefab;

    //  ����
    [SerializeField] private List<GameObject> shoulders;
    //  ����
    [SerializeField] private List<GameObject> hands;
    //  ��
    [SerializeField] private GameObject head;

    //  �N�[���^�C��
    [SerializeField] private float coolTime;
    //  �N�[���^�C�}�[
    private float coolTimer;

    //  �U���͈�
    [SerializeField] private float attackDistance;
    //  �W�����v��
    [SerializeField] private float jumpForce;
    //  �W�����v������
    private bool jump;
    //  �ڕW�n�_
    private Vector3 targetPos;

    //  �J�E���^�[
    private int counter;
    //  �^�C�}�[
    private float timer;

    //  ���˂̑��x
    [SerializeField] private float mayoSpeed;

    //  �ŏ��̊p�x
    private Vector3 startRot;
    //  �ڕW�̊p�x
    private Vector3 targetRot;

    //  �T�C�Y
    private float scaY;
    //  �傫���ύX�p
    private bool size01;
    private bool size02;

    [SerializeField] private float speed;

    //  ��������
    public override void BossStart()
    {
        attackType = AttackType.OverID;

        attack = Attack.OverID;

        coolTimer = 0f;

        rb = gameObject.GetComponent<Rigidbody>();

        targetPos = Vector3.zero;

        jump = false;

        counter = 0;
        timer = 0f;

        startRot = Vector3.zero;
        targetRot = Vector3.zero;

        scaY = 1f;
        size01 = false;
        size02 = false;
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
            Vector3 pToB = player.transform.position - gameObject.transform.position;
            //  ����
            rb.velocity = pToB.normalized * speed;
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
            rb.velocity = Vector3.zero;
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
        switch (attack)
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
                if (rb.velocity.y > 0f) attack++;
                return false;

            //  �d�͂�؂�/�󒆂ŐÎ~
            case Attack.Move02:
                //  �����n�߂Ă���
                if (rb.velocity.y <= 0f)
                {
                    //  �d�͂�؂�
                    rb.useGravity = false;
                    //  �Î~������
                    rb.velocity = Vector3.zero;
                    //  �Î~�ʒu��ݒ�
                    targetPos = gameObject.transform.position;
                    //  ���̊p�x���L�^���Ă���
                    targetRot = shoulders[1].transform.localEulerAngles;
                    //  �]���̎��Ԃ����
                    timer = 1f;
                    //  ���̃X�e�b�v��
                    attack++;
                }
                return false;

            //  �󒆂���}���r�[��
            case Attack.Move03:
                timer -= Time.deltaTime;

                //  �Î~������
                rb.velocity = Vector3.zero;
                //  ��~�ʒu�ŌŒ肷��
                gameObject.transform.position = targetPos;

                //  �v���C���[�ƌ��̊p�x
                float rad = Mathf.Atan2(player.transform.position.y - shoulders[1].transform.position.y,
                    player.transform.position.x - shoulders[1].transform.position.x);
                //  ���̊p�x��ς���
                shoulders[1].transform.localEulerAngles = new Vector3(rad * Mathf.Rad2Deg + 90f, 0f, 0f);
                
                if(timer <= 0f)
                {
                    //  ��΂�����
                    Vector3 dir = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0.0f).normalized;

                    //  �肩��}���l�[�Y���΂�
                    CreateMayo(hands[1].transform.position, dir * mayoSpeed, false);

                    //  �J�E���^�[��i�߂�
                    counter++;
                }

                //  �S�Ĕ��˂�����߂�
                if (counter > 120)
                {
                    //  ���̃X�e�b�v��
                    attack++;
                    //  �d�͂�t����
                    rb.useGravity = true;
                    //  �^�C�}�[�̏�����
                    timer = 0f;
                    //  �ŏ��̌��̊p�x
                    startRot = shoulders[1].transform.localEulerAngles;
                }
                return false;

            case Attack.Move04:
                //  ���Ԃ�i�߂�
                timer += Time.deltaTime;
                //  ��b�����Ė߂�
                Vector3 rot = Vector3.Lerp(startRot, targetRot, timer);
                //  ���߂�
                shoulders[1].transform.localEulerAngles = rot;

                //  ��b��ɍU���I��
                if (timer >= 1f) return true;
                return false;
        }
        //  �U���I��
        return true;
    }
    //  �U���Q
    private bool Attack02()
    {
        ////  �U�����I��
        //return false;

        //  �U���I��
        return true;
    }
    //  �U���R(������}����΂�)
    private bool Attack03()
    {
        switch(attack)
        {
            //  �㉺�ɗh���
            case Attack.Move01:
                //  �T�C�Y�X�V
                gameObject.transform.localScale = new Vector3(1f, scaY, 1f);
                //  �T�C�Y�ύX
                if (!size01)
                {
                    //  ����������
                    scaY -= Time.deltaTime;
                    if (scaY < 0.75f) size01 = true;
                }
                else if(!size02)
                {
                    //  �傫������
                    scaY += Time.deltaTime;
                    if (scaY > 1.25f) size02 = true;
                }
                else
                {
                    //  ����������
                    scaY -= Time.deltaTime;
                    if(scaY < 1f)
                    {
                        //  �T�C�Y�����ɖ߂�
                        gameObject.transform.localScale = new Vector3(1f, 1f, 1f);
                        //  ���̃X�e�b�v��
                        attack++;
                    }
                }
                return false;

            //  �����甭��
            case Attack.Move02:
                //  �e�𔭎ˁi�R���j
                for (int i = 0; i < 3; i++)
                {
                    //  ���������������_���Ŏ擾
                    float x = Random.Range(-1f, 1f);
                    //  ���˕���
                    Vector3 di = new Vector3(x, 0.5f, 0f).normalized;
                    //  ����
                    CreateMayo(head.transform.position, di * mayoSpeed, true);
                }
                //  �U���I��
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

        //  �e�֐���������
        jump = false;
        counter = 0;
        timer = 0f;
        startRot = Vector3.zero;
        targetRot = Vector3.zero;
        scaY = 1f;
        size01 = false;
        size02 = false;
    }
    //  �}���l�[�Y�����
    private void CreateMayo(Vector3 pos, Vector3 dir, bool useGrabity)
    {
        //  ��������
        GameObject mayo = Instantiate(mayoPrefab, pos, Quaternion.identity);
        //  ��΂�
        mayo.GetComponent<Rigidbody>().AddForce(dir);
        //  �d�͂�ON�EOFF
        mayo.GetComponent<Rigidbody>().useGravity = useGrabity;
    }
}
