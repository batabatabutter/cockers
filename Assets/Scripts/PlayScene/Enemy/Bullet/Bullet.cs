using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  •K—v‚ÈƒRƒ“ƒ|[ƒlƒ“ƒg‚ð’è‹`
[RequireComponent(typeof(Rigidbody))]
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;

    //  ’e‚ÌŽõ–½
    [SerializeField] private float bulletTime = 3.0f;
    private float timer = 0f;
    //  ”­ŽË‚µ‚½“G‚ÌUŒ‚—Í
    private int attack;

    //  ’e‚ð”­ŽË
    public void Shot(Vector3 dir, int enemyAttack)
    {
        //  Rigidbody‚ª–³‚¯‚ê‚Îˆ—‚µ‚È‚¢
        if (rb == null) return;

        //  —Í‚ð—^‚¦‚é
        rb.AddForce(dir);
        rb.AddTorque(dir);

        attack = enemyAttack;

        timer = bulletTime;
    }

    private void Update()
    {
        //  ŽžŠÔ‚ðÁ”ï
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
