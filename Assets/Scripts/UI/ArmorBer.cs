using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorBer : MonoBehaviour
{
    private float currentArmor;     //���݂̃A�[�}�[
    public Slider slider;           //Slider�i�[�p
    GameObject PlayerStatus;        //�v���C���[�̃X�e�[�^�X�i�[�p
    GameObject HPber;               //HP�i�[�p
    Vector3 tmp;                    //HP�o�[�̈ʒu
    float HPberPos;                 // HP�o�[�̕�
    int adjustment = 100;           //����

    void Start()
    {
        PlayerStatus = GameObject.Find("PlayManager").GetComponent<PlayManager>().GetPlayer();
        HPber = GameObject.Find("HPber");
        tmp = this.GetComponent<RectTransform>().position;
    }

    // Update is called once per frame
    void Update()
    {
        HPberPos = HPber.GetComponent<HPber>().Get_HPberPos();
        currentArmor = PlayerStatus.GetComponent<Armor>().Get_Value();
        slider.value = currentArmor;        //�A�[�}�[��

        tmp.x = HPberPos + adjustment;
        this.GetComponent<RectTransform>().position = tmp;
    }

    //�l�̎擾
    public float Get_currentArmor() { return currentArmor; }

}
