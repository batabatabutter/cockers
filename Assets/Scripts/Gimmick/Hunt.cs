using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Hunt : MonoBehaviour
{
    bool EnterFlag = false;     //触れているか確認
    bool SuccessFlag = false;   //成功したか確認

    GameObject pObject;         //プレイヤー格納用
    GameObject eObject;         //敵格納用
    GameObject iObject;         //アイテム格納用
    UIManager UIManager;        //UI格納用

    private EnemyManager enemy = null;
    private ItemManager item = null;

    GameObject text;        //報酬表示
    GameObject text2;       //成功表示

    //敵の数取得
    [HideInInspector] public List<GameObject> enemySpawn;

    public List<Vector3> EnemyPos = new List<Vector3>();        //敵の数と位置決め
    public List<Vector3> ItemPos = new List<Vector3>();         //アイテムの数と位置決め

    int Count = 0;      //成功回数
    int use = 0;        //キーの押された回数
    float time = 0.0f;  //タイマー

    Vector3 tmp;        //オブジェクトの現在位置

    // Start is called before the first frame update
    void Start()
    {
        pObject = GameObject.Find("PlayManager");
        eObject = GameObject.Find("EnemyManager");
        iObject = GameObject.Find("ItemManager");
        UIManager = GameObject.Find("UICanvas").GetComponent<UIManager>();
        text = UIManager.GetRewardUI();
        text2 = UIManager.GetSuccessUI();
        tmp = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        //敵が全員倒されたかどうか
        bool check_flg = true;
        //キー入力
        var current = Keyboard.current;

        if (enemy == null) enemy = eObject.GetComponent<EnemyManager>();
        if (item == null) item = iObject.GetComponent<ItemManager>();

        //キーを押して敵が出現
        if (EnterFlag == true && SuccessFlag == false)
        {
            text.SetActive(true);
            if (current.cKey.wasPressedThisFrame && use == 0)
            {
                EnemySpawn();
                use++;
            }
        }
        //コメント表示
        if (EnterFlag == false) text.SetActive(false);

        //敵が全員倒されたかどうか確認
        foreach (GameObject enemy in enemySpawn)
        {
            if (enemy != null)
            {
                check_flg = false;
            }
            SuccessFlag = check_flg;
        }
        //アイテム出現
        if (SuccessFlag == true && Count == 0)
        {
            for (int i = 0; i < ItemPos.Count; ++i)
            {
                item.GenerateItem(ItemID.Pork, ItemPos[i] + tmp);
            }
            Count++;
            //テキスト表示
            text2.SetActive(true);
        }

        //2秒後にテキストが消える
        if (text2.gameObject.activeSelf == true) time += Time.deltaTime;
        if (time >= 2.0f)
        {
            text2.SetActive(false);
            this.gameObject.SetActive(false);
            time = 0f;
        }
    }

    //敵が出現
    public void EnemySpawn()
    {
        for (int i = 0; i < EnemyPos.Count; ++i)
        {
            enemySpawn.Add(enemy.GenerateEnemyReturn(EnemyID.CarrotSpear, EnemyPos[i] + tmp));
        }
    }

    //プレイヤーがオブジェクトに触れている
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) EnterFlag = true;
    }
    //プレイヤーがオブジェクトに触れていない
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) EnterFlag = false;
    }

}
