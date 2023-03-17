using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayManager : MonoBehaviour
{
    [SerializeField, Label("ステージ管理")] StageManager stageManager;
    [SerializeField, Label("アイテム管理")] ItemManager itemManager;
    [SerializeField, Label("敵管理")] EnemyManager enemyManager;

    // Start is called before the first frame update
    void Start()
    {
        //stageManager.DebugCreateStage();
        stageManager.CreateField(StageID.Stage1, 0);

        itemManager.ItemReset();
        enemyManager.EnemyReset();
    }
}
