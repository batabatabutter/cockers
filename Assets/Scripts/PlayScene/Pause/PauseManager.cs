using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Linq;

public enum Select_ID
{
    Cook,
    Item,
    Other,

    OverID
}

public class PauseManager : MonoBehaviour
{
    //pause���
    [SerializeField, HeaderAttribute("pause���")] private GameObject pause_menu;

    [SerializeField, HeaderAttribute("�A�C�e�����X�g�̕\���ꏊ")] private GameObject item_list_content;
    [SerializeField, HeaderAttribute("�A�C�e���̐��̊Ǘ��I�u�W�F�N�g")] private GameObject item_manager_obj;
    [SerializeField] private List<Button> select_list;
    //[SerializeField,HeaderAttribute("���j���[���e")] private MenuManager;
    private ItemManager item_manager;
    private CookManager cook_manager;

    [SerializeField, HeaderAttribute("�ǉ�������̂�prefab")] public GameObject button_def;

    private Dictionary<string, ItemID> item_dic;
    private Dictionary<string, CookID> cook_dic;

    //���ꂼ��̃A�C�e���̐���ێ����郊�X�g
    [SerializeField] private List<KeyValuePair<ItemID, int>> item_val;
    [SerializeField] private List<KeyValuePair<CookID, int>> cook_val;

    //�A�C�e���ꗗ�쐬�p
    [SerializeField] private GameObject item_content;
    private List<GameObject> item_contents;

    public PlayerStatus player_status;

    private Select_ID select_id;

    private bool eat_check;

    //�m�F��ʗp
    [SerializeField, HeaderAttribute("�m�F���")] private GameObject check_panel;
    private GameObject check_text;
    private GameObject yes_button;
    private GameObject no_button;

    private Menu eat_set_menu;
    private CookID eat_set_id;
    private const int Full = 100;

    // Start is called before the first frame update
    public void PauseReset()
    {
        item_dic = new Dictionary<string, ItemID>();
        item_dic.Add("�L���x�c", ItemID.Cabbage);
        item_dic.Add("�j���W��", ItemID.Carrot);
        item_dic.Add("�ؓ�", ItemID.Pork);
        item_dic.Add("�{��", ItemID.Chicken);
        item_dic.Add("����", ItemID.Beaf);
        item_dic.Add("�ʂ˂�", ItemID.Onion);
        item_dic.Add("���イ��", ItemID.Cucumber);
        item_dic.Add("�哤", ItemID.Been);
        item_dic.Add("��", ItemID.Egg);
        item_dic.Add("����", ItemID.Milk);
        item_dic.Add("�W���K�C��", ItemID.Potato);
        item_dic.Add("�����S", ItemID.Apple);
        item_dic.Add("�C�`�S", ItemID.Strawberry);
        item_val = new List<KeyValuePair<ItemID, int>>();
        foreach (var item in item_dic)
        {
            item_val.Add(new KeyValuePair<ItemID, int>(item.Value, 0));
        }
        cook_dic = new Dictionary<string, CookID>();
        cook_dic.Add("��݂��L���x�c", CookID.GoodCabegge);
        cook_dic.Add("�؂ƃL���x�c�̗₵���", CookID.PorkAndCabeggeSyabu);
        cook_dic.Add("�ؓ��ƃL���x�c�̊Ðh�݂��u��", CookID.PorkCabeggeAndMiso);
        cook_dic.Add("�J�b�g�����S", CookID.CutApple);
        cook_val = new List<KeyValuePair<CookID, int>>();
        foreach (var cook in cook_dic)
        {
            cook_val.Add(new KeyValuePair<CookID, int>(cook.Value, 0));
        }

        select_list[0].Select();

        item_manager = item_manager_obj.GetComponent<ItemManager>();
        cook_manager = item_manager_obj.GetComponent<CookManager>();

        All_Item_Val_Check();

        item_contents = new List<GameObject>();

        Transform children = check_panel.GetComponentInChildren<Transform>();
        foreach (Transform ob in children)
        {
            if (ob.name == "YesButton")
            {
                yes_button = ob.gameObject;
            }
            else if (ob.name == "NoButton")
            {
                no_button = ob.gameObject;
            }
            else if (ob.name == "CheckText")
            {
                check_text = ob.gameObject;
            }
        }

        select_id = Select_ID.OverID;

        player_status = GameObject.Find("PlayManager").GetComponent<PlayManager>().GetPlayer().GetComponent<PlayerStatus>();
    }

    // Update is called once per frame
    void Update()
    {
        Keyboard keyboard = Keyboard.current;
        if (keyboard.tabKey.wasPressedThisFrame)
        {
            if (pause_menu.activeSelf)
            {
                Go_back();
            }

            else
            {
                pause_menu.SetActive(true);
                Time.timeScale = 0f;
                select_list[0].Select();
            }
        }
    }

    //���ׂĂ̑f�ނ̏����󋵂̎擾
    private void All_Item_Val_Check()
    {
        foreach (var item in item_dic)
        {
            item_val[(int)item.Value] = new KeyValuePair<ItemID, int>(item.Value, item_manager.GetItemNum(item.Value));
        }
    }

    private void All_Cook_Val_Check()
    {
        foreach (var cook in cook_dic)
        {
            cook_val[(int)cook.Value] = new KeyValuePair<CookID, int>(cook.Value, cook_manager.GetCook(cook.Value));
        }
    }

