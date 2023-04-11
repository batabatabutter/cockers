using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpecialAttack : ActionSkill
{
    public SpecialAttack () : base()
    {

    }
    
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    public override void FixedUpdate() {
        if(is_action_skill)
        {
            now_effect_time += Time.fixedDeltaTime;
            Check_end_special_attack();
        }
    }

    //“ÁêUŒ‚‚ğ‚·‚é
    public void Start_special_attack()
    {
        Start_action_skill();
    }

    //“ÁêUŒ‚I‚í‚è
    public void End_special_attack()
    {
        End_action_skill();
    }

    //“ÁêUŒ‚‚ªI‚í‚Á‚½‚©‚ğƒ`ƒFƒbƒN‚·‚é
    public void Check_end_special_attack()
    {
        Check_end_action_skill();
    }

    //“ÁêUŒ‚‚ª‰Â”\‚©‚ğ•Ô‚·
    public bool Get_now_can_special_attack()
    {
        return Get_can_action_skill();
    }

    //“ÁêUŒ‚’†‚©‚Ç‚¤‚©‚ğ•Ô‚·
    public bool Get_is_special_attack()
    {
        return Get_is_action_skill();
    }
}
