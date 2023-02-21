using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySPColider : MonoBehaviour
{
    //  damage
    private int dmg;

    //  ”»’è
    [SerializeField] new Collider collider;

    private void Start()
    {
        collider.enabled = false;
    }

    //  “–‚½‚è”»’è
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStatus>().Damage(dmg);
        }
    }

    //  “–‚½‚è”»’èON
    public void ChangeColider(bool enable)
    {
        collider.enabled = enable;
    }

    //  ƒ_ƒ[ƒWİ’è
    public void SetDamage(int dmg)
    {
        this.dmg = dmg;
    }
}
