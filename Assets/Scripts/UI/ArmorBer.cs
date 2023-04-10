using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorBer : MonoBehaviour
{
    private float currentArmor;     //現在のアーマー
    public Slider slider;           //Slider格納用
    GameObject PlayerStatus;        //プレイヤーのステータス格納用
    GameObject HPber;               //HP格納用
    Vector3 tmp;                    //HPバーの位置
    float HPberPos;                 // HPバーの幅
    int adjustment = 100;           //調整

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
        slider.value = currentArmor;        //アーマー回復

        tmp.x = HPberPos + adjustment;
        this.GetComponent<RectTransform>().position = tmp;
    }

    //値の取得
    public float Get_currentArmor() { return currentArmor; }

}
