using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTextUIAdd : MonoBehaviour
{
    [SerializeField, Header("テキスト"), Label("表示文"), TextArea] string popUpText; public string GetPopUpText() { return popUpText; }
    [SerializeField, Label("文字色")] Color popUpColor; public Color GetPopUpColor() { return popUpColor; }
    [SerializeField, Header("背景"), Label("背景色")] Color popUpBackColor; public Color GetPopUpBackColor() { return popUpBackColor; }
    [SerializeField, Label("画像")] Texture popUpTexture; public Texture GetPopUpTexture() { return popUpTexture; }
}