    //�A�C�e�����X�g�̍쐬
    public void Item_List_Create()
    {
        if (item_contents.Count() > 0)
        {
            for (int i = 0; i < item_contents.Count; ++i)
            {
                Destroy(item_contents[i]);
            }
            item_contents = new List<GameObject>();
        }
        All_Item_Val_Check();
        foreach (var item in item_dic)
        {
            int val = item_val[(int)item.Value].Value;
            if (val <= 0) continue;
            GameObject obj = Instantiate<GameObject>(button_def, item_content.transform);
            Text new_txt = obj.GetComponentInChildren<Text>();
            new_txt.text = item.Key + " : " + val.ToString();
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            obj.GetComponentInChildren<Text>().text = new_txt.text;
            item_contents.Add(obj);
        }
    }

    //�A�C�e�����X�g�̍쐬
    public void Cook_List_Create()
    {
        if (item_contents.Count() > 0)
        {
            for (int i = 0; i < item_contents.Count; ++i)
            {
                Destroy(item_contents[i]);
            }
            item_contents = new List<GameObject>();
        }
        All_Cook_Val_Check();
        foreach (var cook in cook_dic)
        {
            int val = cook_val[(int)cook.Value].Value;
            if (val <= 0) continue;
            GameObject obj = Instantiate<GameObject>(button_def, item_content.transform);
            Text new_txt = obj.GetComponentInChildren<Text>();
            new_txt.text = cook.Key + " : " + val.ToString();
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            obj.GetComponentInChildren<Text>().text = new_txt.text;
            obj.GetComponentInChildren<Button>().onClick.AddListener(() => Check_Visible(cook.Value));
            item_contents.Add(obj);
        }
    }

    //�{�^�����m�̊֌W�������
    private void Make_Button_Relation()
    {
        List<Button> button_list = new List<Button>();
        foreach(var button_content in item_contents)
        {
            button_list.Add(button_content.GetComponent<Button>());
        }
        int cnt = button_list.Count;
        if (cnt == 0)
        {
            select_id = Select_ID.OverID;
            if (eat_check)
            {
                select_list[0].Select();
            }
        }
        for (int i = 0; i < cnt; ++i)
        {
            if (select_id != Select_ID.Cook)
            {
                //�{�^���̐F�ݒ�
                ColorBlock color_block = button_list[i].colors;
                color_block.normalColor = new Color(255, 255, 255);
                color_block.highlightedColor = new Color(255, 255, 255);
                color_block.pressedColor = new Color(255, 255, 255);
                color_block.selectedColor = new Color(255, 255, 255);
                color_block.disabledColor = new Color(255, 255, 255);
                color_block.colorMultiplier = 1;
                color_block.fadeDuration = 0.1f;

                button_list[i].colors = color_block;
                item_contents[i].GetComponent<Button>().colors = button_list[i].colors;
            }

            //�{�^���̃i�r�Q�[�V�����ݒ�
            Navigation nv = button_list[i].navigation;
            nv.mode = Navigation.Mode.Explicit;
            nv.selectOnUp = button_list[(cnt + i - 1) % cnt];
            nv.selectOnDown = button_list[(cnt + i + 1) % cnt];
            button_list[i].navigation = nv;
            
            item_contents[i].GetComponent<Button>().navigation = button_list[i].navigation;
            Debug.Log(item_contents[i].GetComponent<Button>().onClick);

            if (i == 0)
            {
                Debug.Log(item_contents[i]);
                item_contents[i].GetComponent<Button>().Select();
            }
        }

    }



    public void Item_List_Visible()
    {
        select_id = Select_ID.Item;
        Item_List_Create();
        Make_Button_Relation();
    }

    public void Cook_List_Visible()
    {
        select_id = Select_ID.Cook;
        Cook_List_Create();
        Make_Button_Relation();
    }

    private void Go_back()
    {
        if (select_id == Select_ID.OverID)
        {
            pause_menu.SetActive(false);
            Time.timeScale = 1.0f;
            return;
        }

        if (eat_check)
        {
            return;
        }

        if (item_contents.Count() > 0)
        {
            for (int i = 0; i < item_contents.Count; ++i)
            {
                Destroy(item_contents[i]);
            }
            item_contents = new List<GameObject>();
        }

        select_list[0].Select();
        select_id = Select_ID.OverID;
    }

    //�H�ׂ邩�ǂ����̊m�F��ʂ��o��
    public void Check_Visible(CookID id)
    {
        eat_set_menu = cook_manager.GetMenu(id);
        eat_set_id = id;
        eat_check = true;
        check_panel.SetActive(true);
        no_button.GetComponent<Button>().Select();
    }
    public void Check_Unvisible()
    {
        eat_check = false;
        check_panel.SetActive(false);
    }

    public void Eat() {
        if (player_status.Get_full_stomach() + eat_set_menu.full_stomach > Full) return;
        player_status.Add_hp(eat_set_menu.hp);
        player_status.Add_atk(eat_set_menu.atk);
        player_status.Add_spd(eat_set_menu.spd);
        player_status.Add_full_stomach(eat_set_menu.full_stomach);
        player_status.Heal(eat_set_menu.calory);
        cook_manager.EatCook(eat_set_id);
        Cook_List_Visible();
        Check_Unvisible();
    }

    public bool Get_is_pause_active()
    {
        return pause_menu.activeSelf;
    }
}
