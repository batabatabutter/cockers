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

    GameObject player;
    GameObject cam;

    // Start is called before the first frame update
    void Awake()
    {
        player = Instantiate(p);
        cam = Instantiate(c);
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
