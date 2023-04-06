using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Linq;



public class Cook : MonoBehaviour
{
    [SerializeField] public EventSystem eventSystem;
    private GameObject item_manager_obj;
    private GameObject cook_manager_obj;
    [SerializeField] public GameObject content;
    private GameObject player;
    private PlayerStatus player_status;
    [SerializeField] public GameObject item_text_def;

    public ItemManager item_manager;
    private CookManager cook_manager;

    private Text cooking_text;
    private int now_select;
    private GameObject selected_obj;
    [SerializeField] private List<GameObject> menu_button;
    private Dictionary<string, ItemID> item_dic;
    [SerializeField] private List<KeyValuePair<ItemID, int>> item_val;

    private GameObject item_content;
    private List<GameObject> item_text_obj;
    private List<Text> item_text;
    private List<string> item_text_str;
    private List<string> item_text_no_val;
    private CookID now_select_id;
    private bool first_flg;
    Cook_Dictionary cook_dic;

    //メニュー一覧作成用
    [SerializeField, HeaderAttribute("追加するもののprefab")] public GameObject button_def;
    [SerializeField] private GameObject menu_content;
    private List<GameObject> menu_contents;
    private List<Text> menutext;

    private const int Full = 100;

    //確認画面用
    [SerializeField, HeaderAttribute("確認画面")] private GameObject check_panel;
    [SerializeField] private GameObject no_button;
    private bool eat_check;


    //[SerializeField,ReadOnly, HeaderAttribute("今持ってる素材の種類と数を")]
    // Start is called before the first frame update
    void Start()
    {
        item_manager_obj = GameObject.Find("ItemManager").gameObject;
        cook_manager_obj = GameObject.Find("CookManager").gameObject;
        item_manager = item_manager_obj.GetComponent<ItemManager>();
        cook_manager = cook_manager_obj.GetComponent<CookManager>();
        cook_dic = cook_manager.Get_Cook_Dictionary();
        now_select = 0;
        item_dic = new Dictionary<string, ItemID>();
        item_dic.Add("キャベツ", ItemID.Cabbage);
        item_dic.Add("ニンジン", ItemID.Carrot);
        item_dic.Add("豚肉", ItemID.Pork);
        item_dic.Add("鶏肉", ItemID.Chicken);
        item_dic.Add("牛肉", ItemID.Beaf);
        item_dic.Add("玉ねぎ", ItemID.Onion);
        item_dic.Add("きゅうり", ItemID.Cucumber);
        item_dic.Add("大豆", ItemID.Been);
        item_dic.Add("卵", ItemID.Egg);
        item_dic.Add("牛乳", ItemID.Milk);
        item_dic.Add("ジャガイモ", ItemID.Potato);
        item_dic.Add("リンゴ", ItemID.Apple);
        item_dic.Add("イチゴ", ItemID.Strawberry);
        item_val = new List<KeyValuePair<ItemID, int>>();
        foreach (var item in item_dic)
        {
            item_val.Add(new KeyValuePair<ItemID, int>(item.Value, 0));
        }
        player = GameObject.Find("PlayManager").GetComponent<PlayManager>().GetPlayer();
        player_status = player.GetComponent<PlayerStatus>();
        item_content = GameObject.Find("ItemContent");
        first_flg = true;
        item_text_obj = new List<GameObject>();
        item_text = new List<Text>();
        item_text_str = new List<string>();
        item_text_no_val = new List<string>();
        cooking_text = GameObject.Find("MenuText").GetComponent<Text>();

        Transform children = check_panel.GetComponentInChildren<Transform>();
        menu_contents = new List<GameObject>();

        Menu_List_Create();

        now_select_id = CookID.ItemNum;
    }

    // Update is called once per frame
    void Update()
    {
        All_Item_Val_Check();
        All_Menu_Cookable_Check();
        Menu_Visible_Check();
        if (first_flg)
        {
            Item_List_Create();
            Menu_List_Create();
            first_flg = false;
        }
        var keyboard = Keyboard.current;
        if (keyboard.upArrowKey.isPressed || keyboard.downArrowKey.isPressed)
        {
            Check_Now_SelectButton();
            All_Item_Spend_Check();
        }
        if (keyboard.upArrowKey.wasReleasedThisFrame) Check_Now_SelectButton();
        if (keyboard.downArrowKey.wasReleasedThisFrame) Check_Now_SelectButton();
        if (keyboard.zKey.wasPressedThisFrame) OnButtonPress();
    }

