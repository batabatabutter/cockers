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
    [SerializeField] public GameObject item_manager_obj;
    private MenuManager menu_manager;
    [SerializeField] public GameObject content;
    private PlayerStatus player_status;
    [SerializeField] public GameObject item_text_def;

    public ItemManager item_manager;
    private CookManager cook_manager;

    private Text cooking_text;
    private int now_select;
    private GameObject selected_obj;
    [SerializeField] private List<GameObject> menu_button;
    private Dictionary<string, ItemID> dic;
    [SerializeField] private List<KeyValuePair<ItemID, int>> item_val;

    private GameObject item_content;
    private List<GameObject> item_text;
    private bool first_flg;

    private const int Full = 100;

    //確認画面用
    [SerializeField, HeaderAttribute("確認画面")] private GameObject check_panel;
    [SerializeField] private GameObject no_button;
    private bool eat_check;


    //[SerializeField,ReadOnly, HeaderAttribute("今持ってる素材の種類と数を")]
    // Start is called before the first frame update
    void Start()
    {
        item_manager = item_manager_obj.GetComponent<ItemManager>();
        cook_manager = item_manager_obj.GetComponent<CookManager>();
        menu_manager = transform.GetComponent<MenuManager>();
        Add_Menu_button();
        now_select = 0;
        dic = new Dictionary<string, ItemID>();
        dic.Add("キャベツ", ItemID.Cabbage);
        dic.Add("ニンジン", ItemID.Carrot);
        dic.Add("豚肉", ItemID.Pork);
        dic.Add("鶏肉", ItemID.Chicken);
        dic.Add("牛肉", ItemID.Beaf);
        dic.Add("玉ねぎ", ItemID.Onion);
        dic.Add("きゅうり", ItemID.Cucumber);
        dic.Add("大豆", ItemID.Been);
        dic.Add("卵", ItemID.Egg);
        dic.Add("牛乳", ItemID.Milk);
        dic.Add("ジャガイモ", ItemID.Potato);
        dic.Add("リンゴ", ItemID.Apple);
        dic.Add("イチゴ", ItemID.Strawberry);
        item_val = new List<KeyValuePair<ItemID, int>>();
        foreach (var item in dic)
        {
            item_val.Add(new KeyValuePair<ItemID, int>(item.Value, 0));
        }
        player_status = GameObject.Find("PlayManager").GetComponent<PlayManager>().GetPlayer().GetComponent<PlayerStatus>();
        item_content = GameObject.Find("ItemContent");
        first_flg = true;
        item_text = new List<GameObject>();
        cooking_text = GameObject.Find("MenuText").GetComponent<Text>();

        Transform children = check_panel.GetComponentInChildren<Transform>();
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
            first_flg = false;
        }
        var keyboard = Keyboard.current;
        if (keyboard.upArrowKey.wasReleasedThisFrame) Check_Now_SelectButton();
        if (keyboard.downArrowKey.wasReleasedThisFrame) Check_Now_SelectButton();
        if (keyboard.zKey.wasPressedThisFrame) OnButtonPress();
    }

    private void Add_Menu_button()
    {
        for (int i = 0; i < content.transform.childCount; ++i)
        {
            menu_button.Add(content.transform.GetChild(i).gameObject);
        }
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
        foreach (var item in dic)
        {
            item_val[(int)item.Value] = new KeyValuePair<ItemID, int>(item.Value, item_manager.GetItemNum(item.Value));
        }
    }

    private void All_Menu_Cookable_Check()
    {
        int menu_count = menu_manager.Get_Menu_Num();
        for (int i = 0; i < menu_count; ++i)
        {
            List<KeyValuePair<string, int>> need_material;
            need_material = menu_manager.Get_Need_material(i);
            bool flg = true;
            foreach (var material in need_material)
            {
                int have_val = item_val[(int)dic[material.Key]].Value;
                if (have_val < material.Value) flg = false;
            }
            menu_manager.Set_cookable(i, flg);
        }
    }

    public void Cooking()
    {
        if (!menu_manager.Get_menu_cookable(now_select)) return;
        Menu menu = menu_manager.Get_Menu(now_select);
        foreach (var material in menu.need_material)
        {
            item_manager.SpendItem(dic[material.Key], material.Value);
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
        for (int i = 0; i < menu_manager.Get_Menu_Num(); ++i) {
            menu_button[i].SetActive(menu_manager.Get_Menu_unlock(i));
        }
    }

    public void Item_List_Create()
    {
        if (item_text.Count() > 0)
        {
            for(int i = 0; i < item_text.Count; ++i)
            {
                Destroy(item_text[i]);
            }
            item_text = new List<GameObject>();
        }
        All_Item_Val_Check();
        foreach (var item in dic)
        {
            int val = item_val[(int)item.Value].Value;
            if (val <= 0) continue;
            GameObject obj = Instantiate<GameObject>(item_text_def,item_content.transform);
            Text new_txt = obj.GetComponent<Text>();
            new_txt.text = item.Key + " : " + val.ToString();
            obj.transform.localPosition = Vector3.zero;
            obj.transform.localScale = Vector3.one;
            obj.GetComponent<Text>().text = new_txt.text;
            item_text.Add(obj);
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
    }

    //Yesボタンを押したとき(Yesのとき)
    public void Eat()
    {
        Menu menu = menu_manager.Get_Menu(now_select);
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
        Check_Unvisible();
    }

    ///Noボタンを押したとき(Noのとき)
    public void Cook_Stack()
    {
        cook_manager.AddCook((CookID)now_select);
        Check_Unvisible();
    }
}
