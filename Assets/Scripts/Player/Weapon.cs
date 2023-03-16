using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private PlayerStatus player;
    [SerializeField,HeaderAttribute("ƒXƒe[ƒ^ƒX")] private int atk;
    [SerializeField] private float atk_enable_time;
    [SerializeField] private float atk_per_sec;

    private float now_atk_enable_time;
    private float now_cool_time;
    // Start is called before the first frame update

    //UŒ‚‚µ‚½‰ñ”(‚¨‚È‚©‚ª‹ó‚­‚Ü‚Å‚ÌUŒ‚‰ñ”‚Ì‚ ‚Ü‚è)
    private int atk_cnt;

    //UŒ‚‰ñ”‚Ì—]‚èZ‚Ég‚¤’è”
    [SerializeField] private const int Atk_MOD = 3;

    //UŒ‚‚ğˆê’è‰ñ”‚µ‚½‚Æ‚«‚É‚¨‚È‚©‚ª‹ó‚­—Ê
    [SerializeField] private const int Sub_full_val = 1;

    //ƒ`ƒƒ[ƒWUŒ‚‚©‚Ç‚¤‚©‚Ìƒtƒ‰ƒO
    private bool charge_attack_flg;

    //“ÁêUŒ‚‚©‚Ç‚¤‚©‚Ìƒtƒ‰ƒO
    private bool special_attack_flg;

    [SerializeField,HeaderAttribute("“ÁêUŒ‚‚ÌUŒ‚”{—¦")] private float special_attack_buff = 2.0f;
    [SerializeField, HeaderAttribute("“ÁêUŒ‚‚ÌUŒ‚ŠÔ")] private float special_attack_time = 0.5f;

    [SerializeField, HeaderAttribute("ƒ`ƒƒ[ƒWUŒ‚‚ÌUŒ‚”{—¦")] private float charge_attack_buff = 1.5f;
    [SerializeField, HeaderAttribute("ƒ`ƒƒ[ƒWUŒ‚‚ÌUŒ‚ŠÔ")] private float charge_attack_time = 0.5f;

    [SerializeField, HeaderAttribute("“Š±UŒ‚‚ÌUŒ‚”{—¦")] private float throwing_attack_buff = 1.0f;
    [SerializeField, HeaderAttribute("“Š±UŒ‚‚ÌUŒ‚ŠÔ")] private float throwing_attack_time = 0.5f;
    [SerializeField, HeaderAttribute("“Š±UŒ‚‚Ì‹——£")] private float throwing_attack_dist = 5.0f;

    //©•ª‚Ìboxcollider‚ğ–‘O‚É‚Á‚Ä‚¨‚­‚±‚Æ‚Åˆ—ŠÔ‚Ì’Zk‚ğ}‚é
    BoxCollider box_collider;



    /// <summary>
    /// “Š±UŒ‚—p
    /// </summary>

    //“Š±UŒ‚ƒtƒ‰ƒO
    private bool throwing_attack_flg;

    //•Ší”Ô†•Û‘¶—p
    Weapon_no weapon_no;

    private Vector3 first_pos;
    private Vector3 target_pos;

    //Œü‚«•Û‘¶—p
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

    //’ÊíUŒ‚‚ÌUŒ‚ŠJnŠÖ”
    public void Attack()
    {
        if (now_cool_time > 0.0f) return;
        box_collider.enabled = true;
        now_atk_enable_time = atk_enable_time;
        now_cool_time = 1.0f / (atk_per_sec);
        atk_cnt++;
        if (atk_cnt % Atk_MOD == 0) player.Sub_full_stomach(Sub_full_val);
    }

    //ƒ`ƒƒ[ƒWUŒ‚‚ÌUŒ‚ŠJnŠÖ”
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

    //“ÁêUŒ‚—p‚ÌUŒ‚ŠJnŠÖ”
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

        //UŒ‚‚Ìí—Ş‚É‚æ‚Á‚ÄUŒ‚—Í‚Éƒoƒt‚ğŠ|‚¯‚é
        if (charge_attack_flg) atk_value = (int)(atk_value * charge_attack_buff);
        if (special_attack_flg) atk_value = (int)(atk_value * special_attack_buff);

        //ƒ^ƒO‚ªenemy‚È‚ç
        if (other.CompareTag("Enemy"))
        {
            //“G‚Éƒ_ƒ[ƒW‚ğ—^‚¦‚éˆ—
            other.GetComponent<Enemy>().Damage(atk_value);
        }
        else if(other.CompareTag("Boss"))
        {
            //ƒ{ƒX‚Éƒ_ƒ[ƒW‚ğ—^‚¦‚éˆ—
            other.GetComponent<Boss>().Damage(atk_value);
        }
    }
}
