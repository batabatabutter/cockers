using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayManager : MonoBehaviour
{
    [SerializeField, Label("�X�e�[�W�Ǘ�")] StageManager stageManager;    public StageManager GetStageManager() { return stageManager; }
    [SerializeField, Label("�A�C�e���Ǘ�")] ItemManager itemManager;      public ItemManager GetItemManager() { return itemManager; }
    [SerializeField, Label("�������Ǘ�")] CookManager cookManager;          public CookManager GetCookManager() { return cookManager; }
    [SerializeField, Label("�G�Ǘ�")] EnemyManager enemyManager;          public EnemyManager GetEnemyManager() { return enemyManager; }
    [SerializeField, Label("�Q�[����UI�Ǘ�")] GameInUIManager gameInUIManager;    public GameInUIManager GetGameInUIManager() { return gameInUIManager; }
    [SerializeField, Label("�|�[�Y�Ǘ�")] PauseManager pauseManager;      public PauseManager GetPauseManager() { return pauseManager; }
    [SerializeField, Label("�������j���[")] GameObject cookingCamvas; public GameObject GetCookingCamvas() { return cookingCamvas; }



    [SerializeField, Label("�v���C���[")] GameObject p;
    [SerializeField, Label("�J����")] GameObject c;

    [SerializeField, Label("�f�o�b�O�X�C�b�`")] bool debug; public bool GetDebug() { return debug; }
    [SerializeField, Label("�f�o�b�O�p�X�e�[�W�ԍ�")] StageID stageID;

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

    //  �t�B�[���h�؂�ւ�
    public void ChangeField(int fieldNum)
    {
        stageManager.CreateField(fieldNum);
        itemManager.ItemReset();
        enemyManager.EnemyReset();
        gameInUIManager.Reset();
        player.transform.position = new Vector3(0.0f, 1.0f, 0.0f);
        cam.transform.position = Vector3.zero;
    }

    //  �X�e�[�W�N���A�֐�
    public void StageClear()
    {
        StageSelectManager.ClearStage(StageSelectManager.GetStageID());
        SceneManager.LoadScene("StageSelectScene");
    }
}
