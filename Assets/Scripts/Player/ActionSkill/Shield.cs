using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Shield : ActionSkill
{
    public Shield() : base()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public override void FixedUpdate()
    {
    }

    //�V�[���h��W�J����
    public void Start_shield()
    {
        Start_action_skill();
    }

    //�V�[���h�W�J�I���
    public void End_shield()
    {
        if (!have_action_skill) return;
        End_action_skill();
    }

    //�V�[���h�W�J���\����Ԃ�
    public bool Get_now_can_shield()
    {
        return Get_can_action_skill();
    }

    //�V�[���h��W�J�����ǂ�����Ԃ�
    public bool Get_is_shield()
    {
        return Get_is_action_skill();
    }

}