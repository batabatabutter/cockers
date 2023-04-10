using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutLine : MonoBehaviour
{
    Vector2 tmp;            //�I�u�W�F�N�g��Right��Bottom
    float ArmorLength;      //�A�[�}�[�̕�
    float HPLength;         //HP�̕�
    GameObject ArmorBer;    //�A�[�}�[�i�[�p
    GameObject HPber;       //HP�i�[�p
    public float HP = 100;  //MAX��Ԃ�HP

    void Start()
    {
        ArmorBer = GameObject.Find("ArmorBer");
        HPber = GameObject.Find("HPber");
        tmp = GetComponent<RectTransform>().offsetMax;
    }

    // Update is called once per frame
    void Update()
    {
        ArmorLength = ArmorBer.GetComponent<ArmorBer>().Get_currentArmor();
        HPLength = HPber.GetComponent<HPber>().Get_HP();

        //HP�ƃA�[�}�[�̍��v��100�ȉ�
        if (HPLength + ArmorLength <= HP)
        {
            //�g���������
            GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
        }
        //HP�ƃA�[�}�[�̍��v��100�ȏ�
        else if (HPLength + ArmorLength >= HP)
        {
            tmp.x = ArmorLength + HPLength - HP;
            GetComponent<RectTransform>().offsetMax = tmp;
        }
    }
}
