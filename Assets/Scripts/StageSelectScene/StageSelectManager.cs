using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{ 
    //  ���ݑI������Ă���X�e�[�WID
    private static StageID stageID; public static StageID GetStageID() { return stageID; }

    //  �N���A��
    private static List<bool> stageClear = new List<bool>();
    public static bool GetStageClear(StageID stageID) { return stageClear[(int)stageID]; }

    [SerializeField, Header("UI����"), Label("�X�e�[�W�S�̒Ǐ]Y�ʒu")] GameObject followAll;
    [SerializeField, Label("�I���J�[�\��")] GameObject selectObj;
    [SerializeField, Label("�J�[�\�����x")] float cursorSpeed;
    [SerializeField, Label("�I���X�e�[�W")] List<GameObject> stageObj;

    // Update is called once per frame
    private void Update()
    {
        var keyboad = Keyboard.current;

        //  �X�e�[�W�I��
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

        //  �J�[�\���ړ�
        if(stageObj[(int)stageID] != null)
        {
            selectObj.transform.position = Vector3.Lerp(selectObj.transform.position, stageObj[(int)stageID].transform.position, Time.deltaTime * cursorSpeed);
        }

        //  UI�ړ�
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
