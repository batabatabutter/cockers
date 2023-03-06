using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  必要なコンポーネントを定義
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    //  発射した敵の攻撃力
    private int attack;

    //  弾を発射
    public void Shot(Vector3 dir, int enemyAttack)
    {
        //  Rigidbodyが無ければ処理しない
        if (rb == null) return;

        //  力を与える
        rb.AddForce(dir);
        rb.AddTorque(dir);

        attack = enemyAttack;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerStatus>().Damage(attack);
            Destroy(gameObject);
        }
    }
}
