using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StageID
{
    [InspectorName("ステージ1")] Stage1,
    [InspectorName("ステージ2")] Stage2,
    [InspectorName("ステージ3")] Stage3,
    [InspectorName("ステージ4")] Stage4,
    [InspectorName("ステージ5")] Stage5,
    [InspectorName("ステージ6")] Stage6,

    [InspectorName("")] StageNum
}

public class StageManager : MonoBehaviour
{
    [SerializeField, Label("デバッグ用ステージ番号")] StageID stageID;
    [SerializeField, Label("ステージ")] List<GameObject> stageObjects;

    private void Start()
    {
        CreateStage(stageID);
    }

    //  ステージ生成
    private void CreateStage(StageID stageID)
    {
        if (stageObjects[(int)stageID] == null) Debug.Log("Error:" + stageID + "対応のプレハブがありません");
        GameObject stage = Instantiate(stageObjects[(int)stageID], Vector3.zero, Quaternion.identity);
    }
}
