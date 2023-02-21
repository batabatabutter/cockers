using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySPColider : MonoBehaviour
{
    //  damage
    private int dmg;

    //  判定
    [SerializeField] new Collider collider;

    private void Start()
    {
        collider.enabled = false;
    }

    //  当たり判定
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStatus>().Damage(dmg);
        }
    }

    //  当たり判定ON
    public void ChangeColider(bool enable)
    {
        collider.enabled = enable;
    }

    //  ダメージ設定
    public void SetDamage(int dmg)
    {
        this.dmg = dmg;
    }
}
