using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameOverUI : MonoBehaviour
{
    [SerializeField, Header("フェイド関係"), Label("フェード速度")] float fadeSpeed;
    [SerializeField, Label("最大フェード値")] float maxFadeVal;
    [SerializeField, Label("最小フェード値")] float minFadeVal;
    [SerializeField, Label("フェードアウト用黒幕")] UnityEngine.UI.RawImage rawImage;

    CanvasGroup canvasGroop;
    //  フェードインかアウトか判断
    bool fadeIn = false; public void SetFadeIn(bool val) { fadeIn = val; }

    //  現在の選択状態
    enum ButtonStatas
    {
        restart,
        returnSelect,

        buttonStatasNum
    };
    ButtonStatas buttonStatas = ButtonStatas.restart;
    bool isSelected = false;

    [SerializeField, Label("選択カーソル")] GameObject selectObj;
    [SerializeField, Label("カーソル速度")] float selectSpeed;
    [SerializeField, Label("カーソル移動差")] Vector3 selectError;
    [SerializeField, Label("カーソル移動間隔")] float selectDelay;
    [SerializeField, Label("選択ボタン")] List<GameObject> buttonObj;
    [SerializeField, Label("選択位置")] List<Transform> buttonSelect;

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

        //  フェードインなら
        if(fadeIn)
        {
            canvasGroop.alpha += fadeSpeed * Time.deltaTime;
        }
        //  フェードアウトなら
        else
        {
            canvasGroop.alpha -= fadeSpeed * Time.deltaTime;
        }
        canvasGroop.alpha = Mathf.Clamp(canvasGroop.alpha, minFadeVal, maxFadeVal);

        //  既に選択されていたら
        if(isSelected)
        {
            selectObj.transform.position = Vector3.Lerp(selectObj.transform.position, buttonSelect[(int)buttonStatas].position, Time.deltaTime * selectSpeed);
            rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, rawImage.color.a + fadeSpeed * Time.deltaTime);
            if (rawImage.color.a >= 1.0f)
                this.Select();
            return;
        }

        //  カーソル移動
        if (buttonObj[(int)buttonStatas] != null && !isSelected)
        {
            selectObj.transform.position = Vector3.Lerp(selectObj.transform.position, buttonObj[(int)buttonStatas].transform.position, Time.deltaTime * selectSpeed);
            if (timer > selectDelay)
                selectObj.transform.position = Vector3.Lerp(selectObj.transform.position, buttonObj[(int)buttonStatas].transform.position + selectError, Time.deltaTime * selectSpeed * 2);
            if (timer > selectDelay * 2)
                timer = 0.0f;
        }

        //  キー関係
        var keyboad = Keyboard.current;

        //  ステージ選択
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

        //  決定
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
