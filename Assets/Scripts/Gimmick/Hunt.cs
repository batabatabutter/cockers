using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Hunt : MonoBehaviour
{
    bool EnterFlag = false;

    GameObject pObject;
    GameObject eObject;
    GameObject iObject;

    private PlayerStatus player = null;
    private EnemyManager enemy = null;
    private ItemManager item = null;

    [SerializeField] GameObject text;

    public List<Vector3> Enemy = new List<Vector3>();


    // Start is called before the first frame update
    void Start()
    {

        pObject = GameObject.Find("PlayManager");
        eObject = GameObject.Find("EnemyManager");
        iObject = GameObject.Find("ItemManager");
    }

    // Update is called once per frame
    void Update()
    {
        var current = Keyboard.current;

        if (enemy == null) enemy = eObject.GetComponent<EnemyManager>();

        if (EnterFlag == true)
        {
            text.SetActive(true);
            if (current.cKey.wasPressedThisFrame)
            {
                EnemySpawn();
            }
        }

        if (EnterFlag == false) text.SetActive(false);
    }

    public void EnemySpawn()
    {
        for (int i = 0; i < Enemy.Count; ++i)
        {
            enemy.GenerateEnemy(EnemyID.CarrotSpear, Enemy[i]);
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
