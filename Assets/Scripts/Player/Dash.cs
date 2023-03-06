using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    //rigidbody
    [SerializeField] private Rigidbody rigid;

    //�����������_�b�V���������Ă��邩
    [SerializeField] private bool have_dash;

    //���_�b�V���ł��邩
    [SerializeField] private bool can_dash;

    //�_�b�V�����Ă邩
    private bool is_dash;

    //�_�b�V���̌��݃N�[���^�C��
    private float cool_time;

    //�_�b�V���̃N�[���^�C��
    [SerializeField] private const float Dash_cool_time = 3.0f;

    //�ڕW�̃|�W�V����
    private Vector3 target_pos;

    //�������(X�̂�)
    [SerializeField, HeaderAttribute("�_�b�V�����ɉ������")] private float dash_force = 5.0f;

    //�������(�s��)
    private Vector3 dash_force_vec;

    //�_�b�V�����鎞��
    [SerializeField, HeaderAttribute("�_�b�V������")] private float dash_time = 0.4f;

    //�_�b�V���̎c�莞��
    private float now_dash_time;

    //�v���C���[���ǂ��������Ă邩(true : �E,false : ��)
    private bool look_allow;

    // Start is called before the first frame update
    void Start()
    {
        rigid = transform.GetComponent<Rigidbody>();
        have_dash = false;
        can_dash = false;
        is_dash = false;
        cool_time = 0.0f;
        dash_force_vec = Vector3.zero;
        now_dash_time = 0.0f;
    }

    //�A�b�v�f�[�g
    //�N�[���^�C���̌����ɗp����
    void Update()
    {
        if (!have_dash) return;

        if (0.0f < cool_time)
        {
            cool_time -= Time.deltaTime;
            if (cool_time < 0.0f)
            {
                can_dash = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (is_dash)
        {
            rigid.AddForce(dash_force_vec, ForceMode.Impulse);
            now_dash_time += Time.deltaTime;
            Check_end_dash();
        }
    }

    //�_�b�V�����Q�b�g�����Ƃ��Ɏg��
    public void Allow_dash_to_player()
    {
        have_dash = true;
        can_dash = true;
    }

    //�_�b�V������
    public void Start_Dash(bool allow)
    {
        is_dash = true;
        can_dash = false;
        if(allow) dash_force_vec.x = dash_force;
        else dash_force_vec.x = -dash_force;
        rigid.constraints |= RigidbodyConstraints.FreezePositionY;
    }

    //�_�b�V���I���
    private void End_dash()
    {
        is_dash = false;
        cool_time = Dash_cool_time;
        rigid.constraints = RigidbodyConstraints.None;
        rigid.constraints |= RigidbodyConstraints.FreezeRotation;
        rigid.constraints |= RigidbodyConstraints.FreezePositionZ;
        now_dash_time = 0.0f;
        rigid.velocity = Vector3.zero;
    }

    //�_�b�V�����I����������`�F�b�N����
    public void Check_end_dash()
    {
        if (now_dash_time >= dash_time)
        {
            End_dash();
        }
    }

    //�_�b�V�����\����Ԃ�
    public bool Get_now_can_dash()
    {
        return can_dash & !is_dash;
    }

    //�_�b�V�������ǂ�����Ԃ�
    public bool Get_is_dash()
    {
        return is_dash;
    }
}
