using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RockBreak : MonoBehaviour
{
    bool RockFlag = false;  //岩に触れたかどうか
    GameObject Object_Rock; //岩格納用
    UIManager UIManager;    //UI格納用
    GameObject text;        //破壊
    GameObject text2;       //非破壊
    float time = 0.0f;      //タイマー

    //  プレイヤーステータス格納用
    private PlayerStatus player = null;

    //プレイヤー格納用
    GameObject PObject;

    //  各ステータス要求量
    [SerializeField ]int atk;          //  攻撃力


    // Start is called before the first frame update
    void Start()
    {
        Object_Rock = GameObject.Find("Rock");
        PObject = GameObject.FindGameObjectWithTag("PlayManager");
        UIManager = GameObject.Find("UICanvas").GetComponent<UIManager>();
        text = UIManager.GetBreakUI();
        text2 = UIManager.GetNoBreakUI();
    }

    // Update is called once per frame
    void Update()
    {
        //キー入力
        var current = Keyboard.current;
        if (player == null)
        {
            player = PObject.GetComponent<PlayManager>().GetPlayer().GetComponent<PlayerStatus>();
        }

        //岩破壊
        if (RockFlag == true && player.Get_atk() >= atk && current.cKey.wasPressedThisFrame)
        {
            text.SetActive(true);
            Destroy();
        }
        //岩破壊できない
        else if (RockFlag == true && player.Get_atk() < atk && current.cKey.wasPressedThisFrame)
        {
            text2.SetActive(true);
            DontDestroy();
        }

        //2秒後にテキストが消える
        if (text.gameObject.activeSelf == true) time += Time.deltaTime;
        if (time >= 2.0f)
        {
            text.SetActive(false);
            time = 0f;
        }
        if (text2.gameObject.activeSelf == true) time += Time.deltaTime;
        if (time >= 2.0f)
        {
            text2.SetActive(false);
            time = 0f;
        }

    }

    //岩の破壊
    public void Destroy()
    {
        Object_Rock.SetActive(false);
    }

    //岩が破壊出来ない
    public void DontDestroy()
    {
        Object_Rock.SetActive(true);
    }

    //プレイヤーが岩に触れている
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) RockFlag = true;
    }

    //プレイヤーが岩に触れていない
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) RockFlag = false;
    }


}

