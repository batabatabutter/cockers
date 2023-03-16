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

    //“Š±UŒ‚‚ğ‚·‚é
    public void Start_throwing_attack()
    {
        Start_action_skill();
    }

    //“Š±UŒ‚I‚í‚è
    public void End_throwing_attack()
    {
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
