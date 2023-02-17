using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    //rigidbody
    Rigidbody rigid;

    //�v���C���[�̈ړ��X�s�[�h�A���x�{��
    [SerializeField] private float move_speed = 1.0f;
    [SerializeField] private float speed_force = 4.0f;
    [SerializeField] private float jump_speed = 1.0f;
    [SerializeField] private float jump_force = 4.0f;

    //�v���C���[�̑��x
    private float horizontal_speed;
    private float vertical_speed;

    //�W�����v�̕b��
    private float jump_time;
    [SerializeField] private float max_jump_time = 0.5f;

    //�W�����v���Ă邩
    private bool isJumping;

    //���n���Ă邩
    private bool isGround;

    //ray�̋���
    [SerializeField] private float ray_dist = 0.5f;

    // Start is called before the first frame update
    void Start()
    {
        rigid = transform.GetComponent<Rigidbody>();
    }

    private void Update()
    {
        var keyboard = Keyboard.current;
        horizontal_speed = 0;
        vertical_speed = 0;

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
        //���n���Ă�Ƃ��A�W�����v�\�ɂ���
        if (isGround)
        {
            jump_time = 0.0f;
        }

        //�W�����v��A�������鏈��
        else if(!keyboard.upArrowKey.isPressed || jump_time >= max_jump_time)
        {
            vertical_speed = -jump_speed;
            isJumping = false;
        }

        //���n���Ă�Ƃ��A�W�����v����
        if (isGround && keyboard.upArrowKey.wasPressedThisFrame)
        {
            isJumping = true;
            jump_time = 0.0f;
            vertical_speed = jump_speed;
        }
        if (isJumping && keyboard.upArrowKey.isPressed && jump_time < max_jump_time)
        {
            vertical_speed = jump_speed;
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

        rigid.AddForce(speed_force * (velocity - now_y));
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
