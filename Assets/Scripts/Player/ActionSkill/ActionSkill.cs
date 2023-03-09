using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract class ActionSkill : MonoBehaviour
{
    [SerializeField, HeaderAttribute("�X�L���������Ă邩")] protected bool have_action_skill;

    [SerializeField, HeaderAttribute("�X�L�����g���邩")] protected bool can_action_skill;

    [SerializeField, HeaderAttribute("�N�[���^�C��")] protected float cool_time = 1.0f;

    [SerializeField, HeaderAttribute("���ʎ���")] protected float effect_time = 1.0f;

    //���݃X�L�����g�p�����ǂ���
    protected bool is_action_skill;

    //���݂̎c��N�[���^�C��
    protected float now_cool_time;

    //���݂̎c����ʎ���
    protected float now_effect_time;

    public ActionSkill()
    {
        have_action_skill = false;
        can_action_skill = false;
        is_action_skill = false;
        now_cool_time = 0.0f;
        now_effect_time = 0.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        have_action_skill = false;
        can_action_skill = false;
        is_action_skill = false;
        now_cool_time = 0.0f;
        now_effect_time = 0.0f;
    }

    // Update is called once per frame
    //�A�b�v�f�[�g
    //�N�[���^�C���̌����ɗp����
    void Update()
    {
        if (!have_action_skill) return;

        if (0.0f < now_cool_time)
        {
            now_cool_time -= Time.deltaTime;
            if (now_cool_time <= 0.0f)
            {
                can_action_skill = true;
            }
        }
    }

    public abstract void FixedUpdate();

    public void Allow_action_skill_to_player()
    {
        have_action_skill = true;
        can_action_skill = true;
    }

    protected void Start_action_skill()
    {
        is_action_skill = true;
        can_action_skill = false;
    }

    protected void End_action_skill()
    {
        is_action_skill = false;
        now_cool_time = cool_time;
        now_effect_time = 0.0f;
    }

    public bool Check_end_action_skill()
    {
        if (now_effect_time >= effect_time)
        {
            End_action_skill();
            return true;
        }
        return false;
    }

    public bool Get_can_action_skill()
    {
        return can_action_skill & !is_action_skill;
    }

    public bool Get_is_action_skill()
    {
        return is_action_skill;
    }
}
