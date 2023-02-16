using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RockBreak : MonoBehaviour
{
    bool Rock = true;
    GameObject Object_Rock;
    [SerializeField] GameObject text;
    [SerializeField] GameObject text2;
    float time = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        Object_Rock = GameObject.Find("Rock");
    }

    // Update is called once per frame
    void Update()
    {
        var current = Keyboard.current;

        if (current.fKey.wasPressedThisFrame)
        {
            Debug.Log("壊れた");
            text.SetActive(true);
            Destroy();
        }
        else if (current.eKey.wasPressedThisFrame)
        {
            Debug.Log("壊れない");
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
}
