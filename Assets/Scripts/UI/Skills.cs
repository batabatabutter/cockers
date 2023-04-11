using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;


public class Skills : MonoBehaviour
{
    GameObject SPAttack;        //特殊攻撃オブジェクト格納用
    GameObject throwAttack;     //投げ攻撃オブジェクト格納用
    GameObject ChargeAttack;    //チャージアタックオブジェクト格納用

    GameObject dash;            //ダッシュオブジェクト格納用
    GameObject doubleJump;      //ダブルジャンプオブジェクト格納用

    GameObject armor;           //アーマーオブジェクト格納用
    GameObject barrier;         //バリアオブジェクト格納用


    GameObject PlayerSkills;    //プレイヤー格納用


    bool have_SPAttack;         //特殊攻撃の有無
    bool have_throwAttack;      //投げ攻撃の有無
    bool have_ChargeAttack;     //チャージアタックの有無

    bool have_dash;             //ダッシュの有無
    bool have_doubleJump;       //ジャンプの有無

    bool have_armor;            //アーマーの有無
    bool have_barrier;          //バリアの有無

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


        //表示or非表示
        SPAttack.SetActive(have_SPAttack);
        throwAttack.SetActive(have_throwAttack);
        ChargeAttack.SetActive(have_ChargeAttack);

        dash.SetActive(have_dash);
        doubleJump.SetActive(have_doubleJump);

        armor.SetActive(have_armor);
        barrier.SetActive(have_barrier);
    }
}
