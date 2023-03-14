using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Armor : ActionSkill
{
    //�v���C���[�̃X�e�[�^�X
    PlayerStatus status;

    //�_���[�W���󂯂ĂȂ�����
    private float no_damage_time;

    //�A�[�}�[�̎󂯂��_���[�W��
    [SerializeField] private int armor_val;

    //�A�[�}�[�̍ő�l
    private int armor_max_val;

    //�A�[�}�[�̍ŏ��l
    private const int armor_min_val = 0;

    //�v���C���[�̊�bhp
    private const int player_def_hp = 100;

    //�A�[�}�[�̑�����
    [SerializeField, HeaderAttribute("1�b���̃A�[�}�[�̑�����(�ő�HP�ɑ΂���%)")] 
    private int armor_add_per;

    //�A�[�}�[�̍ő�l
    [SerializeField, HeaderAttribute("�A�[�}�[�̍ő�t�^��(�ő�HP�ɑ΂���%)")]
    private int armor_max_per;

    //�A�[�}�[���t�^����n�߂�܂ł̎���(�������Ɏg�����)
    private const float first_cool_time = 5.0f;

    public Armor() : base()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        status = transform.GetComponent<PlayerStatus>();
        now_cool_time = first_cool_time;
    }

    public override void FixedUpdate()
    {
        if (can_action_skill)
        {
            no_damage_time += Time.fixedDeltaTime;
            armor_max_val = (int)((player_def_hp + status.Get_hp()) * armor_max_per * 0.01);
        }

        //1�b���ɃA�[�}�[��t�^����
        if (no_damage_time >= 1.0f)
        {
            armor_val += (int)((player_def_hp + status.Get_hp()) * 0.04);
            armor_val = Mathf.Clamp(armor_val, armor_min_val, armor_max_val);
            no_damage_time = 0.0f;
        }
    }

    //�V�[���h��W�J����
    public void Start_armor()
    {
        Start_action_skill();
    }

    //�V�[���h�W�J�I���
    public void End_armor()
    {
        if (!have_action_skill) return;
        End_action_skill();
    }

    //�V�[���h�W�J���\����Ԃ�
    public bool Get_now_can_armor()
    {
        return Get_can_action_skill();
    }

    //�V�[���h��W�J�����ǂ�����Ԃ�
    public bool Get_is_armor()
    {
        return Get_is_action_skill();
    }

    //�_���[�W�y��
    public void damage_reduction(ref int damage)
    {
        int reduction_val = armor_val - damage;
        if (reduction_val>=0)
        {
            armor_val -= damage;
            damage = 0;
        }

        else
        {
            damage -= armor_val;
            armor_val = 0;
        }

        Start_action_skill();
        End_action_skill();
    }
}
