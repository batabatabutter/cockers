using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPber : MonoBehaviour
{
    private int currentHp;         //    ���݂̗̑�
    public Slider slider;          // Skider�i�[�p
    GameObject PlayerStatus;

    void Start()
    {
        slider.value = currentHp;   // Slider�̏�����Ԃ�ݒ�iHP���^���j
        PlayerStatus = GameObject.Find("Player");

    }
    private void Update()
    {
        currentHp = PlayerStatus.GetComponent<PlayerStatus>().Get_hp();

        slider.value = currentHp;
        Debug.Log(slider.value);

        if (slider.value <= 0)
        {
            Debug.Log("����");
        }
    }
}
