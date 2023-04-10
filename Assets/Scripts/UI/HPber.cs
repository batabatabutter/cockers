using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPber : MonoBehaviour
{
    private float currentHp;             //現在の体力
    public int beforHP = 100;            //前の体力
    public Slider slider;                //Slider格納用
    GameObject PlayerStatus;             //プレイヤーのステータス格納用

    Vector3 tmpWidth;                    //オブジェクトの幅
    float HPberPos;                      //位置
    float division;                      //割る値

    void Start()
    {
        slider.value = currentHp;   // Sliderの初期状態を設定（HP満タン）
        PlayerStatus = GameObject.Find("PlayManager").GetComponent<PlayManager>().GetPlayer();
        tmpWidth.x = this.transform.localScale.x * this.GetComponent<RectTransform>().rect.width;
    }
    private void Update()
    {
        currentHp = PlayerStatus.GetComponent<PlayerStatus>().Get_now_hp();
        slider.value = currentHp;
        division = currentHp * 0.01f;
        HPberPos = tmpWidth.x * division;

    }

    //値の取得
    public float Get_HPberPos() { return HPberPos; }
    public float Get_HP() { return currentHp; }
}
