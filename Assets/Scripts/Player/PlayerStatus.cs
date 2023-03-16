using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public enum Weapon_no
{
    knife,
    flypan,

    over_id
}

public class PlayerStatus : MonoBehaviour
{
    //栄養素の構造体
    public struct Status
    {
        public int hp;  //炭水化物
        public int atk; //タンパク質
        public int spd; //無機質

        public void Zero()
        {
            hp = 0;
            atk = 0;
            spd = 0;
        }

        public void Initialize(int hp, int atk, int spd)
        {
            this.hp = hp;
            this.atk = atk;
            this.spd = spd;
        }
    }

    public static Status status;

    private const int Min_nut = 0;
    private const int Max_nut = 100;

    [SerializeField, HeaderAttribute("基礎ステータス")] private int def_hp = 100;    //体力
    [SerializeField] private int def_atk = 10;                                       //攻撃力
    [SerializeField] private int def_spd = 10;                                       //スピード
    [SerializeField] private float invincible_time = 1.0f;                           //無敵時間

    [SerializeField, ReadOnly, HeaderAttribute("現在ステータス(編集するな)")] private int now_hp;        //体力
    [SerializeField, ReadOnly] private int now_atk;                                                      //攻撃力
    [SerializeField, ReadOnly] private int now_spd;                                                      //スピード
    [SerializeField, ReadOnly] private int full_stomach;                                             //満腹度
    [SerializeField, ReadOnly] private float now_invincible_time;                                    //無敵時間



    // Start is called before the first frame update
    void Start()
    {
        status.Zero();
        //status.Initialize(def_hp, def_atk, def_spd);
        now_hp = def_hp;
        now_atk = def_atk;
        now_spd = def_spd;
        full_stomach = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (now_invincible_time > 0.0f)
        {
            now_invincible_time -= Time.deltaTime;
            if (now_invincible_time <= 0.0f)
            {
                now_invincible_time = 0.0f;
            }
        }
    }

    //値の設定
    public void Set_carbohydrates(int val) { status.hp = val; }
    public void Set_proteins(int val) { status.atk = val; }
    public void Set_minerals(int val) { status.spd = val; }

    //値の加算
    public void Add_hp(int val)
    {
        status.hp += val;
        now_hp = Mathf.CeilToInt(def_hp * (1 + (status.hp * 0.01f)));
        status.hp = Mathf.Clamp(status.hp, Min_nut, Max_nut);
    }
    public void Add_atk(int val)
    {
        status.atk += val;
        now_atk = Mathf.CeilToInt(def_atk * (1 + (status.atk * 0.01f)));
        status.atk = Mathf.Clamp(status.atk, Min_nut, Max_nut);
    }

    public void Add_spd(int val)
    {
        status.spd += val;
        now_spd = Mathf.CeilToInt(def_spd * (1 + (status.spd * 0.01f)));
        status.spd = Mathf.Clamp(status.spd, Min_nut, Max_nut);
    }

    public void Add_full_stomach(int val)
    {
        full_stomach += val;
        full_stomach = Mathf.Clamp(full_stomach, Min_nut, Max_nut);
    }

    //値の減算
    public void Sub_hp(int val)
    {
        status.hp -= val;
        now_hp = Mathf.CeilToInt(def_hp * (1 + (status.hp * 0.01f)));
        status.hp = Mathf.Clamp(status.hp, Min_nut, Max_nut);
    }
    public void Sub_atk(int val)
    {
        status.atk -= val;
        now_atk = Mathf.CeilToInt(def_atk * (1 + (status.atk * 0.01f)));
        status.atk = Mathf.Clamp(status.atk, Min_nut, Max_nut);
    }

    public void Sub_spd(int val)
    {
        status.spd -= val;
        now_spd = Mathf.CeilToInt(def_spd * (1 + (status.spd * 0.01f)));
        status.spd = Mathf.Clamp(status.spd, Min_nut, Max_nut);
    }

    public void Sub_full_stomach(int val)
    {
        full_stomach -= val;
        full_stomach = Mathf.Clamp(full_stomach, Min_nut, Max_nut);
    }


    //値の取得
    public int Get_hp() { return status.hp; }
    public int Get_atk() { return status.atk; }

    public int Get_spd() { return status.spd; }

    public int Get_now_hp() { return now_hp; }
    public int Get_now_atk() { return now_atk; }
    public int Get_now_spd() { return now_spd; }
    public int Get_full_stomach() { return full_stomach; }

    //敵からダメージを負ったとき
    public void Damage(int val)
    {
        //無敵時間中ならダメージを受けない
        if (now_invincible_time > 0.0f) return;

        //シールドチェック
        Shield shield = transform.GetComponent<Shield>();
        //シールド展開中ならダメージを受けない
        if (shield.Get_is_shield()) return;

        //アーマーがついてたらダメージ軽減
        Armor armor = transform.GetComponent<Armor>();
        armor.damage_reduction(ref val);

        Mathf.Clamp(val, 0, 9999);
        now_hp -= val;
        now_hp = Mathf.Clamp(now_hp, 0, 10000);
        now_invincible_time = invincible_time;
    }
}
