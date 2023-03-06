using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  �K�v�ȃR���|�[�l���g���`
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
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
