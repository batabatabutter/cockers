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

    GameObject player;
    GameObject cam;

    // Start is called before the first frame update
    void Start()
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
