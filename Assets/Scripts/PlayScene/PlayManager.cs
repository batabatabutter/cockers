using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField, Label("�X�e�[�W�Ǘ�")] StageManager stageManager;    public StageManager GetStageManager() { return stageManager; }
    [SerializeField, Label("�A�C�e���Ǘ�")] ItemManager itemManager;      public ItemManager GetItemManager() { return itemManager; }
    [SerializeField, Label("�������Ǘ�")] CookManager cookManager;          public CookManager GetCookManager() { return cookManager; }
    [SerializeField, Label("�G�Ǘ�")] EnemyManager enemyManager;          public EnemyManager GetEnemyManager() { return enemyManager; }
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
        StageID s = StageSelectManager.stageID;
        if (debug) s = this.stageID;

        itemManager.ItemStart();
        if (cookManager != null) cookManager.CookManager_Reset();
        if (pauseManager != null) pauseManager.PauseReset();
        stageManager.SetStageID(s);
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
