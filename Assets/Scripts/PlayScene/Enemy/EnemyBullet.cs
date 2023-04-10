using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField, Label("�З�")] int damage;
    [SerializeField, Label("���Ŏ���")] float destroyTime;

    //  �X�V
    private void Update()
    {
        destroyTime -= Time.deltaTime;
        if (destroyTime <= 0.0f) Destroy(gameObject);
    }

    //  �q�b�g����
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStatus>().Damage(damage);
            Destroy(gameObject);
        }
    }
}
