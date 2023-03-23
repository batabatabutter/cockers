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
            //y���x�_�̉�]
            Quaternion rot = Quaternion.AngleAxis(one_add * Time.fixedDeltaTime, Vector3.up);
            //���݂�rotation�擾
            Quaternion q = transform.rotation;
            //��]
            transform.rotation = q * rot;

            now_effect_time += Time.fixedDeltaTime;
            Check_end_special_attack();
        }
    }

    //����U��������
    public void Start_special_attack()
    {
        Start_action_skill();
    }

    //����U���I���
    public void End_special_attack()
    {
        End_action_skill();
    }

    //����U�����I����������`�F�b�N����
    public void Check_end_special_attack()
    {
        Check_end_action_skill();
    }

    //����U�����\����Ԃ�
    public bool Get_now_can_special_attack()
    {
        return Get_can_action_skill();
    }

    //����U�������ǂ�����Ԃ�
    public bool Get_is_special_attack()
    {
        return Get_is_action_skill();
    }
}
