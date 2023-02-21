using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RockBreak : MonoBehaviour
{
    bool RockFlag = false;
    GameObject Object_Rock;
    [SerializeField] GameObject text;
    [SerializeField] GameObject text2;
    float time = 0.0f;

    //  プレイヤー格納用
    private PlayerStatus player;

    //  各ステータス要求量
    //[SerializeField] int carbohydrates;     //  炭水化物
    [SerializeField] int proteins;          //  タンパク質
    //[SerializeField] int lipid;             //  脂質
    //[SerializeField] int vitamins;          //  ビタミン
    //[SerializeField] int minerals;          //  無機質

    //public int carbohydrates;  //炭水化物
    //public int proteins;       //タンパク質
    //public int lipid;          //脂質
    //public int vitamins;       //ビタミン
    //public int minerals;       //無機質


    // Start is called before the first frame update
    void Start()
    {
        Object_Rock = GameObject.Find("Rock");
        player = GameObject.Find("Player").GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        var current = Keyboard.current;

        if (RockFlag == true && player.Get_proteins() >= proteins && current.cKey.wasPressedThisFrame)
        {
            text.SetActive(true);
            Destroy();
        }
        else if (RockFlag == true && player.Get_proteins() < proteins && current.cKey.wasPressedThisFrame)
        {
            text2.SetActive(true);
            DontDestroy();
        }

        //2秒後に消える
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            RockFlag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            RockFlag = false;
        }

    }


}

