using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System.Linq;

public class Cook : MonoBehaviour
{
    [SerializeField] EventSystem eventSystem;
    [SerializeField] ItemManager item_manager;
    [SerializeField] MenuManager menu_manager;
    [SerializeField] GameObject content;

    private int now_select;
    private GameObject selected_obj;
    [SerializeField] private List<GameObject> menu_button;
    private Dictionary<string, ItemID> dic;
    [SerializeField] private List<KeyValuePair<ItemID, int>> item_val;

    //選んだ料理のステータス
    int calore;
    int full;


    //[SerializeField,ReadOnly, HeaderAttribute("今持ってる素材の種類と数を")]
    // Start is called before the first frame update
    void Start()
    {
        menu_manager = transform.GetComponent<MenuManager>();
        Add_Menu_button();
        now_select = 0;
        dic = new Dictionary<string, ItemID>();
        dic.Add("キャベツ", ItemID.Cabbage);
        dic.Add("ニンジン", ItemID.Carrot);
        dic.Add("豚肉", ItemID.Pork);
        Debug.Log(dic.Count);
        item_val = new List<KeyValuePair<ItemID, int>>();
        foreach (var item in dic)
        {
            item_val.Add(new KeyValuePair<ItemID, int>(item.Value, 0));
        }
    }

    // Update is called once per frame
    void Update()
    {
        All_Item_Val_Check();
        All_Menu_Cookable_Check();
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
        selected_obj = eventSystem.currentSelectedGameObject.gameObject;
        now_select = 0;
        foreach (GameObject obj in menu_button)
        {
            if (selected_obj.name == obj.name) break;
            now_select++;

        }
        Debug.Log(now_select);
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
        List<KeyValuePair<string, int>> need_material;
        need_material = menu_manager.Get_Need_material(now_select);
        foreach (var material in need_material)
        {
            item_manager.SpendItem(dic[material.Key], material.Value);
        }
    }

    private void OnButtonPress()
    {
        selected_obj = eventSystem.currentSelectedGameObject.gameObject;
        selected_obj.GetComponent<Button>().onClick.Invoke();
    }
}
