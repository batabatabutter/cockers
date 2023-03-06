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

    void Start()
    {
        transform.GetComponent<BoxCollider>().enabled = false;
        now_atk_enable_time = 0.0f;
        now_cool_time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (now_atk_enable_time > 0.0f)
        {
            now_atk_enable_time -= Time.deltaTime;
            if (now_atk_enable_time <= 0.0f)
            {
                now_atk_enable_time = 0.0f;
                transform.GetComponent<BoxCollider>().enabled = false;
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

    public void Attack()
    {
        if (now_cool_time > 0.0f) return;
        transform.GetComponent<BoxCollider>().enabled = true;
        now_atk_enable_time = atk_enable_time;
        now_cool_time = 1.0f / (atk_per_sec);
        atk_cnt++;
        if (atk_cnt % Atk_MOD == 0) player.Sub_full_stomach(Sub_full_val);
    }

    private void OnTriggerEnter(Collider other)
    {
        int atk_value = (int)(atk * (1 + player.Get_atk() * 0.01));
        atk_value += player.Get_now_atk();
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
    }
}
