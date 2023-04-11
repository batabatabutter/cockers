using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class Skills : MonoBehaviour
{
    GameObject armor;
    GameObject barrier;
    GameObject ChargeAttack;
    GameObject dash;
    GameObject doubleJump;
    GameObject SPAttack;
    GameObject throwAttack;

    GameObject PlayerSkills;

    bool have_armor;
    bool have_barrier;
    bool have_ChargeAttack;
    bool have_dash;
    bool have_doubleJump;
    bool have_SPAttack;
    bool have_throwAttack;

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
        have_armor = PlayerSkills.GetComponent<Armor>().Get_can_action_skill();
        have_barrier = PlayerSkills.GetComponent<Shield>().Get_can_action_skill();
        have_ChargeAttack = PlayerSkills.GetComponent<ChargeAttack>().Get_can_action_skill();
        have_dash = PlayerSkills.GetComponent<Dash>().Get_can_action_skill();
        have_doubleJump = PlayerSkills.GetComponent<DoubleJump>().Get_can_action_skill();
        have_SPAttack = PlayerSkills.GetComponent<SpecialAttack>().Get_can_action_skill();
        have_throwAttack = PlayerSkills.GetComponent<ThrowingAttack>().Get_can_action_skill();

        armor.SetActive(have_armor);
        barrier.SetActive(have_barrier);
        ChargeAttack.SetActive(have_ChargeAttack);
        dash.SetActive(have_dash);
        doubleJump.SetActive(have_doubleJump);
        SPAttack.SetActive(have_SPAttack);
        throwAttack.SetActive(have_throwAttack);


    }
}
