using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class Skills : MonoBehaviour
{
    GameObject SPAttack;        //����U���I�u�W�F�N�g�i�[�p
    GameObject throwAttack;     //�����U���I�u�W�F�N�g�i�[�p
    GameObject ChargeAttack;    //�`���[�W�A�^�b�N�I�u�W�F�N�g�i�[�p

    GameObject dash;            //�_�b�V���I�u�W�F�N�g�i�[�p
    GameObject doubleJump;      //�_�u���W�����v�I�u�W�F�N�g�i�[�p

    GameObject armor;           //�A�[�}�[�I�u�W�F�N�g�i�[�p
    GameObject barrier;         //�o���A�I�u�W�F�N�g�i�[�p


    GameObject PlayerSkills;    //�v���C���[�i�[�p


    bool have_SPAttack;         //����U���̗L��
    bool have_throwAttack;      //�����U���̗L��
    bool have_ChargeAttack;     //�`���[�W�A�^�b�N�̗L��

    bool have_dash;             //�_�b�V���̗L��
    bool have_doubleJump;       //�W�����v�̗L��

    bool have_armor;            //�A�[�}�[�̗L��
    bool have_barrier;          //�o���A�̗L��

    // Start is called before the first frame update
    void Start()
    {
        armor = GameObject.Find("armor");
        barrier = GameObject.Find("barrier");
        ChargeAttack = GameObject.Find("ChargeAttack");
        dash = GameObject.Find("dash");
        doubleJump = GameObject.Find("doubleJump");
        SPAttack = GameObject.Find("SPAttack");
        throwAttack = GameObject.Find("throwAttack");

        PlayerSkills = GameObject.Find("PlayManager").GetComponent<PlayManager>().GetPlayer();

    }

    // Update is called once per frame
    void Update()
    {
        have_SPAttack = PlayerSkills.GetComponent<SpecialAttack>().Get_can_action_skill();
        have_throwAttack = PlayerSkills.GetComponent<ThrowingAttack>().Get_can_action_skill();
        have_ChargeAttack = PlayerSkills.GetComponent<ChargeAttack>().Get_can_action_skill();

        have_dash = PlayerSkills.GetComponent<Dash>().Get_can_action_skill();
        have_doubleJump = PlayerSkills.GetComponent<DoubleJump>().Get_can_action_skill();

        have_armor = PlayerSkills.GetComponent<Armor>().Get_can_action_skill();
        have_barrier = PlayerSkills.GetComponent<Shield>().Get_can_action_skill();


        //�\��or��\��
        SPAttack.SetActive(have_SPAttack);
        throwAttack.SetActive(have_throwAttack);
        ChargeAttack.SetActive(have_ChargeAttack);

        dash.SetActive(have_dash);
        doubleJump.SetActive(have_doubleJump);

        armor.SetActive(have_armor);
        barrier.SetActive(have_barrier);
    }
}
