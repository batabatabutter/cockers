using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Hunt : MonoBehaviour
{
    bool EnterFlag = false;
    bool SuccessFlag = false;

    GameObject pObject;
    GameObject eObject;
    GameObject iObject;
    UIManager UIManager;


    private PlayerStatus player = null;
    private EnemyManager enemy = null;
    private ItemManager item = null;

    GameObject text;
    GameObject text2;


    [HideInInspector] public List<GameObject> enemySpawn;

    public List<Vector3> EnemyPos = new List<Vector3>();

    public List<Vector3> ItemPos = new List<Vector3>();

    int Count = 0;
    int use = 0;
    float time = 0.0f;

    Vector3 tmp;

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
        bool check_flg = true;
        var current = Keyboard.current;

        if (enemy == null) enemy = eObject.GetComponent<EnemyManager>();
        if (item == null) item = iObject.GetComponent<ItemManager>();

        if (EnterFlag == true)
        {
            text.SetActive(true);
            if (current.cKey.wasPressedThisFrame && use == 0)
            {
                EnemySpawn();
                use++;
            }
        }

        if (EnterFlag == false) text.SetActive(false);
        foreach (GameObject enemy in enemySpawn)
        {
            if (enemy != null)
            {
                check_flg = false;
            }
            SuccessFlag = check_flg;
        }
        if (SuccessFlag == true && Count == 0)
        {
            for (int i = 0; i < ItemPos.Count; ++i)
            {
                item.GenerateItem(ItemID.Pork, ItemPos[i] + tmp);
            }
            Count++;
            text2.SetActive(true);
        }

        if (text2.gameObject.activeSelf == true) time += Time.deltaTime;
        if (time >= 2.0f)
        {
            text2.SetActive(false);
            this.gameObject.SetActive(false);
            time = 0f;
        }

        //this.gameObject.SetActive(false);
    }

    public void EnemySpawn()
    {
        for (int i = 0; i < EnemyPos.Count; ++i)
        {
            enemySpawn.Add(enemy.GenerateEnemyReturn(EnemyID.CarrotSpear, EnemyPos[i] + tmp));
        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EnterFlag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            EnterFlag = false;
        }
    }

}
