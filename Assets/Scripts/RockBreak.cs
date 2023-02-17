using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RockBreak : MonoBehaviour
{
    bool Rock = true;
    GameObject Object_Rock;
    GameObject PlayerStatus;
    [SerializeField] GameObject text;
    [SerializeField] GameObject text2;
    float time = 0.0f;

    public int carbohydrates;  //�Y������
    public int proteins;       //�^���p�N��
    public int lipid;          //����
    public int vitamins;       //�r�^�~��
    public int minerals;       //���@��


    // Start is called before the first frame update
    void Start()
    {
        Object_Rock = GameObject.Find("Rock");
        PlayerStatus = GameObject.Find("Player");
    }

    // Update is called once per frame
    void Update()
    {
        var current = Keyboard.current;

        carbohydrates = PlayerStatus.GetComponent<PlayerStatus>().Get_carbohydrates();

        if (carbohydrates >= 30 && current.fKey.wasPressedThisFrame)
        {
            Debug.Log("��ꂽ");
            text.SetActive(true);
            Destroy();
        }
        else if (carbohydrates < 30 && current.fKey.wasPressedThisFrame)
        {
            Debug.Log("���Ȃ�");
            text2.SetActive(true);
            DontDestroy();
        }

        //2�b��ɏ�����
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

    //��̔j��
    public void Destroy()
    {
        Object_Rock.SetActive(false);
    }

    //�₪�j��o���Ȃ�
    public void DontDestroy()
    {
        Object_Rock.SetActive(true);
    }
}
