using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    [SerializeField, Label("ステージ管理")] StageManager stageManager;    public StageManager GetStageManager() { return stageManager; }
    [SerializeField, Label("アイテム管理")] ItemManager itemManager;      public ItemManager GetItemManager() { return itemManager; }
    [SerializeField, Label("料理物管理")] CookManager cookManager;          public CookManager GetCookManager() { return cookManager; }
    [SerializeField, Label("敵管理")] EnemyManager enemyManager;          public EnemyManager GetEnemyManager() { return enemyManager; }
    [SerializeField, Label("ゲーム内UI管理")] GameInUIManager gameInUIManager;    public GameInUIManager GetGameInUIManager() { return gameInUIManager; }
    [SerializeField, Label("ポーズ管理")] PauseManager pauseManager;      public PauseManager GetPauseManager() { return pauseManager; }
    [SerializeField, Label("料理メニュー")] GameObject cookingCamvas; public GameObject GetCookingCamvas() { return cookingCamvas; }



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
        StageID s = StageSelectManager.GetStageID();
        if (debug) s = this.stageID;

        itemManager.ItemStart();
        if (cookManager != null) cookManager.CookManager_Reset();
        if (gameInUIManager != null) gameInUIManager.Initialize();
        if (pauseManager != null) pauseManager.PauseReset();
        stageManager.SetStageID(s);
        ChangeField(0);
    }

    //  フィールド切り替え
    public void ChangeField(int fieldNum)
    {
        stageManager.CreateField(fieldNum);
        itemManager.ItemReset();
        enemyManager.EnemyReset();
        gameInUIManager.Reset();
        player.transform.position = new Vector3(0.0f, 1.0f, 0.0f);
        cam.transform.position = Vector3.zero;
    }

    //  ステージクリア関数
    public void StageClear()
    {
        StageSelectManager.ClearStage(StageSelectManager.GetStageID());
        SceneManager.LoadScene("StageSelectScene");
    }
}
