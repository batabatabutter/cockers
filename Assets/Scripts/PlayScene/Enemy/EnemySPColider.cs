using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySPColider : MonoBehaviour
{
    //  damage
    private int dmg;

    //  ����
    [SerializeField] new Collider collider;

    private void Start()
    {
        collider.enabled = false;
    }

    //  �����蔻��
    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStatus>().Damage(dmg);
        }
    }

    //  �����蔻��ON
    public void ChangeColider(bool enable)
    {
        collider.enabled = enable;
    }

    //  �_���[�W�ݒ�
    public void SetDamage(int dmg)
    {
        this.dmg = dmg;
    }
}
