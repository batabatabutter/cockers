using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPlayer : MonoBehaviour
{
    [SerializeField, Label("���A��")] Transform tpTransform;
    [SerializeField, Label("�����_���[�W%"), Range(0.0f, 1.0f)] float per;

    //  �Փ˔���
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.position = tpTransform.position;
            PlayerStatus player = other.GetComponent<PlayerStatus>();
            float dmg = per * player.Get_Max_hp();
            player.Damage((int)dmg);
        }
    }
}
