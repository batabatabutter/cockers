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

    bool isSelected = false;

    [SerializeField, Label("�I���J�[�\��")] GameObject selectObj;
    [SerializeField, Label("�I���J�[�\��")] floatMotion selectFloat;
    [SerializeField, Label("�J�[�\�����x")] float selectSpeed;
    [SerializeField, Label("�J�[�\���ړ���")] Vector3 selectError;
    [SerializeField, Label("�J�[�\���ړ��Ԋu")] float selectDelay;
    [SerializeField, Label("�I���{�^��")] List<GameObject> buttonObj;
    [SerializeField, Label("�I���ʒu")] List<Transform> buttonSelect;

    [SerializeField, Header("�t�F�C�h�֌W"), Label("�t�F�[�h���x")] float fadeSpeed;
    [SerializeField, Label("�t�F�[�h�A�E�g�p����")] UnityEngine.UI.RawImage rawImage;

    float timer = 0.0f;


    // Update is called once per frame
    private void Update()
    {
        timer += Time.deltaTime;
        //  ���ɑI������Ă�����
        if (isSelected)
        {
            selectObj.transform.position = Vector3.Lerp(selectObj.transform.position, buttonSelect[(int)stageID].position, Time.deltaTime * selectSpeed);
            rawImage.color = new Color(rawImage.color.r, rawImage.color.g, rawImage.color.b, rawImage.color.a + fadeSpeed * Time.deltaTime);
            if (rawImage.color.a >= 1.0f)
                this.Select();
            return;
        }

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
                isSelected = true;
                selectFloat.SetMove(false);
            }
        }

        //  �J�[�\���ړ�
        if(buttonObj[(int)stageID] != null)
        {
            selectObj.transform.position = Vector3.Lerp(selectObj.transform.position, buttonObj[(int)stageID].transform.position, Time.deltaTime * selectSpeed);
        }

        //  UI�ړ�
    }

    //  ����
    private void Select()
    {
        SceneManager.LoadScene("StageWorks");
    }

    //  �N���A
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
