using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Dash : ActionSkill
{
    //rigidbody
    [SerializeField] private Rigidbody rigid;

    //加える力(Xのみ)
    [SerializeField, HeaderAttribute("ダッシュ時に加える力")] private float dash_force = 5.0f;

    //加える力(行列)
    private Vector3 dash_force_vec;

    //プレイヤーがどっち向いてるか(true : 右,false : 左)
    private bool look_allow;

    public Dash() : base()
    {
        //rigid = transform.GetComponent<Rigidbody>();
        dash_force_vec = Vector3.zero;
    }

    // Start is called before the first frame update
    void Start()
    {
        rigid = transform.GetComponent<Rigidbody>();
        dash_force_vec = Vector3.zero;
    }

    public override void FixedUpdate()
    {
        if (is_action_skill)
        {
            rigid.AddForce(dash_force_vec, ForceMode.Impulse);
            now_effect_time += Time.deltaTime;
            Check_end_dash();
        }
    }

    //ダッシュする
    public void Start_Dash(bool allow)
    {
        Start_action_skill();
        if(allow) dash_force_vec.x = dash_force;
        else dash_force_vec.x = -dash_force;
        rigid.constraints |= RigidbodyConstraints.FreezePositionY;
    }

    //ダッシュ終わり
    private void End_dash()
    {
        rigid.constraints = RigidbodyConstraints.None;
        rigid.constraints |= RigidbodyConstraints.FreezeRotation;
        rigid.constraints |= RigidbodyConstraints.FreezePositionZ;
        rigid.velocity = Vector3.zero;
    }

    //ダッシュが終わったかをチェックする
    public void Check_end_dash()
    {
        if (Check_end_action_skill())
        {
            End_dash();
        }
    }

    //ダッシュが可能かを返す
    public bool Get_now_can_dash()
    {
        return Get_can_action_skill();
    }

    //ダッシュ中かどうかを返す
    public bool Get_is_dash()
    {
        return Get_is_action_skill();
    }

}
