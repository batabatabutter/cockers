using System;
using System.Text;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Menu
{
    public string name;             //料理名
    public int calory;              //カロリー(体力回復量)
    public int hp;                  //体力
    public int atk;                 //攻撃力
    public int spd;                 //スピード
    public int full_stomach;        //満腹度
    public List<KeyValuePair<string, int>> need_material = new List<KeyValuePair<string, int>>();
    public string get_skill_name;
    public string unlodk_key;
}

[System.Serializable]
public class InputMenu
{
    public string name;             //料理名
    public int calory;              //カロリー(体力回復量)
    public int hp;                  //体力
    public int atk;                 //攻撃力
    public int spd;                 //スピード
    public int full_stomach;        //満腹度
    public string[] need_material;  //素材
    public string[] need_value;     //素材の必要数
    public string get_skill_name;
    public string unlodk_key;
}

public class MenuManager : MonoBehaviour
{

    [SerializeField] public List<Menu> menu=new List<Menu>();
    [SerializeField] private InputMenu[] inputmenu;
    [SerializeField] private List<Text> menutext;
    private List<bool> menu_cookable;

    private static char erase_str = '"';

    // Start is called before the first frame update
    void Start()
    {
        menu_cookable = new List<bool>();
        //料理を外部ファイルから読み込む
        TextAsset csv_file = new TextAsset();
        csv_file = Resources.Load("CookData",typeof(TextAsset)) as TextAsset;
        inputmenu = CSVSerializer.Deserialize<InputMenu>(csv_file.text);
        int cnt = 0;
        foreach(InputMenu m in inputmenu)
        {
            Menu new_menu = new Menu();
            new_menu.name = m.name;
            new_menu.calory = m.calory;
            new_menu.hp=m.hp;
            new_menu.atk=m.atk;
            new_menu.spd=m.spd;
            new_menu.full_stomach= m.full_stomach;
            for(int i = 0; i < m.need_material.Length; ++i)
            {
                string material = m.need_material[i];
                string value = m.need_value[i];
                material=material.Replace(erase_str.ToString(), "");
                value=value.Replace(erase_str.ToString(), "");
                new_menu.need_material.Add(new KeyValuePair<string,int>(material,int.Parse(value)));
            }
            new_menu.get_skill_name = m.get_skill_name;
            new_menu.unlodk_key = m.unlodk_key;
            menu.Add(new_menu);
            menutext[cnt].text = new_menu.name;
            menu_cookable.Add(false);
            cnt++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Menu Get_Menu(int button_id) {
        return menu[button_id];
    }

    //現在の料理メニュー数の取得
    public int Get_Menu_Num()
    {
        return menu.Count;
    }

    //メニューに必要な素材一覧の取得
    public List<KeyValuePair<string,int>> Get_Need_material(int button_id)
    {
        return menu[button_id].need_material;
    }

    public void Set_cookable(int button_id, bool cookable)
    {
        menu_cookable[button_id] = cookable;
        if (cookable)
        {
            menutext[button_id].color = new Color(1.0f, 0.67711f, 0.0f);
        }
        else
        {
            menutext[button_id].color = new Color(0.3098039f, 0.3098039f, 0.3098039f);
        }
    }

    public bool Get_menu_cookable(int button_id)
    {
        return menu_cookable[button_id];
    }
}
