using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutLine : MonoBehaviour
{
    Vector2 tmp;            //オブジェクトのRightとBottom
    float ArmorLength;      //アーマーの幅
    float HPLength;         //HPの幅
    GameObject ArmorBer;    //アーマー格納用
    GameObject HPber;       //HP格納用
    public float HP = 100;  //MAX状態のHP

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

        //HPとアーマーの合計が100以下
        if (HPLength + ArmorLength <= HP)
        {
            //枠線初期状態
            GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
        }
        //HPとアーマーの合計が100以上
        else if (HPLength + ArmorLength >= HP)
        {
            tmp.x = ArmorLength + HPLength - HP;
            GetComponent<RectTransform>().offsetMax = tmp;
        }
    }
}
