using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class DoubleJump : ActionSkill
{
    //加える力(Xのみ)
    [SerializeField, HeaderAttribute("ジャンプ力")] private float jump_force = 5.0f;

    //加える力(行列)
    private Vector3 jump_force_vec;

    public DoubleJump() : base()
    {
        //rigid = transform.GetComponent<Rigidbody>();
        jump_force_vec = Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        jump_force_vec = Vector3.zero;
        jump_force_vec.y = jump_force;
    }

    public override void FixedUpdate()
    {
    }

    //ダッシュする
    public void Start_jump(ref Rigidbody rigid)
    {
        Start_action_skill();
        Vector3 vel = rigid.velocity;
        vel.y = 0;
        rigid.velocity = vel;
        rigid.AddForce(jump_force_vec, ForceMode.Impulse);
    }

    //ダッシュ終わり
    public void End_double_jump()
    {
        if (!have_action_skill) return;
        End_action_skill();
    }

    //ダッシュが可能かを返す
    public bool Get_now_can_double_jump()
    {
        return Get_can_action_skill();
    }

    //ダッシュ中かどうかを返す
    public bool Get_is_double_jump()
    {
        return Get_is_action_skill();
    }

}
