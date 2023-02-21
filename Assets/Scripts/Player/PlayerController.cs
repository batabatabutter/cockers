using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //rigidbody
    Rigidbody rigid;

    //プレイヤーの移動スピード、速度倍率
    [SerializeField,HeaderAttribute("移動ステータス")] private float move_speed = 1.0f;
    [SerializeField] private float speed_force = 4.0f;
    [SerializeField] private float jump_speed = 1.0f;
    [SerializeField] private float jump_force = 4.0f;

    [SerializeField, HeaderAttribute("所持武器")] private List<GameObject> weapons_list;

    //プレイヤーの速度
    private float horizontal_speed;
    private float vertical_speed;

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

    //現在の使用武器番号
    private int now_use_weapon_no;

    // Start is called before the first frame update
    void Start()
    {
        rigid = transform.GetComponent<Rigidbody>();
        isJumping = true;
        isJumpFinish = true;
        isGround = false;
        now_use_weapon_no = 0;
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        horizontal_speed = 0;
        vertical_speed = -jump_speed;

        if (keyboard != null)
        {
            if (keyboard.rightArrowKey.isPressed)
            {
                horizontal_speed = move_speed;
            }
            if (keyboard.leftArrowKey.isPressed)
            {
                horizontal_speed = -move_speed;
            }
        }

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

        //着地してるとき、ジャンプする
        if (isGround && keyboard.upArrowKey.wasPressedThisFrame)
        {
            isJumping = true;
            jump_time = 0.0f;
            vertical_speed = jump_speed;
        }

        if (isJumping && 
            !isJumpFinish && 
            keyboard.upArrowKey.isPressed && 
            jump_time < max_jump_time)
        {
            vertical_speed = jump_speed;
        }

        if(isJumping && !isJumpFinish && keyboard.upArrowKey.wasReleasedThisFrame)
        {
            isJumpFinish = true;
        }

        //攻撃処理
        if (keyboard.xKey.wasPressedThisFrame) {
            weapons_list[now_use_weapon_no].GetComponent<Weapon>().Attack();
        }

        if (keyboard.cKey.wasPressedThisFrame)
        {
            now_use_weapon_no++;
            now_use_weapon_no %= weapons_list.Count;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 velocity = Vector3.zero;
        Vector3 now_velocity = rigid.velocity;
        Vector3 now_x = new Vector3(now_velocity.x, 0, 0);
        Vector3 now_y = new Vector3(0, now_velocity.y, 0);

        velocity.x = move_speed * horizontal_speed;
        rigid.AddForce(speed_force * (velocity - now_x));

        velocity = Vector3.zero;

        velocity.y = jump_speed * vertical_speed;
        if (isJumping)
        {
            jump_time += Time.fixedDeltaTime;
        }

        rigid.AddForce(jump_force * (velocity - now_y));
    }

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
}
