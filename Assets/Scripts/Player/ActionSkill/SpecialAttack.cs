using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class SpecialAttack : ActionSkill
{

    //rigidbody
    [SerializeField] private Rigidbody rigid;

    private float one_add = 360.0f / 0.5f;

    public SpecialAttack () : base()
    {

    }
    
    // Start is called before the first frame update
    void Start()
    {
        rigid = transform.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    public override void FixedUpdate() {
        if(is_action_skill)
        {
            //y軸支点の回転
            Quaternion rot = Quaternion.AngleAxis(one_add * Time.fixedDeltaTime, Vector3.up);
            //現在のrotation取得
            Quaternion q = transform.rotation;
            //回転
            transform.rotation = q * rot;

            now_effect_time += Time.fixedDeltaTime;
            Check_end_special_attack();
        }
    }

    //特殊攻撃をする
    public void Start_special_attack()
    {
        Start_action_skill();
    }

    //特殊攻撃終わり
    public void End_special_attack()
    {
        End_action_skill();
    }

    //特殊攻撃が終わったかをチェックする
    public void Check_end_special_attack()
    {
        Check_end_action_skill();
    }

    //特殊攻撃が可能かを返す
    public bool Get_now_can_special_attack()
    {
        return Get_can_action_skill();
    }

    //特殊攻撃中かどうかを返す
    public bool Get_is_special_attack()
    {
        return Get_is_action_skill();
    }
}
