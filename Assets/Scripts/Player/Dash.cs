using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    //rigidbody
    [SerializeField] private Rigidbody rigid;

    //そもそも今ダッシュを持っているか
    [SerializeField] private bool have_dash;

    //今ダッシュできるか
    [SerializeField] private bool can_dash;

    //ダッシュしてるか
    private bool is_dash;

    //ダッシュの現在クールタイム
    private float cool_time;

    //ダッシュのクールタイム
    [SerializeField] private const float Dash_cool_time = 3.0f;

    //目標のポジション
    private Vector3 target_pos;

    //加える力(Xのみ)
    [SerializeField, HeaderAttribute("ダッシュ時に加える力")] private float dash_force = 5.0f;

    //加える力(行列)
    private Vector3 dash_force_vec;

    //ダッシュする時間
    [SerializeField, HeaderAttribute("ダッシュ時間")] private float dash_time = 0.4f;

    //ダッシュの残り時間
    private float now_dash_time;

    //プレイヤーがどっち向いてるか(true : 右,false : 左)
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

    //アップデート
    //クールタイムの減少に用いる
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

    //ダッシュをゲットしたときに使う
    public void Allow_dash_to_player()
    {
        have_dash = true;
        can_dash = true;
    }

    //ダッシュする
    public void Start_Dash(bool allow)
    {
        is_dash = true;
        can_dash = false;
        if(allow) dash_force_vec.x = dash_force;
        else dash_force_vec.x = -dash_force;
        rigid.constraints |= RigidbodyConstraints.FreezePositionY;
    }

    //ダッシュ終わり
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

    //ダッシュが終わったかをチェックする
    public void Check_end_dash()
    {
        if (now_dash_time >= dash_time)
        {
            End_dash();
        }
    }

    //ダッシュが可能かを返す
    public bool Get_now_can_dash()
    {
        return can_dash & !is_dash;
    }

    //ダッシュ中かどうかを返す
    public bool Get_is_dash()
    {
        return is_dash;
    }
}
