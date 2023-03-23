using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldChanger : MonoBehaviour
{
    [SerializeField, Label("�t�B�[���h�ԍ�")] int num;

    PlayManager playManager;

    private void Start()
    {
        playManager = GameObject.FindWithTag("PlayManager").GetComponent<PlayManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            playManager.ChangeField(num);
        }
    }
}
