using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{ 
    //  現在選択されているステージID
    private static StageID stageID; public static StageID GetStageID() { return stageID; }

    //  クリア状況
    private static List<bool> stageClear = new List<bool>();
    public static bool GetStageClear(StageID stageID) { return stageClear[(int)stageID]; }

    //  ステージ解放状況
    private static List<bool> stageCanPlay = new List<bool>();

    //  選択されているかどうか
    bool isSelected = false;

    [Header("選択UI")]
    [SerializeField, Label("選択カーソル")] GameObject selectObj;
    [SerializeField, Label("選択カーソル")] floatMotion selectFloat;
    [SerializeField, Label("カーソル速度")] float selectSpeed;
    [SerializeField, Label("カーソル移動差")] Vector3 selectError;
    [SerializeField, Label("カーソル移動間隔")] float selectDelay;
    [SerializeField, Label("選択ボタン")] List<GameObject> buttonObj;
    [SerializeField, Label("選択位置")] List<Transform> buttonSelect;

    [Header("選択色")]
    [SerializeField, Label("ステージ未開放時透明度"), Range(0.0f, 1.0f)] float lockdArpha;

    [SerializeField, Header("フェイド関係"), Label("フェード速度")] float fadeSpeed;
    [SerializeField, Label("フェードアウト用黒幕")] UnityEngine.UI.RawImage rawImage;

    float timer = 0.0f;
    private List<UnityEngine.UI.RawImage> buttonRawImages;

    private void Awake()
    {
        buttonRawImages = new List<UnityEngine.UI.RawImage>();

        while ((int)StageID.StageNum > stageClear.Count)
        {
            stageClear.Add(false);
            stageCanPlay.Add(false);
        }
        stageCanPlay[0] = true;

        foreach (GameObject obj in buttonObj)
        {
            buttonRawImages.Add(obj.GetComponent<UnityEngine.UI.RawImage>());
        }
    }

    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;
        //  既に選択されていたら
        if (isSelected)
        {
            selectObj.transform.position = Vector3.Lerp(selectObj.transform.position, buttonSelect[(int)stageID].position, Time.deltaTime * selectSpeed);
            rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, rawImage.color.a + fadeSpeed * Time.deltaTime);
            if (rawImage.color.a >= 1.0f)
                this.Select();
            return;
        }

        var keyboad = Keyboard.current;

        //  ステージ選択
        if (keyboad != null)
        {
            if (keyboad.leftArrowKey.wasPressedThisFrame)
            {
                StageID beforeStageID = stageID;
                stageID--;
                stageID = (StageID)Mathf.Clamp((float)stageID, (float)StageID.Stage1, (float)StageID.StageNum - 1);
                while (!stageCanPlay[(int)stageID])
                {
                    stageID--;
                    if (stageID < 0)
                    {
                        stageID = beforeStageID;
                        break;
                    }
                }
            }
            if (keyboad.rightArrowKey.wasPressedThisFrame)
            {
                StageID beforeStageID = stageID;
                stageID++;
                stageID = (StageID)Mathf.Clamp((float)stageID, (float)StageID.Stage1, (float)StageID.StageNum - 1);
                while (!stageClear[(int)stageID - 1])
                {
                    stageID++;
                    if (stageID > StageID.StageNum - 1)
                    {
                        stageID = beforeStageID;
                        break;
                    }
                }
            }
            stageID = (StageID)Mathf.Clamp((float)stageID, (float)StageID.Stage1, (float)StageID.StageNum - 1);

            if (keyboad.zKey.wasPressedThisFrame)
            {
                isSelected = true;
                selectFloat.SetMove(false);
            }
        }

        //  カーソル移動
        if (buttonObj[(int)stageID] != null)
        {
            selectObj.transform.position = Vector3.Lerp(selectObj.transform.position, buttonObj[(int)stageID].transform.position, Time.deltaTime * selectSpeed);
        }

        //  UI仕様
        for (int i = 0; i < buttonRawImages.Count; i++)
        {
            Color color = buttonRawImages[i].color;
            if (stageCanPlay[i])
                color.a = 1.0f;
            else
                color.a = lockdArpha;
            buttonRawImages[i].color = color;
        }
    }

    //  決定
    private void Select()
    {
        SceneManager.LoadScene("PlayScene");
    }

    //  クリア
    public static void ClearStage(StageID stageID)
    {
        while((int)StageID.StageNum > stageClear.Count)
        {
            stageClear.Add(false);
            stageCanPlay.Add(false);
        }
        if (stageID >= StageID.StageNum)
            stageID = StageID.Stage1;
        stageClear[(int)stageID] = true;
        if (stageID < StageID.StageNum)
            stageCanPlay[(int)stageID + 1] = true;
    }
}
