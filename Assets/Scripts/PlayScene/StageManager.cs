using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageID
{
    [InspectorName("�X�e�[�W1")] Stage1,
    [InspectorName("�X�e�[�W2")] Stage2,
    [InspectorName("�X�e�[�W3")] Stage3,
    [InspectorName("�X�e�[�W4")] Stage4,
    [InspectorName("�X�e�[�W5")] Stage5,
    [InspectorName("�X�e�[�W6")] Stage6,

    [InspectorName("")] StageNum
}

public class StageManager : MonoBehaviour
{
    [SerializeField, Label("�f�o�b�O�p�X�e�[�W�ԍ�")] StageID stageID;
    [SerializeField, Label("�X�e�[�W")] List<GameObject> stageObjects;

    private void Start()
    {
        CreateStage(stageID);
    }

    //  �X�e�[�W����
    private void CreateStage(StageID stageID)
    {
        if (stageObjects[(int)stageID] == null) Debug.Log("Error:" + stageID + "�Ή��̃v���n�u������܂���");
        GameObject stage = Instantiate(stageObjects[(int)stageID], Vector3.zero, Quaternion.identity);
    }
}
