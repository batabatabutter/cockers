using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//UI�g���Ƃ��ɕK�v
using UnityEngine.UI;

public class FirstButtonSelect : MonoBehaviour
{
    Button button;

    void Start()
    {
        button = GameObject.Find("Menu1").GetComponent<Button>();
        //�{�^�����I�����ꂽ��ԂɂȂ�
        button.Select();
    }
}