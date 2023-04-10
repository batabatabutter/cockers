using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Hunt : MonoBehaviour
{
    bool EnterFlag = false;     //�G��Ă��邩�m�F
    bool SuccessFlag = false;   //�����������m�F

    GameObject pObject;         //�v���C���[�i�[�p
    GameObject eObject;         //�G�i�[�p
    GameObject iObject;         //�A�C�e���i�[�p
    UIManager UIManager;        //UI�i�[�p

    private EnemyManager enemy = null;
    private ItemManager item = null;

    GameObject text;        //��V�\��
    GameObject text2;       //�����\��

    //�G�̐��擾
    [HideInInspector] public List<GameObject> enemySpawn;

    public List<Vector3> EnemyPos = new List<Vector3>();        //�G�̐��ƈʒu����
    public List<Vector3> ItemPos = new List<Vector3>();         //�A�C�e���̐��ƈʒu����

    int Count = 0;      //������
    int use = 0;        //�L�[�̉����ꂽ��
    float time = 0.0f;  //�^�C�}�[

    Vector3 tmp;        //�I�u�W�F�N�g�̌��݈ʒu

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
        //�G���S���|���ꂽ���ǂ���
        bool check_flg = true;
        //�L�[����
        var current = Keyboard.current;

        if (enemy == null) enemy = eObject.GetComponent<EnemyManager>();
        if (item == null) item = iObject.GetComponent<ItemManager>();

        //�L�[�������ēG���o��
        if (EnterFlag == true && SuccessFlag == false)
        {
            text.SetActive(true);
            if (current.cKey.wasPressedThisFrame && use == 0)
            {
                EnemySpawn();
                use++;
            }
        }
        //�R�����g�\��
        if (EnterFlag == false) text.SetActive(false);

        //�G���S���|���ꂽ���ǂ����m�F
        foreach (GameObject enemy in enemySpawn)
        {
            if (enemy != null)
            {
                check_flg = false;
            }
            SuccessFlag = check_flg;
        }
        //�A�C�e���o��
        if (SuccessFlag == true && Count == 0)
        {
            for (int i = 0; i < ItemPos.Count; ++i)
            {
                item.GenerateItem(ItemID.Pork, ItemPos[i] + tmp);
            }
            Count++;
            //�e�L�X�g�\��
            text2.SetActive(true);
        }

        //2�b��Ƀe�L�X�g��������
        if (text2.gameObject.activeSelf == true) time += Time.deltaTime;
        if (time >= 2.0f)
        {
            text2.SetActive(false);
            this.gameObject.SetActive(false);
            time = 0f;
        }
    }

    //�G���o��
    public void EnemySpawn()
    {
        for (int i = 0; i < EnemyPos.Count; ++i)
        {
            enemySpawn.Add(enemy.GenerateEnemyReturn(EnemyID.CarrotSpear, EnemyPos[i] + tmp));
        }
    }

    //�v���C���[���I�u�W�F�N�g�ɐG��Ă���
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) EnterFlag = true;
    }
    //�v���C���[���I�u�W�F�N�g�ɐG��Ă��Ȃ�
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player")) EnterFlag = false;
    }

}
