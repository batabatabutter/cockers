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
    }

    //“Š±UŒ‚‚ğ‚·‚é
    public void Start_throwing_attack()
    {
        Start_action_skill();
    }

    //“Š±UŒ‚I‚í‚è
    public void End_throwing_attack(Weapon_no weapon_no)
    {
        End_action_skill();
        if (weapon_no == Weapon_no.knife) now_cool_time = 1.0f;
        else if (weapon_no == Weapon_no.flypan) now_cool_time = 2.0f;
    }

    //“Š±UŒ‚‚ªI‚í‚Á‚½‚©‚ğƒ`ƒFƒbƒN‚·‚é
    public void Check_end_throwing_attack()
    {
        Check_end_action_skill();
    }

    //“Š±UŒ‚‚ª‰Â”\‚©‚ğ•Ô‚·
    public bool Get_now_can_throwing_attack()
    {
        return Get_can_action_skill();
    }

    //“Š±UŒ‚’†‚©‚Ç‚¤‚©‚ğ•Ô‚·
    public bool Get_is_throwing_attack()
    {
        return Get_is_action_skill();
    }
}
