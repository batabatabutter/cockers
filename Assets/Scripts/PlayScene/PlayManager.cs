using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField, Label("�X�e�[�W�Ǘ�")] StageManager stageManager;
    [SerializeField, Label("�A�C�e���Ǘ�")] ItemManager itemManager;
    [SerializeField, Label("�G�Ǘ�")] EnemyManager enemyManager;

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
