using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTextUIAdd : MonoBehaviour
{
    [SerializeField, Label("�\����"), TextArea] string popUpText; public string GetPopUpText() { return popUpText; }
    [SerializeField, Label("�\���F")] Color popUpColor; public Color GetPopUpColor() { return popUpColor; }
    [SerializeField, Label("�w�i�F")] Color popUpBackColor; public Color GetPopUpBackColor() { return popUpBackColor; }
}
