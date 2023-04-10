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

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStatus>().Damage(dmg);
            Destroy(gameObject);
        }
    }
}
