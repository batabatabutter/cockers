using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Salt : MonoBehaviour
{
    int dmg;

    public void SetDmg(int dmg)
    {
        this.dmg = dmg;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerStatus>().Damage(dmg);
        }
        Destroy(gameObject);
    }
}
