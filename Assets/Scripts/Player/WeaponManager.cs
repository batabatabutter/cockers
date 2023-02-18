using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class WeaponManager : MonoBehaviour
{
    [SerializeField,HeaderAttribute("ステータス")] private int atk;
    [SerializeField] private float cool_time;

    private bool is_collider_active;
    private float now_cool_time;
    // Start is called before the first frame update
    void Start()
    {
        is_collider_active = false;
        transform.GetComponent<BoxCollider>().enabled = false;
        now_cool_time = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (now_cool_time > 0.0f)
        {
            now_cool_time -= Time.deltaTime;
            if (now_cool_time <= 0.0f)
            {
                is_collider_active = false;
                transform.GetComponent<BoxCollider>().enabled = false;
                now_cool_time = 0.0f;
            }
        }
    }

    public void Attack()
    {
        if (now_cool_time > 0.0f) return;
        is_collider_active = true;
        transform.GetComponent<BoxCollider>().enabled = true;
        now_cool_time = cool_time;
    }

    private void OnTriggerEnter(Collider other)
    {
        int atk_value = atk;
        atk += transform.parent.GetComponent<PlayerStatus>().Get_atk();
        //タグがenemyなら
        if (other.CompareTag("enemy"))
        {
            //敵にダメージを与える処理
        }
    }
}
