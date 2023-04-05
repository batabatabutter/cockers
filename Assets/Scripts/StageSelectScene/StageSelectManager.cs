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

    [SerializeField, Header("UI周り"), Label("ステージ全体追従Y位置")] GameObject followAll;
    [SerializeField, Label("選択カーソル")] GameObject selectObj;
    [SerializeField, Label("カーソル速度")] float cursorSpeed;
    [SerializeField, Label("選択ステージ")] List<GameObject> stageObj;

    // Update is called once per frame
    private void Update()
    {
        var keyboad = Keyboard.current;

        //  ステージ選択
        if (keyboad != null)
        {
            if (keyboad.leftArrowKey.wasPressedThisFrame)
            {
                stageID--;
            }
            if (keyboad.rightArrowKey.wasPressedThisFrame)
            {
                stageID++;
            }
            stageID = (StageID)Mathf.Clamp((float)stageID, (float)StageID.Stage1, (float)StageID.StageNum - 1);

            if (keyboad.zKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene("StageWorks");
            }
        }

        //  カーソル移動
        if(stageObj[(int)stageID] != null)
        {
            selectObj.transform.position = Vector3.Lerp(selectObj.transform.position, stageObj[(int)stageID].transform.position, Time.deltaTime * cursorSpeed);
        }

        //  UI移動
    }

    public static void ClearStage(StageID stageID)
    {
        while((int)StageID.StageNum > stageClear.Count)
        {
            stageClear.Add(false);
        }
        if(stageID >= StageID.StageNum)
            stageID = StageID.Stage1;
        stageClear[(int)stageID] = true; 
    }
}
