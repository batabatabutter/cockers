using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private PlayerStatus player;
    [SerializeField,HeaderAttribute("ステータス")] private int atk;
    [SerializeField] private float atk_enable_time;
    [SerializeField] private float atk_per_sec;

    private float now_atk_enable_time;
    private float now_cool_time;
    // Start is called before the first frame update

    //攻撃した回数(おなかが空くまでの攻撃回数のあまり)
    private int atk_cnt;

    //攻撃回数の余り算に使う定数
    [SerializeField] private const int Atk_MOD = 3;

    //攻撃を一定回数したときにおなかが空く量
    [SerializeField] private const int Sub_full_val = 1;

    //チャージ攻撃かどうかのフラグ
    private bool charge_attack_flg;

    //特殊攻撃かどうかのフラグ
    private bool special_attack_flg;

    [SerializeField,HeaderAttribute("特殊攻撃の攻撃倍率")] private float special_attack_buff = 2.0f;
    [SerializeField, HeaderAttribute("特殊攻撃の攻撃時間")] private float special_attack_time = 0.5f;

    [SerializeField, HeaderAttribute("チャージ攻撃の攻撃倍率")] private float charge_attack_buff = 1.5f;
    [SerializeField, HeaderAttribute("チャージ攻撃の攻撃時間")] private float charge_attack_time = 0.5f;

    [SerializeField, HeaderAttribute("投擲攻撃の攻撃倍率")] private float throwing_attack_buff = 1.0f;
    [SerializeField, HeaderAttribute("投擲攻撃の攻撃時間")] private float throwing_attack_time = 0.5f;
    [SerializeField, HeaderAttribute("投擲攻撃の距離")] private float throwing_attack_dist = 5.0f;

    //自分のboxcolliderを事前に持っておくことで処理時間の短縮を図る
    BoxCollider box_collider;



    /// <summary>
    /// 投擲攻撃用
    /// </summary>

    //投擲攻撃フラグ
    private bool throwing_attack_flg;

    //武器番号保存用
    Weapon_no weapon_no;

    private Vector3 first_pos;
    private Vector3 target_pos;

    //向き保存用
    private bool allow;

    private float move_time = 0.5f;

    private float elapsed_time;

    private float rate;

    Vector3 fly_pan_vel;
    Rigidbody rb;

    private bool finish_flg;

    void Start()
    {
        box_collider = transform.GetComponent<BoxCollider>();
        Debug.Log(box_collider);
        box_collider.enabled = false;
        now_atk_enable_time = 0.0f;
        now_cool_time = 0.0f;
        charge_attack_flg = false;
        special_attack_flg = false;
        throwing_attack_flg = false;
        finish_flg = false;
        rb = transform.GetComponent<Rigidbody>();
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
                else if (weapon_no == Weapon_no.flypan)
                {
                    Vector3 now_pos = transform.position;
                    now_pos += fly_pan_vel * Time.deltaTime;
                    fly_pan_vel.y -= 9.81f * Time.deltaTime;
                    transform.position = now_pos;
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
                finish_flg = true;
            }
        }
        if (now_cool_time > 0.0f)
        {
            now_cool_time -= Time.deltaTime;
            if (now_cool_time <= 0.0f)
            {
                now_cool_time = 0.0f;
                box_collider.enabled=false;
            }
        }
    }

    //通常攻撃の攻撃開始関数
    public bool Attack()
    {
        if (now_cool_time > 0.0f) return false;
        box_collider.enabled=true;
        now_atk_enable_time = atk_enable_time;
        now_cool_time = 1.0f / (atk_per_sec);
        atk_cnt++;
        if (atk_cnt % Atk_MOD == 0) player.Sub_full_stomach(Sub_full_val);
        return true;
    }

    //チャージ攻撃の攻撃開始関数
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

    //特殊攻撃用の攻撃開始関数
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
        box_collider.enabled = true;
        if (weapon_no == Weapon_no.knife)
        {
            target_pos = transform.position;
            if (allow) target_pos.x += throwing_attack_dist;
            else target_pos.x -= throwing_attack_dist;
            now_atk_enable_time = throwing_attack_time;
            now_cool_time = throwing_attack_time + 1.0f / (atk_per_sec);
        }
        else if (weapon_no == Weapon_no.flypan)
        {
            fly_pan_vel = new Vector3(1.0f, 1.0f, 0.0f);
            fly_pan_vel *= 5.0f;
            if (!allow) fly_pan_vel.x *= -1;
            now_atk_enable_time = throwing_attack_time * 100;
        }
        
        throwing_attack_flg = true;
        atk_cnt++;
        if (atk_cnt % Atk_MOD == 0) player.Sub_full_stomach(Sub_full_val);
    }

    private void OnTriggerEnter(Collider other)
    {
        int atk_value = (int)(atk * (1 + player.Get_atk() * 0.01));
        atk_value += player.Get_now_atk();

        //攻撃の種類によって攻撃力にバフを掛ける
        if (charge_attack_flg) atk_value = (int)(atk_value * charge_attack_buff);
        if (special_attack_flg) atk_value = (int)(atk_value * special_attack_buff);

        //タグがenemyなら
        if (other.CompareTag("Enemy"))
        {
            //敵にダメージを与える処理
            other.GetComponent<Enemy>().Damage(atk_value);
        }
        else if(other.CompareTag("Boss"))
        {
            //ボスにダメージを与える処理
            other.GetComponent<Boss>().Damage(atk_value);
        }

        if(throwing_attack_flg && weapon_no == Weapon_no.flypan)
        {
            transform.localPosition = first_pos;
            now_atk_enable_time = 0.0f;
            box_collider.enabled = false;
            throwing_attack_flg = false;
            finish_flg = true;
            now_cool_time = 1.0f / (atk_per_sec);
        }
    }

    public bool Get_is_attack_now()
    {
        return box_collider.enabled;
    }

    public bool Get_finish_flg()
    {
        return finish_flg;
    }

    public void Reset_finish_flg()
    {
        finish_flg = false;
    }

}
