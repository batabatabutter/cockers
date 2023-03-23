using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageID
{
    [InspectorName("ステージ1初期")] Stage1,
    [InspectorName("ステージ2初期")] Stage2,
    [InspectorName("ステージ3初期")] Stage3,
    [InspectorName("ステージ4初期")] Stage4,
    [InspectorName("ステージ5初期")] Stage5,
    [InspectorName("ステージ6初期")] Stage6,

    [InspectorName("")] StageNum
}

[System.Serializable]
public class StageData
{
    [SerializeField, Label("ロビー")] GameObject loby;
    [SerializeField, Label("フィールド")] List<GameObject> Fields;


    public GameObject GetLoby() { return loby; }         //  ロビー受渡
    public GameObject GetField(int fieldID) { if (Fields.Count <= fieldID) return null; return Fields[fieldID]; }    //  フィールド受渡
}

public class StageManager : MonoBehaviour
{
    [SerializeField, Label("デバッグ用ステージ番号")] StageID stageID;
    [SerializeField, Label("ステージ")] List<StageData> stages;

    private StageID nowStageID;
    private GameObject nowField;

    private void Start()
    {
        nowStageID = stageID;
    }

    //  ステージ生成
    public void CreateStage(StageID stageID)
    {
        if (nowField != null) Destroy(nowField);
        if (stages[(int)stageID] == null) Debug.Log("Error:" + stageID + "対応のプレハブがありません");
        nowField = Instantiate(stages[(int)stageID].GetField(0), Vector3.zero, Quaternion.identity);
    }

    //  フィールド生成
    public void CreateField(int fieldID)
    {
        if (nowField != null) Destroy(nowField);
        if (stages[(int)nowStageID].GetField(fieldID) == null)
        {
            Debug.Log("Error:" + nowStageID + fieldID + "対応のプレハブがありません");
        }
        nowField = Instantiate(stages[(int)nowStageID].GetField(fieldID), Vector3.zero, Quaternion.identity);
    }
}
