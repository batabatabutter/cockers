using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private PlayerStatus player;
    [SerializeField,HeaderAttribute("�X�e�[�^�X")] private int atk;
    [SerializeField] private float atk_enable_time;
    [SerializeField] private float atk_per_sec;

    private float now_atk_enable_time;
    private float now_cool_time;
    // Start is called before the first frame update

    //�U��������(���Ȃ����󂭂܂ł̍U���񐔂̂��܂�)
    private int atk_cnt;

    //�U���񐔂̗]��Z�Ɏg���萔
    [SerializeField] private const int Atk_MOD = 3;

    //�U�������񐔂����Ƃ��ɂ��Ȃ����󂭗�
    [SerializeField] private const int Sub_full_val = 1;

    //�`���[�W�U�����ǂ����̃t���O
    private bool charge_attack_flg;

    //����U�����ǂ����̃t���O
    private bool special_attack_flg;

    [SerializeField,HeaderAttribute("����U���̍U���{��")] private float special_attack_buff = 2.0f;
    [SerializeField, HeaderAttribute("����U���̍U������")] private float special_attack_time = 0.5f;

    [SerializeField, HeaderAttribute("�`���[�W�U���̍U���{��")] private float charge_attack_buff = 1.5f;
    [SerializeField, HeaderAttribute("�`���[�W�U���̍U������")] private float charge_attack_time = 0.5f;

    [SerializeField, HeaderAttribute("�����U���̍U���{��")] private float throwing_attack_buff = 1.0f;
    [SerializeField, HeaderAttribute("�����U���̍U������")] private float throwing_attack_time = 0.5f;
    [SerializeField, HeaderAttribute("�����U���̋���")] private float throwing_attack_dist = 5.0f;

    //������boxcollider�����O�Ɏ����Ă������Ƃŏ������Ԃ̒Z�k��}��
    BoxCollider box_collider;



    /// <summary>
    /// �����U���p
    /// </summary>

    //�����U���t���O
    private bool throwing_attack_flg;

    //����ԍ��ۑ��p
    Weapon_no weapon_no;

    private Vector3 first_pos;
    private Vector3 target_pos;

    //�����ۑ��p
    private bool allow;

    private float move_time = 0.5f;

    private float elapsed_time;

    private float rate;

    void Start()
    {
        box_collider = transform.GetComponent<BoxCollider>();
        box_collider.enabled = false;
        now_atk_enable_time = 0.0f;
        now_cool_time = 0.0f;
        charge_attack_flg = false;
        special_attack_flg = false;
        throwing_attack_flg = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (now_atk_enable_time > 0.0f)
        {
            now_atk_enable_time -= Time.deltaTime;
            
            if (throwing_attack_flg)
            {
                if (weapon_no == Weapon_no.knife)
                {
                    rate = Mathf.Clamp01((throwing_attack_time - now_atk_enable_time) / move_time);

                    transform.position = Vector3.Lerp(transform.position, target_pos, rate);
                }
            }

            if (now_atk_enable_time <= 0.0f)
            {
                now_atk_enable_time = 0.0f;
                box_collider.enabled = false;
                charge_attack_flg = false;
                special_attack_flg = false;
                if (throwing_attack_flg)
                {
                    transform.localPosition = first_pos;
                }
                throwing_attack_flg = false;
            }
        }
        if (now_cool_time > 0.0f)
        {
            now_cool_time -= Time.deltaTime;
            if (now_cool_time <= 0.0f)
            {
                now_cool_time = 0.0f;
            }
        }
    }

    //�ʏ�U���̍U���J�n�֐�
    public void Attack()
    {
        if (now_cool_time > 0.0f) return;
        box_collider.enabled = true;
        now_atk_enable_time = atk_enable_time;
        now_cool_time = 1.0f / (atk_per_sec);
        atk_cnt++;
        if (atk_cnt % Atk_MOD == 0) player.Sub_full_stomach(Sub_full_val);
    }

    //�`���[�W�U���̍U���J�n�֐�
    public void Charge_Attack()
    {
        if (now_cool_time > 0.0f) return;
        box_collider.enabled = true;
        now_atk_enable_time = charge_attack_time;
        now_cool_time = charge_attack_time + 1.0f / (atk_per_sec);
        charge_attack_flg = true;
        atk_cnt++;
        if (atk_cnt % Atk_MOD == 0) player.Sub_full_stomach(Sub_full_val);
    }

    //����U���p�̍U���J�n�֐�
    public void Special_Attack()
    {
        if (now_cool_time > 0.0f) return;
        box_collider.enabled = true;
        now_atk_enable_time = special_attack_time;
        now_cool_time = special_attack_time + 1.0f / (atk_per_sec);
        special_attack_flg = true;
        atk_cnt++;
        if (atk_cnt % Atk_MOD == 0) player.Sub_full_stomach(Sub_full_val);
    }

    public void Throwing_Attack(Weapon_no weapon_no,bool allow) {
        this.weapon_no = weapon_no;
        this.allow = allow;
        first_pos = transform.localPosition;
        target_pos = transform.position;
        if (allow) target_pos.x += throwing_attack_dist;
        else target_pos.x -= throwing_attack_dist;
        now_atk_enable_time = throwing_attack_time;
        now_cool_time = throwing_attack_time + 1.0f / (atk_per_sec);
        throwing_attack_flg = true;
        atk_cnt++;
        if (atk_cnt % Atk_MOD == 0) player.Sub_full_stomach(Sub_full_val);
    }

    private void OnTriggerEnter(Collider other)
    {
        int atk_value = (int)(atk * (1 + player.Get_atk() * 0.01));
        atk_value += player.Get_now_atk();

        //�U���̎�ނɂ���čU���͂Ƀo�t���|����
        if (charge_attack_flg) atk_value = (int)(atk_value * charge_attack_buff);
        if (special_attack_flg) atk_value = (int)(atk_value * special_attack_buff);

        //�^�O��enemy�Ȃ�
        if (other.CompareTag("Enemy"))
        {
            //�G�Ƀ_���[�W��^���鏈��
            other.GetComponent<Enemy>().Damage(atk_value);
        }
        else if(other.CompareTag("Boss"))
        {
            //�{�X�Ƀ_���[�W��^���鏈��
            other.GetComponent<Boss>().Damage(atk_value);
        }
    }
}
