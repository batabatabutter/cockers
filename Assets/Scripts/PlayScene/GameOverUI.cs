using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameOverUI : MonoBehaviour
{
    [SerializeField, Header("�t�F�C�h�֌W"), Label("�t�F�[�h���x")] float fadeSpeed;
    [SerializeField, Label("�ő�t�F�[�h�l")] float maxFadeVal;
    [SerializeField, Label("�ŏ��t�F�[�h�l")] float minFadeVal;
    [SerializeField, Label("�t�F�[�h�A�E�g�p����")] UnityEngine.UI.RawImage rawImage;

    CanvasGroup canvasGroop;
    //  �t�F�[�h�C�����A�E�g�����f
    bool fadeIn = false; public void SetFadeIn(bool val) { fadeIn = val; }

    //  ���݂̑I�����
    enum ButtonStatas
    {
        restart,
        returnSelect,

        buttonStatasNum
    };
    ButtonStatas buttonStatas = ButtonStatas.restart;
    bool isSelected = false;

    [SerializeField, Label("�I���J�[�\��")] GameObject selectObj;
    [SerializeField, Label("�J�[�\�����x")] float selectSpeed;
    [SerializeField, Label("�J�[�\���ړ���")] Vector3 selectError;
    [SerializeField, Label("�J�[�\���ړ��Ԋu")] float selectDelay;
    [SerializeField, Label("�I���{�^��")] List<GameObject> buttonObj;
    [SerializeField, Label("�I���ʒu")] List<Transform> buttonSelect;

    float timer = 0.0f;

    PlayManager playManager;

    private void Start()
    {
        playManager = GameObject.FindGameObjectWithTag("PlayManager").GetComponent<PlayManager>();
        canvasGroop = gameObject.GetComponent<CanvasGroup>();
        canvasGroop.alpha = minFadeVal;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        //  �t�F�[�h�C���Ȃ�
        if(fadeIn)
        {
            canvasGroop.alpha += fadeSpeed * Time.deltaTime;
        }
        //  �t�F�[�h�A�E�g�Ȃ�
        else
        {
            canvasGroop.alpha -= fadeSpeed * Time.deltaTime;
        }
        canvasGroop.alpha = Mathf.Clamp(canvasGroop.alpha, minFadeVal, maxFadeVal);

        //  ���ɑI������Ă�����
        if(isSelected)
        {
            selectObj.transform.position = Vector3.Lerp(selectObj.transform.position, buttonSelect[(int)buttonStatas].position, Time.deltaTime * selectSpeed);
            rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, rawImage.color.a + fadeSpeed * Time.deltaTime);
            if (rawImage.color.a >= 1.0f)
                this.Select();
            return;
        }

        //  �J�[�\���ړ�
        if (buttonObj[(int)buttonStatas] != null && !isSelected)
        {
            selectObj.transform.position = Vector3.Lerp(selectObj.transform.position, buttonObj[(int)buttonStatas].transform.position, Time.deltaTime * selectSpeed);
            if (timer > selectDelay)
                selectObj.transform.position = Vector3.Lerp(selectObj.transform.position, buttonObj[(int)buttonStatas].transform.position + selectError, Time.deltaTime * selectSpeed * 2);
            if (timer > selectDelay * 2)
                timer = 0.0f;
        }

        //  �L�[�֌W
        var keyboad = Keyboard.current;

        //  �X�e�[�W�I��
        if (keyboad != null)
        {
            if (keyboad.leftArrowKey.wasPressedThisFrame)
            {
                buttonStatas--;
            }
            if (keyboad.rightArrowKey.wasPressedThisFrame)
            {
                buttonStatas++;
            }
            buttonStatas = (ButtonStatas)Mathf.Clamp((float)buttonStatas, 0.0f, (float)ButtonStatas.buttonStatasNum - 1);
        }

        //  ����
        if (keyboad.zKey.wasPressedThisFrame)
        {
            isSelected = true;
        }
    }

    private void Select()
    {
        switch (buttonStatas)
        {
            case ButtonStatas.restart:
                playManager.Restart();
                break;
            case ButtonStatas.returnSelect:
                playManager.ReturnSelectScene();
                break;
        }
    }
}
