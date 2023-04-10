using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField, Label("威力")] int damage;
    [SerializeField, Label("消滅時間")] float destroyTime;

    //  更新
    private void Update()
    {
        destroyTime -= Time.deltaTime;
        if (destroyTime <= 0.0f) Destroy(gameObject);
    }

    //  ヒット判定
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStatus>().Damage(damage);
            Destroy(gameObject);
        }
    }
}
