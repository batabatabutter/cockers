using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    //‰h—{‘f‚Ì\‘¢‘Ì
    public struct Nutrients
    {
        public int carbohydrates;  //’Y…‰»•¨
        public int proteins;       //ƒ^ƒ“ƒpƒNŽ¿
        public int lipid;          //Ž‰Ž¿
        public int vitamins;       //ƒrƒ^ƒ~ƒ“
        public int minerals;       //–³‹@Ž¿

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

    [SerializeField, HeaderAttribute("Šî‘bƒXƒe[ƒ^ƒX")] private int def_hp = 100;    //‘Ì—Í
    [SerializeField] private int def_atk = 10;                                       //UŒ‚—Í
    [SerializeField] private int def_dfc = 10;                                       //–hŒä—Í
    [SerializeField] private int def_atk_itv = 1;                                    //UŒ‚‰ñ””{—¦
    [SerializeField] private int def_spd = 10;                                       //ƒXƒs[ƒh
    [SerializeField] private float invincible_time = 1.0f;                           //–³“GŽžŠÔ

    [SerializeField, ReadOnly, HeaderAttribute("Œ»ÝƒXƒe[ƒ^ƒX(•ÒW‚·‚é‚È)")] private int hp;        //‘Ì—Í
    [SerializeField, ReadOnly] private int atk;                                                      //UŒ‚—Í
    [SerializeField, ReadOnly] private int dfc;                                                      //–hŒä—Í
    [SerializeField, ReadOnly] private int atk_itv;                                                  //UŒ‚‰ñ””{—¦
    [SerializeField, ReadOnly] private int spd;                                                      //ƒXƒs[ƒh
    [SerializeField, ReadOnly] private int full_stomach;                                             //–ž• “x
    [SerializeField, ReadOnly] private float now_invincible_time;                                    //–³“GŽžŠÔ



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

    //’l‚ÌÝ’è
    public void Set_carbohydrates(int val) { nutrients.carbohydrates = val; }
    public void Set_proteins(int val) { nutrients.proteins = val; }
    public void Set_lipid(int val) { nutrients.lipid = val; }
    public void Set_vitamins(int val) { nutrients.vitamins = val; }
    public void Set_minerals(int val) { nutrients.minerals = val; }

    //’l‚Ì‰ÁŽZ
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

    //’l‚ÌŒ¸ŽZ
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


    //’l‚ÌŽæ“¾
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

    //“G‚©‚çƒ_ƒ[ƒW‚ð•‰‚Á‚½‚Æ‚«
    public void Damage(int val)
    {
        //–³“GŽžŠÔ’†‚È‚çƒ_ƒ[ƒW‚ðŽó‚¯‚È‚¢
        if (now_invincible_time > 0.0f) return;
        val -= Get_dfc();
        Mathf.Clamp(val, 1, 9999);
        hp -= val;
        hp = Mathf.Clamp(hp, 0, 10000);
        now_invincible_time = invincible_time;
    }
}