    //どの料理のボタンで止まったかを調べる
    private void Check_Now_SelectButton()
    {
        if (eat_check) return;
        selected_obj = eventSystem.currentSelectedGameObject.gameObject;
        now_select = 0;
        foreach (GameObject obj in menu_button)
        {
            if (selected_obj.name == obj.name) break;
            now_select++;
        }
    }

    //すべての素材の所持状況の取得
    private void All_Item_Val_Check()
    {
        foreach (var item in item_dic)
        {
            item_val[(int)item.Value] = new KeyValuePair<ItemID, int>(item.Value, item_manager.GetItemNum(item.Value));
        }
    }

    private void All_Menu_Cookable_Check()
    {
        int menu_count = menu_contents.Count;
        for (int i = 0; i < menu_count; ++i)
        {
            List<KeyValuePair<string, int>> need_material;
            need_material = cook_manager.Get_Need_material(i);
            bool flg = true;
            foreach (var material in need_material)
            {
                int have_val = item_val[(int)item_dic[material.Key]].Value;
                if (have_val < material.Value) flg = false;
            }
            cook_manager.Set_cookable(i, flg);
        }
    }

    private void All_Item_Spend_Check(GameObject select_obj = null)
    {
        string now_select = "";
        if (selected_obj == null) now_select = eventSystem.currentSelectedGameObject.gameObject.GetComponentInChildren<Text>().text;
        else now_select = selected_obj.GetComponentInChildren<Text>().text;
        CookID id = cook_dic.Search_CookID(now_select);
        if (id == now_select_id) return;
        now_select_id = id;
        if (!cook_manager.Get_menu_cookable(id)) return;
        Menu menu = cook_manager.Get_Menu(id);
        for(int i=0;i<item_text.Count;++i)
        {
            string str = item_text_no_val[i];
            item_text_obj[i].GetComponent<Text>().text = item_text_str[i];
            item_text[i].text = item_text_str[i];
            foreach (var material in menu.need_material)
            {
            
                if (str == material.Key)
                {
                    str = item_text_str[i];
                    int remining_items = item_manager.GetItemNum(item_dic[material.Key]) - material.Value;
                    str += "  →  " + remining_items.ToString();
                    item_text_obj[i].GetComponent<Text>().text = str;
                    item_text[i].text = str;
                }
            }
        }
    }

    public void Cooking(CookID id)
    {
        if (!cook_manager.Get_menu_cookable(id)) return;
        Menu menu = cook_manager.Get_Menu(id);
        foreach (var material in menu.need_material)
        {
            item_manager.SpendItem(item_dic[material.Key], material.Value);
        }
        All_Item_Val_Check();
        Item_List_Create();
        Check_Visible();
    }

    private void OnButtonPress()
    {
        selected_obj = eventSystem.currentSelectedGameObject.gameObject;
        selected_obj.GetComponent<Button>().onClick.Invoke();
    }

    public void Menu_Visible_Check()
    {
        for (int i = 0; i < menu_button.Count; ++i)
        {
            CookID id = cook_dic.Search_CookID(menutext[i].text);
            menu_button[i].SetActive(cook_manager.Get_Menu_unlock(id));
        }
    }



    /// <summary>
    /// リスト作成
    /// </summary>

    public void Item_List_Create()
    {
        if (item_text_obj.Count() > 0)
        {
            for (int i = 0; i < item_text_obj.Count; ++i)
            {
                Destroy(item_text_obj[i]);
            }
            item_text_obj = new List<GameObject>();
        }
        if (item_text.Count() > 0)
        {
            for (int i = 0; i < item_text.Count; ++i)
            {
                Destroy(item_text[i]);
            }
            item_text = new List<Text>();
        }
        if (item_text_str.Count() > 0)
        {
            item_text_str.Clear();
            item_text_str = new List<string>();
        }
        if (item_text_no_val.Count() > 0)
        {
            item_text_no_val.Clear();
            item_text_no_val = new List<string>();
        }

        All_Item_Val_Check();
        foreach (var item in item_dic)
        {
            int val = item_val[(int)item.Value].Value;
            if (val <= 0) continue;
            GameObject obj = Instantiate<GameObject>(item_text_def, item_content.transform);
            Text new_text = obj.GetComponent<Text>();
            string str = item.Key;
            item_text_no_val.Add(str);
            str += " : " + val.ToString();
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            new_text.text = str;
            obj.GetComponent<Text>().text = new_text.text;
            item_text_obj.Add(obj);
            item_text.Add(new_text);
            item_text_str.Add(str);
        }
    }

