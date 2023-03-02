using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    //�h�{�f�̍\����
    public struct Status
    {
        public int hp;  //�Y������
        public int atk;       //�^���p�N��
        public int spd;       //���@��

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

    public static Status nutrients;

    private const int Min_nut = 0;
    private const int Max_nut = 100;

    [SerializeField, HeaderAttribute("��b�X�e�[�^�X")] private int def_hp = 100;    //�̗�
    [SerializeField] private int def_atk = 10;                                       //�U����
    [SerializeField] private int def_spd = 10;                                       //�X�s�[�h
    [SerializeField] private float invincible_time = 1.0f;                           //���G����

    [SerializeField, ReadOnly, HeaderAttribute("���݃X�e�[�^�X(�ҏW�����)")] private int now_hp;        //�̗�
    [SerializeField, ReadOnly] private int now_atk;                                                      //�U����
    [SerializeField, ReadOnly] private int now_spd;                                                      //�X�s�[�h
    [SerializeField, ReadOnly] private int full_stomach;                                             //�����x
    [SerializeField, ReadOnly] private float now_invincible_time;                                    //���G����



    // Start is called before the first frame update
    void Start()
    {
        nutrients.Zero();
        nutrients.Initialize(def_hp, def_atk, def_spd);
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

    //�l�̐ݒ�
    public void Set_carbohydrates(int val) { nutrients.hp = val; }
    public void Set_proteins(int val) { nutrients.atk = val; }
    public void Set_minerals(int val) { nutrients.spd = val; }

    //�l�̉��Z
    public void Add_hp(int val)
    {
        nutrients.hp += val;
        now_hp = Mathf.CeilToInt(def_hp * (1 + (nutrients.hp * 0.01f)));
        nutrients.hp = Mathf.Clamp(nutrients.hp, Min_nut, Max_nut);
    }
    public void Add_atk(int val)
    {
        nutrients.atk += val;
        now_atk = Mathf.CeilToInt(def_atk * (1 + (nutrients.atk * 0.01f)));
        nutrients.atk = Mathf.Clamp(nutrients.atk, Min_nut, Max_nut);
    }

    public void Add_spd(int val)
    {
        nutrients.spd += val;
        now_spd = Mathf.CeilToInt(def_spd * (1 + (nutrients.spd * 0.01f)));
        nutrients.spd = Mathf.Clamp(nutrients.spd, Min_nut, Max_nut);
    }

    public void Add_full_stomach(int val)
    {
        full_stomach += val;
        full_stomach = Mathf.Clamp(full_stomach, Min_nut, Max_nut);
    }

    //�l�̌��Z
    public void Sub_hp(int val)
    {
        nutrients.hp -= val;
        now_hp = Mathf.CeilToInt(def_hp * (1 + (nutrients.hp * 0.01f)));
        nutrients.hp = Mathf.Clamp(nutrients.hp, Min_nut, Max_nut);
    }
    public void Sub_atk(int val)
    {
        nutrients.atk -= val;
        now_atk = Mathf.CeilToInt(def_atk * (1 + (nutrients.atk * 0.01f)));
        nutrients.atk = Mathf.Clamp(nutrients.atk, Min_nut, Max_nut);
    }

    public void Sub_spd(int val)
    {
        nutrients.spd -= val;
        now_spd = Mathf.CeilToInt(def_spd * (1 + (nutrients.spd * 0.01f)));
        nutrients.spd = Mathf.Clamp(nutrients.spd, Min_nut, Max_nut);
    }

    public void Sub_full_stomach(int val)
    {
        full_stomach -= val;
        full_stomach = Mathf.Clamp(full_stomach, Min_nut, Max_nut);
    }


    //�l�̎擾
    public int Get_hp() { return nutrients.hp; }
    public int Get_atk() { return nutrients.atk; }

    public int Get_spd() { return nutrients.spd; }

    public int Get_now_hp() { return now_hp; }
    public int Get_now_atk() { return now_atk; }
    public int Get_now_spd() { return now_spd; }
    public int Get_full_stomach() { return full_stomach; }

    //�G����_���[�W�𕉂����Ƃ�
    public void Damage(int val)
    {
        //���G���Ԓ��Ȃ�_���[�W���󂯂Ȃ�
        if (now_invincible_time > 0.0f) return;
        Mathf.Clamp(val, 1, 9999);
        now_hp -= val;
        now_hp = Mathf.Clamp(now_hp, 0, 10000);
        now_invincible_time = invincible_time;
    }
}
