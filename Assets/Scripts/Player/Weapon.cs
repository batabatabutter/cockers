using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField,HeaderAttribute("�X�e�[�^�X")] private int atk;
    [SerializeField] private float atk_enable_time;
    [SerializeField] private float atk_per_sec;

    private float now_atk_enable_time;
    private float now_cool_time;
    // Start is called before the first frame update
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
        now_cool_time = 1.0f / (atk_per_sec * player.GetComponent<PlayerStatus>().Get_atk_itr());
    }

    private void OnTriggerEnter(Collider other)
    {
        int atk_value = atk;
        atk += transform.parent.GetComponent<PlayerStatus>().Get_atk();
        //�^�O��enemy�Ȃ�
        if (other.CompareTag("Enemy"))
        {
            //�G�Ƀ_���[�W��^���鏈��
        }
    }
}