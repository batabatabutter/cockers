using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField, Label("ステージ管理")] StageManager stageManager;
    [SerializeField, Label("アイテム管理")] ItemManager itemManager;
    [SerializeField, Label("敵管理")] EnemyManager enemyManager;

    [SerializeField, Label("プレイヤー")] GameObject p;
    [SerializeField, Label("カメラ")] GameObject c;

    [SerializeField, Label("デバッグスイッチ")] bool debug; public bool GetDebug() { return debug; }
    [SerializeField, Label("デバッグ用ステージ番号")] StageID stageID;

    GameObject player;  public GameObject GetPlayer() { return player; }
    GameObject cam;     public GameObject GetPlayerCam() { return cam; }

    // Start is called before the first frame update
    void Awake()
    {
        player = Instantiate(p);
        cam = Instantiate(c);
        StageID s = StageSelectManager.stageID;
        if (debug) s = stageID;

        stageManager.SetStageID(stageID);
        ChangeField(0);
    }

    public void ChangeField(int fieldNum)
    {
        stageManager.CreateField(fieldNum);
        itemManager.ItemReset();
        enemyManager.EnemyReset();
        player.transform.position = new Vector3(0.0f, 1.0f, 0.0f);
        cam.transform.position = Vector3.zero;
    }
}
