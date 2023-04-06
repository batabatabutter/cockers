using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RockBreak : MonoBehaviour
{
    bool RockFlag = false;  //��ɐG�ꂽ���ǂ���
    GameObject Object_Rock; //��i�[�p
    UIManager UIManager;    //UI�i�[�p
    GameObject text;        //�j��
    GameObject text2;       //��j��
    float time = 0.0f;      //�^�C�}�[

    //  �v���C���[�X�e�[�^�X�i�[�p
    private PlayerStatus player = null;

    //�v���C���[�i�[�p
    GameObject PObject;

    //  �e�X�e�[�^�X�v����
    [SerializeField ]int atk;          //  �U����


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
        //�L�[����
        var current = Keyboard.current;
        if (player == null)
        {
            player = PObject.GetComponent<PlayManager>().GetPlayer().GetComponent<PlayerStatus>();
        }

        //��j��
        if (RockFlag == true && player.Get_atk() >= atk && current.cKey.wasPressedThisFrame)
        {
            text.SetActive(true);
            Destroy();
        }
        //��j��ł��Ȃ�
        else if (RockFlag == true && player.Get_atk() < atk && current.cKey.wasPressedThisFrame)
        {
            text2.SetActive(true);
            DontDestroy();
        }

        //2�b��Ƀe�L�X�g��������
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

    //�v���C���[����ɐG��Ă���
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) RockFlag = true;
    }

    //�v���C���[����ɐG��Ă��Ȃ�
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) RockFlag = false;
    }


}

