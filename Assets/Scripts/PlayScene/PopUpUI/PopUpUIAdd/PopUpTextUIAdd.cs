using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpTextUIAdd : MonoBehaviour
{
    [SerializeField, Header("�e�L�X�g"), Label("�\����"), TextArea] string popUpText; public string GetPopUpText() { return popUpText; }
    [SerializeField, Label("�����F")] Color popUpColor; public Color GetPopUpColor() { return popUpColor; }
    [SerializeField, Header("�w�i"), Label("�w�i�F")] Color popUpBackColor; public Color GetPopUpBackColor() { return popUpBackColor; }
    [SerializeField, Label("�摜")] Texture popUpTexture; public Texture GetPopUpTexture() { return popUpTexture; }
}
