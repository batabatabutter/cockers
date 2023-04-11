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

    //  �X�e�[�W�����
    private static List<bool> stageCanPlay = new List<bool>();

    //  �I������Ă��邩�ǂ���
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

    private void Awake()
    {
        while ((int)StageID.StageNum > stageClear.Count)
        {
            stageClear.Add(false);
            stageCanPlay.Add(false);
        }
        stageCanPlay[0] = true;
    }

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
        SceneManager.LoadScene("PlayScene");
    }

    //  �N���A
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
