using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropPlayer : MonoBehaviour
{
    [SerializeField, Label("復帰先")] Transform tpTransform;
    [SerializeField, Label("落下ダメージ%"), Range(0.0f, 1.0f)] float per;

    //  衝突判定
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
