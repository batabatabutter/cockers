using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//UI�g���Ƃ��ɕK�v
using UnityEngine.UI;

public class FirstButtonSelect : MonoBehaviour
{
    [SerializeField] Button button;

    void Start()
    {
        //�{�^�����I�����ꂽ��ԂɂȂ�
        button.Select();
    }

    public void First_Select() {
        //�{�^�����I�����ꂽ��ԂɂȂ�
        button.Select();
    }
}