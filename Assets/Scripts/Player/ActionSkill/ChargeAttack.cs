using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ChargeAttack : ActionSkill
{
    //�`���[�W�U���̃`���[�W����
    [SerializeField, HeaderAttribute("�`���[�W�U���̃`���[�W����")] private float charge_time = 1.5f;
    private float now_charge_time;

    public ChargeAttack() : base() { }
    // Start is called before the first frame update
    void Start()
    {
        Reset_charge_time();
    }

    // Update is called once per frame
    public override void FixedUpdate() { }

    //�_�b�V������
    public void Start_charge_attack()
    {
        Start_action_skill();
    }

    //�_�b�V���I���
    public void End__charge_attack()
    {
        End_action_skill();
    }

    public void Reset_charge_time()
    {
        now_charge_time = 0.0f;
    }

    public void Add_carge_time()
    {
        if (now_charge_time >= charge_time) return;
        now_charge_time += Time.deltaTime;
    }

    public bool Check_full_charge()
    {
        return now_charge_time >= charge_time;
    }
}
