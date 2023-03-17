using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageID
{
    [InspectorName("�X�e�[�W1����")] Stage1,
    [InspectorName("�X�e�[�W2����")] Stage2,
    [InspectorName("�X�e�[�W3����")] Stage3,
    [InspectorName("�X�e�[�W4����")] Stage4,
    [InspectorName("�X�e�[�W5����")] Stage5,
    [InspectorName("�X�e�[�W6����")] Stage6,

    [InspectorName("")] StageNum
}

[System.Serializable]
public class StageData
{
    [SerializeField, Label("���r�[")] GameObject loby;
    [SerializeField, Label("�t�B�[���h")] List<GameObject> Fields;

    public GameObject GetLoby() { return loby; }         //  ���r�[��n
    public GameObject GetField(int fieldID) { if (Fields.Count <= fieldID) return null; return Fields[fieldID]; }    //  �t�B�[���h��n
}

public class StageManager : MonoBehaviour
{
    [SerializeField, Label("�f�o�b�O�p�X�e�[�W�ԍ�")] StageID stageID;
    [SerializeField, Label("�X�e�[�W")] List<GameObject> stageObjects;

    [SerializeField, Label("�X�e�[�W")] List<StageData> stages;

    private GameObject nowField;

    //  �X�e�[�W����
    public void CreateStage(StageID stageID)
    {
        if (nowField != null) Destroy(nowField);
        if (stageObjects[(int)stageID] == null) Debug.Log("Error:" + stageID + "�Ή��̃v���n�u������܂���");
        nowField = Instantiate(stageObjects[(int)stageID], Vector3.zero, Quaternion.identity);
    }

    //  �t�B�[���h����
    public void CreateField(StageID stageID, int fieldID)
    {
        if (nowField != null) Destroy(nowField);
        if (stages[(int)stageID].GetField(fieldID) == null)
        {
            Debug.Log("Error:" + stageID + fieldID + "�Ή��̃v���n�u������܂���");
        }
        nowField = Instantiate(stages[(int)stageID].GetField(fieldID), Vector3.zero, Quaternion.identity);
    }

    //  �f�o�b�O�p����
    public void DebugCreateStage()
    {
        CreateStage(stageID);
    }
}