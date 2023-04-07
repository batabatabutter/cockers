using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTextUIAdd : MonoBehaviour
{
    [SerializeField, Label("表示文"), TextArea] string popUpText; public string GetPopUpText() { return popUpText; }
    [SerializeField, Label("表示色")] Color popUpColor; public Color GetPopUpColor() { return popUpColor; }
    [SerializeField, Label("背景色")] Color popUpBackColor; public Color GetPopUpBackColor() { return popUpBackColor; }
}
