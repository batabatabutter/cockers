using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//UI使うときに必要
using UnityEngine.UI;

public class FirstButtonSelect : MonoBehaviour
{
    Button button;

    void Start()
    {
        button = GameObject.Find("Menu1").GetComponent<Button>();
        //ボタンが選択された状態になる
        button.Select();
    }
}