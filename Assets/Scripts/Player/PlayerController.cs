using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /// <summary> ///////////////////////
    /// �ϐ��A�萔�錾
    /// </summary> ///////////////////////
    
    //rigidbody
    Rigidbody rigid;



    /// <summary> ///////////////////////
    /// ��{�ݒ�
    /// </summary> ///////////////////////

    //�v���C���[�̈ړ��X�s�[�h�A���x�{��
    [SerializeField,HeaderAttribute("�ړ��X�e�[�^�X")] private float move_speed = 1.0f;
    [SerializeField] private float speed_force = 4.0f;
    [SerializeField] private float jump_force = 4.0f;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float min_vertical_vel = -15.0f;

    [SerializeField, HeaderAttribute("��������")] private List<GameObject> weapons_list;

    [SerializeField] private MenuManager menu_manag;




    /// <summary> ///////////////////////
    /// �ړ��֘A
    /// </summary> ///////////////////////

    //�v���C���[�̑��x
    private float horizontal_speed;
    private float vertical_speed;

    //�v���C���[���ǂ��������Ă邩(true : �E,false : ��)
    private bool look_allow;



    /// <summary> ///////////////////////
    /// �W�����v�֘A
    /// </summary> //////////////////////

    //�W�����v�̉�
    private int jump_cnt;

    //�㏸����
    private bool isJumping;

    //���n���Ă邩
    private bool isGround;

    //ray�̋���
    [SerializeField] private float ray_dist = 0.5f;



    /// <summary> ///////////////////////
    /// �U���֘A
    /// </summary> ///////////////////////
    
    //���݂̎g�p����ԍ�
    private Weapon_no now_use_weapon_no;

    //���������Weapon�N���X
    [SerializeField] private List<Weapon> weapon;



    /// <summary> ///////////////////////
    /// �A�N�V�����X�L��
    /// </summary> ///////////////////////

    //�_�b�V���N���X
    Dash dash;

    //�_�u���W�����v�N���X
    DoubleJump doublejump;

    //�V�[���h�N���X(�o���A)
    Shield shield;

    //����U���N���X
    SpecialAttack special_attack;

    //�`���[�W�U��
    ChargeAttack charge_attack;

    //�����U��
    ThrowingAttack throwing_attack;



    /// <summary> ///////////////////////
    /// �֐�
    /// </summary> ///////////////////////

    // Start is called before the first frame update
    void Start()
    {
        rigid = transform.GetComponent<Rigidbody>();
        jump_cnt = 0;
        isJumping = false;
        isGround = false;
        now_use_weapon_no = Weapon_no.knife;
        look_allow = true;
        dash = transform.GetComponent<Dash>();
        doublejump = transform.GetComponent<DoubleJump>();
        shield = transform.GetComponent<Shield>();
        special_attack = transform.GetComponent<SpecialAttack>();
        charge_attack = transform.GetComponent<ChargeAttack>();
        throwing_attack = transform.GetComponent<ThrowingAttack>();
        foreach(GameObject wp in weapons_list)
        {
            Weapon wpclass = wp.GetComponent<Weapon>();
            weapon.Add(wpclass);
        }
    }

    private void Update()
    {
        //�ړ����x�̏�����
        horizontal_speed = 0;
        vertical_speed = -jump_force;

        //�������n�߂��璅�n������s���悤�ɂ���
        if (isJumping && rigid.velocity.y <= 0.0f) isJumping = false;

        //���n����
        CheckGround();

        //���n���Ă�Ƃ��A�W�����v�\�ɂ���
        if (isGround)
        {
            vertical_speed = 0;
            jump_cnt = 0;
        }

        //���n���ĂȂ���2�i�ڃW�����v���g���ĂȂ��Ƃ��A1�i�ڃW�����v�����g�������Ƃɂ���
        if(!isGround && jump_cnt == 0)
        {
            jump_cnt = 1;
        }

        //����2�i�ڃW�����v���g���ĂȂ�������A�g�p�\�Ƃ���
        if (jump_cnt <= 1)
        {
            doublejump.End_double_jump();
        }

        //�_�b�V�����Ȃ畂���ĂĂ������Ȃ��悤��
        if (dash.Get_is_dash())
        {
            vertical_speed = 0;
        }

        //�L�[�{�[�h����
        Key_Controll();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 now_velocity = rigid.velocity;
        Vector3 now_x = new Vector3(now_velocity.x, 0, 0);

        if (!isGround)
        {
            now_velocity.y -= gravity * Time.deltaTime;
            now_velocity.y = Mathf.Clamp(now_velocity.y, min_vertical_vel, 1000000.0f);
        }

        rigid.velocity = now_velocity;

        velocity.x = move_speed * horizontal_speed;
        rigid.AddForce(speed_force * (velocity - now_x));

        velocity = Vector3.zero;
    }

    //�n�ʂɂ��Ă邩�̊m�F
    private void CheckGround()
    {
        if (isJumping && rigid.velocity.y > 0)
        {
            isGround = false;
            return;
        }
        Vector3 rayPosition = transform.position;
        Ray ray = new Ray(rayPosition, Vector3.down);
        isGround = Physics.Raycast(ray, ray_dist);
    }

    //�L�[�{�[�h����
    private void Key_Controll() {
        var keyboard = Keyboard.current;
        
        //�����_�b�V�����Ă���A�����؂�
        if (dash.Get_is_dash()) return;

        if (keyboard != null)
        {
            //�ړ�
            if (keyboard.rightArrowKey.isPressed)
            {
                horizontal_speed = move_speed;
            }
            if (keyboard.leftArrowKey.isPressed)
            {
                horizontal_speed = -move_speed;
            }

            //�L�����N�^�[�̉�]
            if (keyboard.rightArrowKey.wasPressedThisFrame)
            {
                rigid.rotation = Quaternion.Euler(0, 0, 0);
                look_allow = true;
            }

            //�L�����N�^�[�̉�]
            if (keyboard.leftArrowKey.wasPressedThisFrame)
            {
                rigid.rotation = Quaternion.Euler(0, 180, 0);
                look_allow = false;
            }

            //���n���Ă�Ƃ��A�W�����v����
            if (keyboard.upArrowKey.wasPressedThisFrame)
            {
                if (isGround && jump_cnt == 0)
                {
                    jump_cnt = 1;
                    rigid.AddForce(new Vector3(0, jump_force, 0), ForceMode.Impulse);
                    isJumping = true;
                }
                else if (doublejump.Get_can_action_skill() && jump_cnt == 1)
                {
                    jump_cnt = 2;
                    doublejump.Start_jump(ref rigid);
                    isJumping = true;
                }
            }

            ////�������ŃW�����v�̍����������Ȃ�
            //if (isJumping &&
            //    !isJumpFinish &&
            //    keyboard.upArrowKey.isPressed &&
            //    jump_time < max_jump_time)
            //{
            //    vertical_speed = jump_speed;
            //}

            ////�r���ŃL�[�𗣂��Ƃ��̎��_�ŗ�����
            //if (isJumping && !isJumpFinish && keyboard.upArrowKey.wasReleasedThisFrame)
            //{
            //    isJumpFinish = true;
            //}

            //�_�b�V��
            if (keyboard.leftShiftKey.wasPressedThisFrame && dash.Get_now_can_dash())
            {
                dash.Start_Dash(look_allow);
                horizontal_speed = 0;
            }

            //�V�[���h�W�J
            if (keyboard.sKey.wasPressedThisFrame && shield.Get_now_can_shield())
            {
                shield.Start_shield();
            }

            //�U������
            if (keyboard.zKey.wasPressedThisFrame)
            {
                weapon[(int)now_use_weapon_no].Attack();
            }

            if (weapon[(int)now_use_weapon_no].Get_is_attack_now()) return;

            if (weapon[(int)now_use_weapon_no].Get_finish_flg())
            {
                throwing_attack.End_throwing_attack(now_use_weapon_no);
                weapon[(int)now_use_weapon_no].Reset_finish_flg();
            }

            //�U���{�^���𒷉���������`���[�W���Ԃ����܂�
            if (keyboard.zKey.isPressed && charge_attack.Get_can_action_skill())
            {
                charge_attack.Add_carge_time();
            }

            //�������Ƃ��܂ł̒��������ԂŃ`���[�W�U�������邩�ǂ����ς��
            if (keyboard.zKey.wasReleasedThisFrame)
            {
                //�`���[�W�������Ԃ��K��ȏ�Ȃ�`���[�W�U�����ł���悤�ɂȂ�(�X�L���������̂�)
                if (charge_attack.Check_full_charge())
                {
                    charge_attack.Start_charge_attack();
                    charge_attack.End__charge_attack();
                    weapon[(int)now_use_weapon_no].Charge_Attack();
                    Debug.Log("�`���[�W�A�^�b�N����");
                }

                //�`���[�W���Ԃ̏�����
                charge_attack.Reset_charge_time();
            }

            //����U��
            if (keyboard.xKey.wasPressedThisFrame && special_attack.Get_can_action_skill())
            {
                special_attack.Start_special_attack();
                Debug.Log("Start");
                weapon[(int)now_use_weapon_no].Special_Attack();
                
            }

            //�����U��
            if(keyboard.aKey.wasPressedThisFrame && throwing_attack.Get_can_action_skill())
            {
                throwing_attack.Start_throwing_attack();
                weapon[(int)now_use_weapon_no].Throwing_Attack(now_use_weapon_no, look_allow);
            }

            //����؂�ւ�
            //if (keyboard.xKey.wasPressedThisFrame)
            //{
            //    now_use_weapon_no = (Weapon_no)((int)now_use_weapon_no + 1);
            //    Debug.Log(now_use_weapon_no);
            //    if (now_use_weapon_no == Weapon_no.over_id)
            //    {
            //        now_use_weapon_no = Weapon_no.knife;
            //    }
            //}

            if (keyboard.jKey.wasPressedThisFrame)
            {
                menu_manag.New_Menu_unlock("�J�b�g�����S");
            }
        }
    }
}
