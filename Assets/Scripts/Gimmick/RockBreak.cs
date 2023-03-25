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

    //  �v���C���[�i�[�p
    private PlayerStatus player = null;

    GameObject PObject;

    //  �e�X�e�[�^�X�v����
    int atk;          //  �U����


    // Start is called before the first frame update
    void Start()
    {
        Object_Rock = GameObject.Find("Rock");
        PObject = GameObject.Find("PlayManager");
    }

    // Update is called once per frame
    void Update()
    {
        var current = Keyboard.current;

        if (player == null)
        {
            player = PObject.GetComponent<PlayManager>().GetPlayer().GetComponent<PlayerStatus>();
        }


        if (RockFlag == true && player.Get_atk() >= atk && current.cKey.wasPressedThisFrame)
        {
            text.SetActive(true);
            Destroy();
        }
        else if (RockFlag == true && player.Get_atk() < atk && current.cKey.wasPressedThisFrame)
        {
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RockFlag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            RockFlag = false;
        }
    }


}

