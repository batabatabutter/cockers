using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  �K�v�ȃR���|�[�l���g���`
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    //  �e�̎���
    [SerializeField] private float bulletTime = 3.0f;
    private float timer = 0f;
    //  ���˂����G�̍U����
    private int attack;

    //  �e�𔭎�
    public void Shot(Vector3 dir, int enemyAttack)
    {
        //  Rigidbody��������Ώ������Ȃ�
        if (rb == null) return;

        //  �͂�^����
        rb.AddForce(dir);
        rb.AddTorque(dir);

        attack = enemyAttack;

        timer = bulletTime;
    }

    private void Update()
    {
        //  ���Ԃ�����
        if (timer > 0f) timer -= Time.deltaTime;
        else Destroy(gameObject);
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
