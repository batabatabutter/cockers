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
    [SerializeField,HeaderAttribute("移動ステータス")] private float move_speed = 1.0f;
    [SerializeField] private float speed_force = 4.0f;
    [SerializeField] private float jump_force = 4.0f;
    [SerializeField] private float gravity = 9.8f;
    [SerializeField] private float min_vertical_vel = -15.0f;

    [SerializeField, HeaderAttribute("所持武器")] private List<GameObject> weapons_list;



    /// <summary> ///////////////////////
    /// 移動関連
    /// </summary> ///////////////////////

    //プレイヤーの速度
    private float horizontal_speed;
    private float vertical_speed;

    //ダッシュクラス
    Dash dash;

    //ダブルジャンプクラス
    DoubleJump doublejump;

    //シールドクラス(バリア)
    Shield shield;

    //プレイヤーがどっち向いてるか(true : 右,false : 左)
    private bool look_allow;



    /// <summary> ///////////////////////
    /// ジャンプ関連
    /// </summary> //////////////////////

    //ジャンプの回数
    private int jump_cnt;

    //上昇中か
    private bool isJumping;

    //着地してるか
    private bool isGround;

    //rayの距離
    [SerializeField] private float ray_dist = 0.5f;



    /// <summary> ///////////////////////
    /// 攻撃関連
    /// </summary> ///////////////////////
    
    //現在の使用武器番号
    private int now_use_weapon_no;



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
        now_use_weapon_no = 0;
        look_allow = true;
        dash = transform.GetComponent<Dash>();
        doublejump = transform.GetComponent<DoubleJump>();
        shield = transform.GetComponent<Shield>();
    }

    private void Update()
    {
        //移動速度の初期化
        horizontal_speed = 0;
        vertical_speed = -jump_force;

        //落下し始めたら着地判定を行うようにする
        if (isJumping && rigid.velocity.y <= 0.0f) isJumping = false;

        //着地判定
        CheckGround();

        //着地してるとき、ジャンプ可能にする
        if (isGround)
        {
            doublejump.End_double_jump();
            vertical_speed = 0;
            jump_cnt = 0;
        }

        //ダッシュ中なら浮いてても落ちないように
        if (dash.Get_is_dash())
        {
            vertical_speed = 0;
        }

        //キーボード操作
        Key_Controll();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 now_velocity = rigid.velocity;
        Vector3 now_x = new Vector3(now_velocity.x, 0, 0);

        if (!isGround)
        {
            now_velocity.y -= gravity * Time.deltaTime;
            now_velocity.y = Mathf.Clamp(now_velocity.y, min_vertical_vel, 1000000.0f);
        }

        rigid.velocity = now_velocity;

        velocity.x = move_speed * horizontal_speed;
        rigid.AddForce(speed_force * (velocity - now_x));

        velocity = Vector3.zero;
    }

    //地面についてるかの確認
    private void CheckGround()
    {
        if (isJumping && rigid.velocity.y > 0)
        {
            isGround = false;
            return;
        }
        Vector3 rayPosition = transform.position;
        Ray ray = new Ray(rayPosition, Vector3.down);
        isGround = Physics.Raycast(ray, ray_dist);
    }

    //キーボード操作
    private void Key_Controll() {
        var keyboard = Keyboard.current;
        
        //もしダッシュしてたら、操作を切る
        if (dash.Get_is_dash()) return;

        if (keyboard != null)
        {
            //移動
            if (keyboard.rightArrowKey.isPressed)
            {
                horizontal_speed = move_speed;
            }
            if (keyboard.leftArrowKey.isPressed)
            {
                horizontal_speed = -move_speed;
            }

            //キャラクターの回転
            if (keyboard.rightArrowKey.wasPressedThisFrame)
            {
                rigid.rotation = Quaternion.Euler(0, 0, 0);
                look_allow = true;
            }

            //キャラクターの回転
            if (keyboard.leftArrowKey.wasPressedThisFrame)
            {
                rigid.rotation = Quaternion.Euler(0, 180, 0);
                look_allow = false;
            }

            //着地してるとき、ジャンプする
            if (keyboard.upArrowKey.wasPressedThisFrame)
            {
                if (isGround && jump_cnt == 0)
                {
                    jump_cnt = 1;
                    rigid.AddForce(new Vector3(0, jump_force, 0), ForceMode.Impulse);
                    isJumping = true;
                    doublejump.End_double_jump();
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

            //攻撃処理
            if (keyboard.zKey.wasPressedThisFrame)
            {
                weapons_list[now_use_weapon_no].GetComponent<Weapon>().Attack();
            }

            //武器切り替え
            if (keyboard.xKey.wasPressedThisFrame)
            {
                now_use_weapon_no++;
                now_use_weapon_no %= weapons_list.Count;
            }

            //ダッシュ
            if (keyboard.leftShiftKey.wasPressedThisFrame && dash.Get_now_can_dash())
            {
                dash.Start_Dash(look_allow);
                horizontal_speed = 0;
            }

            //シールド展開
            if(keyboard.sKey.wasPressedThisFrame && shield.Get_now_can_shield())
            {
                shield.Start_shield();
            }
        }
    }
}