    public void Menu_List_Create()
    {
        menutext = new List<Text>();
        if (menutext.Count > 0)
        {
            for (int i = 0; i < menutext.Count; ++i)
            {
                Destroy(menutext[i]);
            }
            menutext = new List<Text>();
        }
        if (menu_contents.Count() > 0)
        {
            for (int i = 0; i < menu_contents.Count; ++i)
            {
                Destroy(menu_contents[i]);
            }
            menu_contents = new List<GameObject>();
        }
        if (menu_button.Count() > 0)
        {
            for (int i = 0; i < menu_button.Count; ++i)
            {
                Destroy(menu_button[i]);
            }
            menu_button = new List<GameObject>();
        }

        for (int i = 0; i < (int)CookID.ItemNum; ++i)
        {
            //料理を解禁していなかったらcontinue
            if (!cook_manager.Get_Menu_unlock((CookID)i)) continue;

            //解禁していたらボタン作成
            GameObject obj = Instantiate<GameObject>(button_def, menu_content.transform);
            Text new_txt = obj.GetComponentInChildren<Text>();
            new_txt.text = cook_dic.Search_Name((CookID)i);
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            obj.GetComponentInChildren<Text>().text = new_txt.text;
            CookID click_id = (CookID)i;
            obj.GetComponentInChildren<Button>().onClick.AddListener(() => Cooking(click_id));
            menu_button.Add(obj.GetComponentInChildren<Button>().gameObject);
            menu_contents.Add(obj);
            menutext.Add(new_txt);
        }
        Make_Button_Relation();
        All_Menu_Cookable_Check();
        if (menu_button.Count > 0)
        {
            menu_button[0].GetComponent<Button>().Select();
            Reset_now_select_id();
            All_Item_Spend_Check(menu_button[0]);
        }
    }

    //ボタン同士の関係性を作る
    private void Make_Button_Relation()
    {
        List<Button> button_list = new List<Button>();
        foreach (var button_content in menu_contents)
        {
            button_list.Add(button_content.GetComponent<Button>());
        }
        int cnt = button_list.Count;
        for (int i = 0; i < cnt; ++i)
        {
            //ボタンのナビゲーション設定
            Navigation nv = button_list[i].navigation;
            nv.mode = Navigation.Mode.Explicit;
            nv.selectOnUp = button_list[(cnt + i - 1) % cnt];
            nv.selectOnDown = button_list[(cnt + i + 1) % cnt];
            button_list[i].navigation = nv;

            menu_contents[i].GetComponent<Button>().navigation = button_list[i].navigation;

            if (i == 0)
            {
                menu_contents[i].GetComponent<Button>().Select();
            }
        }

    }

    //食べるかどうかの確認画面を出す
    public void Check_Visible()
    {
        eat_check = true;
        check_panel.SetActive(true);
        no_button.GetComponent<Button>().Select();
    }
    public void Check_Unvisible()
    {
        eat_check = false;
        check_panel.SetActive(false);
        menu_button[0].GetComponent<Button>().Select();
        Reset_now_select_id();
        All_Item_Spend_Check(menu_button[0]);
    }

    //Yesボタンを押したとき(Yesのとき)
    public void Eat()
    {
        Menu menu = cook_manager.Get_Menu(now_select);
        if (player_status.Get_full_stomach() + menu.full_stomach > Full)
        {
            Check_Unvisible();
            return;
        }
        player_status.Add_hp(menu.hp);
        player_status.Add_atk(menu.atk);
        player_status.Add_spd(menu.spd);
        player_status.Add_full_stomach(menu.full_stomach);
        player_status.Heal(menu.calory);

        //アクションスキル解放
        switch (menu.get_skill_name)
        {
            case "armor":
                player.GetComponent<Armor>().Allow_action_skill_to_player();
                break;

            case "charge_attack":
                player.GetComponent<ChargeAttack>().Allow_action_skill_to_player();
                break;

            case "dash":
                player.GetComponent<Dash>().Allow_action_skill_to_player();
                break;

            case "double_jump":
                player.GetComponent<DoubleJump>().Allow_action_skill_to_player();
                break;

            case "shield":
                player.GetComponent<Shield>().Allow_action_skill_to_player();
                break;

            case "special_attack":
                player.GetComponent<SpecialAttack>().Allow_action_skill_to_player();
                break;

            case "through_attack":
                player.GetComponent<ThrowingAttack>().Allow_action_skill_to_player();
                break;
        }

        Check_Unvisible();
    }

    ///Noボタンを押したとき(Noのとき)
    public void Cook_Stack()
    {
        cook_manager.AddCook((CookID)now_select);
        Check_Unvisible();
    }

    public bool Flg_Reset()
    {
        bool ret_flg = first_flg;
        first_flg = true;
        return ret_flg;
    }

    private void Reset_now_select_id()
    {
        now_select_id = CookID.ItemNum;
    }
}
