using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Armor : ActionSkill
{
    //プレイヤーのステータス
    PlayerStatus status;

    //ダメージを受けてない時間
    private float no_damage_time;

    //アーマーの受けれるダメージ量
    [SerializeField] private int armor_val;

    //アーマーの最大値
    private int armor_max_val;

    //アーマーの最小値
    private const int armor_min_val = 0;

    //プレイヤーの基礎hp
    private const int player_def_hp = 100;

    //アーマーの増加量
    [SerializeField, HeaderAttribute("1秒毎のアーマーの増加量(最大HPに対する%)")] 
    private int armor_add_per;

    //アーマーの最大値
    [SerializeField, HeaderAttribute("アーマーの最大付与量(最大HPに対する%)")]
    private int armor_max_per;

    //アーマーが付与され始めるまでの時間(初期化に使うやつ)
    private const float first_cool_time = 5.0f;

    public Armor() : base()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        status = transform.GetComponent<PlayerStatus>();
        now_cool_time = first_cool_time;
    }

    public override void FixedUpdate()
    {
        if (can_action_skill)
        {
            no_damage_time += Time.fixedDeltaTime;
            armor_max_val = (int)((player_def_hp + status.Get_hp()) * armor_max_per * 0.01);
        }

        //1秒毎にアーマーを付与する
        if (no_damage_time >= 1.0f)
        {
            armor_val += (int)((player_def_hp + status.Get_hp()) * 0.04);
            armor_val = Mathf.Clamp(armor_val, armor_min_val, armor_max_val);
            no_damage_time = 0.0f;
        }
    }

    //シールドを展開する
    public void Start_armor()
    {
        Start_action_skill();
    }

    //シールド展開終わり
    public void End_armor()
    {
        if (!have_action_skill) return;
        End_action_skill();
    }

    //シールド展開が可能かを返す
    public bool Get_now_can_armor()
    {
        return Get_can_action_skill();
    }

    //シールドを展開中かどうかを返す
    public bool Get_is_armor()
    {
        return Get_is_action_skill();
    }

    //ダメージ軽減
    public void damage_reduction(ref int damage)
    {
        int reduction_val = armor_val - damage;
        if (reduction_val>=0)
        {
            armor_val -= damage;
            damage = 0;
        }

        else
        {
            damage -= armor_val;
            armor_val = 0;
        }

        Start_action_skill();
        End_action_skill();
    }
}
