using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    //栄養素の構造体
    public struct Nutrients
    {
        public int carbohydrates;  //炭水化物
        public int proteins;       //タンパク質
        public int lipid;          //脂質
        public int vitamins;       //ビタミン
        public int minerals;       //無機質

        public void Zero()
        {
            carbohydrates = 0;
            proteins = 0;
            lipid = 0;
            vitamins = 0;
            minerals = 0;
        }
    }

    public static Nutrients nutrients;

    private const int Min_nut = 0;
    private const int Max_nut = 100;

    [SerializeField, HeaderAttribute("基礎ステータス")] private int def_hp = 100;    //体力
    [SerializeField] private int def_atk = 10;                                       //攻撃力
    [SerializeField] private int def_dfc = 10;                                       //防御力
    [SerializeField] private int def_atk_itv = 1;                                    //攻撃回数倍率
    [SerializeField] private int def_spd = 10;                                       //スピード
    [SerializeField] private float invincible_time = 1.0f;                           //無敵時間

    [SerializeField, ReadOnly, HeaderAttribute("現在ステータス(編集するな)")] private int hp;        //体力
    [SerializeField, ReadOnly] private int atk;                                                      //攻撃力
    [SerializeField, ReadOnly] private int dfc;                                                      //防御力
    [SerializeField, ReadOnly] private int atk_itv;                                                  //攻撃回数倍率
    [SerializeField, ReadOnly] private int spd;                                                      //スピード
    [SerializeField, ReadOnly] private int full_stomach;                                             //満腹度
    [SerializeField, ReadOnly] private float now_invincible_time;                                    //無敵時間



    // Start is called before the first frame update
    void Start()
    {
        nutrients.Zero();
        hp = def_hp;
        atk = def_atk;
        dfc = def_dfc;
        atk_itv = def_atk_itv;
        spd = def_spd;
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
    public void Set_carbohydrates(int val) { nutrients.carbohydrates = val; }
    public void Set_proteins(int val) { nutrients.proteins = val; }
    public void Set_lipid(int val) { nutrients.lipid = val; }
    public void Set_vitamins(int val) { nutrients.vitamins = val; }
    public void Set_minerals(int val) { nutrients.minerals = val; }

    //値の加算
    public void Add_carbohydrates(int val)
    {
        nutrients.carbohydrates += val;
        hp = Mathf.CeilToInt(def_hp * (1 + (nutrients.carbohydrates * 0.01f)));
        nutrients.carbohydrates = Mathf.Clamp(nutrients.carbohydrates, Min_nut, Max_nut);
    }
    public void Add_proteins(int val)
    {
        nutrients.proteins += val;
        atk = Mathf.CeilToInt(def_atk * (1 + (nutrients.proteins * 0.01f)));
        nutrients.proteins = Mathf.Clamp(nutrients.proteins, Min_nut, Max_nut);
    }
    public void Add_lipid(int val)
    {
        nutrients.lipid += val;
        dfc = Mathf.CeilToInt(def_dfc * (1 + (nutrients.lipid * 0.01f)));
        nutrients.lipid = Mathf.Clamp(nutrients.lipid, Min_nut, Max_nut);
    }
    public void Add_vitamins(int val)
    {
        nutrients.vitamins += val;
        atk_itv = Mathf.CeilToInt(def_atk_itv * (1 + (nutrients.vitamins * 0.01f)));
        nutrients.vitamins = Mathf.Clamp(nutrients.vitamins, Min_nut, Max_nut);
    }
    public void Add_minerals(int val)
    {
        nutrients.minerals += val;
        spd = Mathf.CeilToInt(def_spd * (1 + (nutrients.minerals * 0.01f)));
        nutrients.minerals = Mathf.Clamp(nutrients.minerals, Min_nut, Max_nut);
    }

    //値の減算
    public void Sub_carbohydrates(int val)
    {
        nutrients.carbohydrates -= val;
        hp = Mathf.CeilToInt(def_hp * (1 + (nutrients.carbohydrates * 0.01f)));
        nutrients.carbohydrates = Mathf.Clamp(nutrients.carbohydrates, Min_nut, Max_nut);
    }
    public void Sub_proteins(int val)
    {
        nutrients.proteins -= val;
        atk = Mathf.CeilToInt(def_atk * (1 + (nutrients.proteins * 0.01f)));
        nutrients.proteins = Mathf.Clamp(nutrients.proteins, Min_nut, Max_nut);
    }
    public void Sub_lipid(int val)
    {
        nutrients.lipid -= val;
        dfc = Mathf.CeilToInt(def_dfc * (1 + (nutrients.lipid * 0.01f)));
        nutrients.lipid = Mathf.Clamp(nutrients.lipid, Min_nut, Max_nut);
    }
    public void Sub_vitamins(int val)
    {
        nutrients.vitamins -= val;
        atk_itv = Mathf.CeilToInt(def_atk_itv * (1 + (nutrients.vitamins * 0.01f)));
        nutrients.vitamins = Mathf.Clamp(nutrients.vitamins, Min_nut, Max_nut);
    }
    public void Sub_minerals(int val)
    {
        nutrients.minerals -= val;
        spd = Mathf.CeilToInt(def_spd * (1 + (nutrients.minerals * 0.01f)));
        nutrients.minerals = Mathf.Clamp(nutrients.minerals, Min_nut, Max_nut);
    }


    //値の取得
    public int Get_carbohydrates() { return nutrients.carbohydrates; }
    public int Get_proteins() { return nutrients.proteins; }
    public int Get_lipid() { return nutrients.lipid; }
    public int Get_vitamins() { return nutrients.vitamins; }
    public int Get_minerals() { return nutrients.minerals; }

    public int Get_hp() { return hp; }
    public int Get_atk() { return atk; }
    public int Get_dfc() { return dfc; }
    public int Get_atk_itr() { return atk_itv; }
    public int Get_spd() { return spd; }
    public int Get_full_stomach() { return full_stomach; }

    //敵からダメージを負ったとき
    public void Damage(int val)
    {
        //無敵時間中ならダメージを受けない
        if (now_invincible_time > 0.0f) return;
        hp -= val;
        hp = Mathf.Clamp(hp, 0, 10000);
        now_invincible_time = invincible_time;
    }
}
