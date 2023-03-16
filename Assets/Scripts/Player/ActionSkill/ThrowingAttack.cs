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

    //投擲攻撃をする
    public void Start_throwing_attack()
    {
        Start_action_skill();
    }

    //投擲攻撃終わり
    public void End_throwing_attack()
    {
    }

    //投擲攻撃が終わったかをチェックする
    public void Check_end_throwing_attack()
    {
        Check_end_action_skill();
    }

    //投擲攻撃が可能かを返す
    public bool Get_now_can_throwing_attack()
    {
        return Get_can_action_skill();
    }

    //投擲攻撃中かどうかを返す
    public bool Get_is_throwing_attack()
    {
        return Get_is_action_skill();
    }
}
