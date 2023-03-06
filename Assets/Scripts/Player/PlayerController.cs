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
    [SerializeField] private float jump_speed = 1.0f;
    [SerializeField] private float jump_force = 4.0f;

    [SerializeField, HeaderAttribute("所持武器")] private List<GameObject> weapons_list;



    /// <summary> ///////////////////////
    /// 移動関連
    /// </summary> ///////////////////////

    //プレイヤーの速度
    private float horizontal_speed;
    private float vertical_speed;

    //ダッシュクラス
    Dash dash;

    //プレイヤーがどっち向いてるか(true : 右,false : 左)
    private bool look_allow;



    /// <summary> ///////////////////////
    /// ジャンプ関連
    /// </summary> //////////////////////

    //ジャンプの秒数
    private float jump_time;
    [SerializeField] private float max_jump_time = 0.5f;

    //ジャンプしてるか
    private bool isJumping;
    private bool isJumpFinish;

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
        isJumping = true;
        isJumpFinish = true;
        isGround = false;
        now_use_weapon_no = 0;
        look_allow = true;
        dash = transform.GetComponent<Dash>();
    }

    private void Update()
    {
        //移動速度の初期化
        horizontal_speed = 0;
        vertical_speed = -jump_speed;

        //着地判定
        CheckGround();

        //着地してるとき、ジャンプ可能にする
        if (isGround)
        {
            jump_time = 0.0f;
            isJumpFinish = false;
            vertical_speed = 0;
        }

        //ジャンプ後、落下する処理
        if(isJumpFinish)
        {
            //vertical_speed = -jump_speed;
            isJumping = false;
        }

        //ダッシュ中なら浮いてても落ちないように
        if (dash.Get_is_dash())
        {
            Debug.Log("A");
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
        Vector3 now_y = new Vector3(0, now_velocity.y, 0);

        velocity.x = move_speed * horizontal_speed;
        velocity.z = 0;
        rigid.AddForce(speed_force * (velocity - now_x));

        velocity = Vector3.zero;

        velocity.y = jump_speed * vertical_speed;
        if (isJumping)
        {
            jump_time += Time.fixedDeltaTime;
        }
        velocity.z = 0;
        rigid.AddForce(jump_force * (velocity - now_y));
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
            if (isGround && keyboard.upArrowKey.wasPressedThisFrame)
            {
                isJumping = true;
                jump_time = 0.0f;
                vertical_speed = jump_speed;
            }

            //長押しでジャンプの高さが高くなる
            if (isJumping &&
                !isJumpFinish &&
                keyboard.upArrowKey.isPressed &&
                jump_time < max_jump_time)
            {
                vertical_speed = jump_speed;
            }

            //途中でキーを離すとその時点で落ちる
            if (isJumping && !isJumpFinish && keyboard.upArrowKey.wasReleasedThisFrame)
            {
                isJumpFinish = true;
            }

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
        }
    }
}
