using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    /// <summary> ///////////////////////
    /// 変数、定数宣言
    /// </summary> ///////////////////////

    //rigidbody
    Rigidbody rigid;



    /// <summary> ///////////////////////
    /// 基本設定
    /// </summary> ///////////////////////

    //プレイヤーの移動スピード、速度倍率
    [SerializeField, HeaderAttribute("移動ステータス")] private float move_speed = 1.0f;
    [SerializeField] private float speed_force = 4.0f;
    [SerializeField] private float jump_force = 4.0f;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float min_vertical_vel = -15.0f;

    [SerializeField, HeaderAttribute("所持武器")] private List<GameObject> weapons_list;

    //プレイヤーのステータス
    private PlayerStatus player_status;


    /// <summary> ///////////////////////
    /// 移動関連
    /// </summary> ///////////////////////

    //プレイヤーの速度
    private float horizontal_speed;
    private float vertical_speed;

    //プレイヤーがどっち向いてるか(true : 右,false : 左)
    private bool look_allow;

    private Vector3 player_Vector;



    /// <summary> ///////////////////////
    /// ジャンプ関連
    /// </summary> //////////////////////

    //ジャンプの回数
    private int jump_cnt;

    //上昇中か
    [SerializeField] private bool isJumping;

    //着地してるか
    [SerializeField] private bool isGround;

    //rayの距離
    [SerializeField] private float ray_dist = 0.5f;



    /// <summary> ///////////////////////
    /// 攻撃関連
    /// </summary> ///////////////////////

    //現在の使用武器番号
    private Weapon_no now_use_weapon_no;

    //所持武器のWeaponクラス
    [SerializeField] private List<Weapon> weapon;



    /// <summary> ///////////////////////
    /// アクションスキル
    /// </summary> ///////////////////////

    //ダッシュクラス
    Dash dash;

    //ダブルジャンプクラス
    DoubleJump doublejump;

    //シールドクラス(バリア)
    Shield shield;

    //特殊攻撃クラス
    SpecialAttack special_attack;

    //チャージ攻撃
    ChargeAttack charge_attack;

    //投擲攻撃
    ThrowingAttack throwing_attack;


    /// <summary> ///////////////////////
    /// アニメーター
    /// </summary> ///////////////////////
    Animator animator;
    private const string NormalAttackFlg = "NormalAttackFlg";
    private const string SpecialAttackFlg = "SpecialAttackFlg";
    private const string WeaponNo = "WeaponNo";
    private const string Speed = "Speed";



    /// <summary> ///////////////////////
    /// 関数
    /// </summary> ///////////////////////

    // Start is called before the first frame update
    void Start()
    {
        rigid = transform.GetComponent<Rigidbody>();
        jump_cnt = 0;
        isJumping = false;
        isGround = false;
        now_use_weapon_no = Weapon_no.knife;
        look_allow = true;
        dash = transform.GetComponent<Dash>();
        doublejump = transform.GetComponent<DoubleJump>();
        shield = transform.GetComponent<Shield>();
        special_attack = transform.GetComponent<SpecialAttack>();
        charge_attack = transform.GetComponent<ChargeAttack>();
        throwing_attack = transform.GetComponent<ThrowingAttack>();
        foreach (GameObject wp in weapons_list)
        {
            Weapon wpclass = wp.GetComponent<Weapon>();
            weapon.Add(wpclass);
        }
        animator = transform.GetComponent<Animator>();
        animator.SetInteger(WeaponNo, 1);

        player_status = transform.GetComponent<PlayerStatus>();
    }

    private void Update()
    {
        //移動速度の初期化
        horizontal_speed = 0;
        vertical_speed = -jump_force;

        //落下し始めたら着地判定を行うようにする
        if (isJumping && player_Vector.y < 0.0f)
        {
            isJumping = false;
        }

        //着地してるとき、ジャンプ可能にする
        if (isGround)
        {
            vertical_speed = 0;
            jump_cnt = 0;
        }

        //着地してなくて2段目ジャンプを使ってないとき、1段目ジャンプだけ使ったことにする
        if (!isGround && jump_cnt == 0)
        {
            jump_cnt = 1;
        }

        //もし2段目ジャンプを使ってなかったら、使用可能とする
        if (jump_cnt <= 1)
        {
            doublejump.End_double_jump();
        }

        //ダッシュ中なら浮いてても落ちないように
        if (dash.Get_is_dash())
        {
            vertical_speed = 0;
        }

        //キーボード操作
        Key_Controll();

        //アニメーションのチェック
        Animation_Check();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 now_velocity = rigid.velocity;
        Vector3 now_x = new Vector3(now_velocity.x, 0, 0);

        if (horizontal_speed > 0.0f) transform.localScale = new Vector3(1, 1, 1);
        else if (horizontal_speed < 0.0f) transform.localScale = new Vector3(-1, 1, -1);
        animator.SetFloat(Speed, Mathf.Abs(horizontal_speed));

        //着地判定
        CheckGround();

        //  重力
        {
            now_velocity.y -= gravity * Time.deltaTime;
            now_velocity.y = Mathf.Clamp(now_velocity.y, min_vertical_vel, 1000000.0f);
        }

        rigid.velocity = now_velocity;

        //  横移動
        velocity.x = move_speed * horizontal_speed;
        rigid.AddForce(speed_force * (velocity - now_x));

        player_Vector = rigid.velocity;
        velocity = Vector3.zero;
    }

    //地面についてるかの確認
    private void CheckGround()
    {
        if (isJumping || rigid.velocity.y < -0.3f)
        {
            isGround = false;
            return;
        }
        Vector3 rayPosition = transform.position;
        Ray ray = new Ray(rayPosition, Vector3.down);
        isGround = Physics.Raycast(ray, ray_dist);
        isGround = true;
    }

    //キーボード操作
    private void Key_Controll()
    {
        var keyboard = Keyboard.current;

        if (Time.timeScale == 0f) return;

        //もしダッシュしてたら、操作を切る
        if (dash.Get_is_dash()) return;

        if (player_status.Get_now_hp() == 0) return;

        if (keyboard != null)
        {
            //キャラクターの回転
            if (keyboard.rightArrowKey.wasPressedThisFrame)
            {
                look_allow = true;
            }

            //キャラクターの回転
            if (keyboard.leftArrowKey.wasPressedThisFrame)
            {
                look_allow = false;
            }

            //移動
            if (keyboard.rightArrowKey.isPressed)
            {
                horizontal_speed = move_speed;
            }
            if (keyboard.leftArrowKey.isPressed)
            {
                horizontal_speed = -move_speed;
            }

            //着地してるとき、ジャンプする
            if (keyboard.upArrowKey.wasPressedThisFrame)
            {
                if (isGround && jump_cnt == 0)
                {
                    jump_cnt = 1;
                    rigid.AddForce(new Vector3(0, jump_force, 0), ForceMode.Impulse);
                    isJumping = true;
                }
                else if (doublejump.Get_can_action_skill() && jump_cnt == 1)
                {
                    jump_cnt = 2;
                    doublejump.Start_jump(ref rigid);
                    isJumping = true;
                }
            }

            ////長押しでジャンプの高さが高くなる
            //if (isJumping &&
            //    !isJumpFinish &&
            //    keyboard.upArrowKey.isPressed &&
            //    jump_time < max_jump_time)
            //{
            //    vertical_speed = jump_speed;
            //}

            ////途中でキーを離すとその時点で落ちる
            //if (isJumping && !isJumpFinish && keyboard.upArrowKey.wasReleasedThisFrame)
            //{
            //    isJumpFinish = true;
            //}

            //ダッシュ
            if (keyboard.leftShiftKey.wasPressedThisFrame && dash.Get_now_can_dash())
            {
                dash.Start_Dash(look_allow);
                horizontal_speed = 0;
            }

            //シールド展開
            if (keyboard.sKey.wasPressedThisFrame && shield.Get_now_can_shield())
            {
                shield.Start_shield();
            }

            //攻撃処理
            if (keyboard.zKey.wasPressedThisFrame)
            {
                bool action = weapon[(int)now_use_weapon_no].Attack();
                if (action) animator.SetBool(NormalAttackFlg, true);
            }

            if (weapon[(int)now_use_weapon_no].Get_is_attack_now()) return;

            if (weapon[(int)now_use_weapon_no].Get_finish_flg())
            {
                throwing_attack.End_throwing_attack(now_use_weapon_no);
                weapon[(int)now_use_weapon_no].Reset_finish_flg();
            }

            //攻撃ボタンを長押ししたらチャージ時間がたまる
            if (keyboard.zKey.isPressed && charge_attack.Get_can_action_skill())
            {
                charge_attack.Add_carge_time();
            }

            //離したときまでの長押し時間でチャージ攻撃をするかどうか変わる
            if (keyboard.zKey.wasReleasedThisFrame)
            {
                //チャージした時間が規定以上ならチャージ攻撃ができるようになる(スキル所持時のみ)
                if (charge_attack.Check_full_charge())
                {
                    charge_attack.Start_charge_attack();
                    charge_attack.End__charge_attack();
                    weapon[(int)now_use_weapon_no].Charge_Attack();
                    Debug.Log("チャージアタック成功");
                }

                //チャージ時間の初期化
                charge_attack.Reset_charge_time();
            }

            //特殊攻撃
            if (keyboard.xKey.wasPressedThisFrame && special_attack.Get_can_action_skill())
            {
                special_attack.Start_special_attack();
                weapon[(int)now_use_weapon_no].Special_Attack();
                animator.SetBool(SpecialAttackFlg, true);
            }

            //投擲攻撃
            if (keyboard.aKey.wasPressedThisFrame && throwing_attack.Get_can_action_skill())
            {
                throwing_attack.Start_throwing_attack();
                weapon[(int)now_use_weapon_no].Throwing_Attack(now_use_weapon_no, look_allow);
            }

            //武器切り替え
            //if (keyboard.xKey.wasPressedThisFrame)
            //{
            //    now_use_weapon_no = (Weapon_no)((int)now_use_weapon_no + 1);
            //    Debug.Log(now_use_weapon_no);
            //    if (now_use_weapon_no == Weapon_no.over_id)
            //    {
            //        now_use_weapon_no = Weapon_no.knife;
            //    }
            //}
        }
    }

    private void Animation_Check()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Knife_Normal_Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
        {
            animator.SetBool(NormalAttackFlg, false);
        }
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("Knife_Special_Attack") && animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 0.5f)
        {
            animator.SetBool(SpecialAttackFlg, false);
        }
    }
}
