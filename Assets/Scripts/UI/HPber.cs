using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HPber : MonoBehaviour
{
    private int currentHp;         //    現在の体力
    public Slider slider;          // Skider格納用
    GameObject PlayerStatus;

    //[SerializeField]
    //[Range(0, 100)]
    //public int HP = 100;

    void Start()
    {
        slider.value = currentHp;   // Sliderの初期状態を設定（HP満タン）
        PlayerStatus = GameObject.Find("Player");
    }
    private void Update()
    {
        currentHp = PlayerStatus.GetComponent<PlayerStatus>().Get_now_hp();
        slider.value = currentHp;
        //slider.value = HP;

        if (slider.value <= 0)
        {
            Debug.Log("死んだ");
        }
    }
}
