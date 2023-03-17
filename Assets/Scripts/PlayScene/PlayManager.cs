using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField, Label("�X�e�[�W�Ǘ�")] StageManager stageManager;
    [SerializeField, Label("�A�C�e���Ǘ�")] ItemManager itemManager;
    [SerializeField, Label("�G�Ǘ�")] EnemyManager enemyManager;

    // Start is called before the first frame update
    void Start()
    {
        //stageManager.DebugCreateStage();
        stageManager.CreateField(StageID.Stage1, 0);

        itemManager.ItemReset();
        enemyManager.EnemyReset();
    }
}
