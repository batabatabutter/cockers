using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Dash : ActionSkill
{
    //rigidbody
    [SerializeField] private Rigidbody rigid;

    //�������(X�̂�)
    [SerializeField, HeaderAttribute("�_�b�V�����ɉ������")] private float dash_force = 5.0f;

    //�������(�s��)
    private Vector3 dash_force_vec;

    //�v���C���[���ǂ��������Ă邩(true : �E,false : ��)
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

    //�_�b�V������
    public void Start_Dash(bool allow)
    {
        Start_action_skill();
        if(allow) dash_force_vec.x = dash_force;
        else dash_force_vec.x = -dash_force;
        rigid.constraints |= RigidbodyConstraints.FreezePositionY;
    }

    //�_�b�V���I���
    private void End_dash()
    {
        rigid.constraints = RigidbodyConstraints.None;
        rigid.constraints |= RigidbodyConstraints.FreezeRotation;
        rigid.constraints |= RigidbodyConstraints.FreezePositionZ;
        rigid.velocity = Vector3.zero;
    }

    //�_�b�V�����I����������`�F�b�N����
    public void Check_end_dash()
    {
        if (Check_end_action_skill())
        {
            End_dash();
        }
    }

    //�_�b�V�����\����Ԃ�
    public bool Get_now_can_dash()
    {
        return Get_can_action_skill();
    }

    //�_�b�V�������ǂ�����Ԃ�
    public bool Get_is_dash()
    {
        return Get_is_action_skill();
    }

}
