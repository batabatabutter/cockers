using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//UI使うときに必要
using UnityEngine.UI;

public class FirstButtonSelect : MonoBehaviour
{
    [SerializeField] Button button;

    void Start()
    {
        //ボタンが選択された状態になる
        button.Select();
    }

    public void First_Select() {
        //ボタンが選択された状態になる
        button.Select();
    }
}