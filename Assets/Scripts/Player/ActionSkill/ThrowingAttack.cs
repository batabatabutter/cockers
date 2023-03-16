using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ThrowingAttack : ActionSkill
{ 

    public ThrowingAttack() : base(){ }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public override void FixedUpdate()
    {
        if (is_action_skill)
        {
            now_effect_time += Time.fixedDeltaTime;
            Check_end_throwing_attack();
        }
    }

    //�����U��������
    public void Start_throwing_attack()
    {
        Start_action_skill();
    }

    //�����U���I���
    public void End_throwing_attack()
    {
    }

    //�����U�����I����������`�F�b�N����
    public void Check_end_throwing_attack()
    {
        Check_end_action_skill();
    }

    //�����U�����\����Ԃ�
    public bool Get_now_can_throwing_attack()
    {
        return Get_can_action_skill();
    }

    //�����U�������ǂ�����Ԃ�
    public bool Get_is_throwing_attack()
    {
        return Get_is_action_skill();
    }
}
