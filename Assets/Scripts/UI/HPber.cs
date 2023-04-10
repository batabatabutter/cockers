using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPber : MonoBehaviour
{
    private float currentHp;          //���݂̗̑�
    public int beforHP = 100;            //�O�̗̑�
    public Slider slider;           //Skider�i�[�p
    GameObject PlayerStatus;        //�v���C���[�̃X�e�[�^�X�i�[�p

    Vector3 tmpWidth;
    float HPberPos;
    float division;

    void Start()
    {
        slider.value = currentHp;   // Slider�̏�����Ԃ�ݒ�iHP���^���j
        PlayerStatus = GameObject.Find("PlayManager").GetComponent<PlayManager>().GetPlayer();
        tmpWidth.x = this.transform.localScale.x * this.GetComponent<RectTransform>().rect.width;
    }
    private void Update()
    {
        currentHp = PlayerStatus.GetComponent<PlayerStatus>().Get_now_hp();
        slider.value = currentHp;
        division = currentHp * 0.01f;
        HPberPos = tmpWidth.x * division;

        if (slider.value <= 0)
        {
            Debug.Log("��");
        }
    }

    public float Get_HPberPos() { return HPberPos; }

    public float Get_HP() { return currentHp; }
}
