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
    public string name;             //������
    public int calory;              //�J�����[(�̗͉񕜗�)
    public int hp;                  //�̗�
    public int atk;                 //�U����
    public int spd;                 //�X�s�[�h
    public int full_stomach;        //�����x
    public List<KeyValuePair<string, int>> need_material = new List<KeyValuePair<string, int>>();
    public string get_skill_name;
    public bool is_unlock_key;
}

[System.Serializable]
public class InputMenu
{
    public string name;             //������
    public int calory;              //�J�����[(�̗͉񕜗�)
    public int hp;                  //�̗�
    public int atk;                 //�U����
    public int spd;                 //�X�s�[�h
    public int full_stomach;        //�����x
    public string[] need_material;  //�f��
    public string[] need_value;     //�f�ނ̕K�v��
    public string get_skill_name;
    public bool is_unlock_key;
}

public enum CookID
{
    [InspectorName("��݂��L���x�c")] GoodCabegge,
    [InspectorName("�؂ƃL���x�c�̗₵���")] PorkAndCabeggeSyabu,
    [InspectorName("�ؓ��ƃL���x�c�̊Ðh�݂��u��")] PorkCabeggeAndMiso,
    [InspectorName("�J�b�g�����S")] CutApple,



    [InspectorName("")] ItemNum
}

public class Cook_Dictionary
{
    Dictionary<string, CookID> cookid_dic;
    Dictionary<CookID, Menu> cook_dic;

    public Cook_Dictionary()
    {
        cookid_dic = new Dictionary<string, CookID>();
        cookid_dic.Add("��݂��L���x�c", CookID.GoodCabegge);
        cookid_dic.Add("�؂ƃL���x�c�̗₵���", CookID.PorkAndCabeggeSyabu);
        cookid_dic.Add("�ؓ��ƃL���x�c�̊Ðh�݂��u��", CookID.PorkCabeggeAndMiso);
        cookid_dic.Add("�J�b�g�����S", CookID.CutApple);

        cook_dic = new Dictionary<CookID, Menu>();
    }

    public void Add_cook_dic(Menu menu)
    {
        CookID id = Search_CookID(menu.name);
        cook_dic.Add(id, menu);
    }

    public CookID Search_CookID(string name)
    {
        foreach (var dicval in cookid_dic)
        {
            if (name == dicval.Key) return dicval.Value;
        }
        return CookID.ItemNum;
    }

    public CookID Search_CookID(Menu menu)
    {
        foreach (var dicval in cook_dic)
        {
            if (menu == dicval.Value) return dicval.Key;
        }
        return CookID.ItemNum;
    }

    public string Search_Name(CookID id)
    {
        foreach (var dicval in cookid_dic)
        {
            if (id == dicval.Value) return dicval.Key;
        }
        return "";
    }

    public string Search_Name(Menu menu)
    {
        CookID id = Search_CookID(menu);
        return Search_Name(id);
    }

    public Menu Search_Menu(CookID id)
    {
        foreach (var dicval in cook_dic)
        {
            if (id == dicval.Key) return dicval.Value;
        }
        return null;
    }

    public Menu Search_Menu(string name)
    {
        CookID id = Search_CookID(name);
        return Search_Menu(id);
    }
}

public class CookManager : MonoBehaviour
{

    public List<Menu> menu = new List<Menu>();
    private InputMenu[] inputmenu;
    private List<bool> menu_cookable;

    private static char erase_str = '"';
    Cook_Dictionary cook_dic;

    //  ����������
    [SerializeField, ReadOnly, Label("�A�C�e����")] List<int> cookNum;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void CookManager_Reset()
    {
        menu_cookable = new List<bool>();
        //�������O���t�@�C������ǂݍ���
        TextAsset csv_file = new TextAsset();
        csv_file = Resources.Load("CookData", typeof(TextAsset)) as TextAsset;
        inputmenu = CSVSerializer.Deserialize<InputMenu>(csv_file.text);
        int cnt = 0;
        cook_dic = new Cook_Dictionary();
        foreach (InputMenu m in inputmenu)
        {
            Menu new_menu = new Menu();
            new_menu.name = m.name;
            new_menu.calory = m.calory;
            new_menu.hp = m.hp;
            new_menu.atk = m.atk;
            new_menu.spd = m.spd;
            new_menu.full_stomach = m.full_stomach;
            for (int i = 0; i < m.need_material.Length; ++i)
            {
                string material = m.need_material[i];
                string value = m.need_value[i];
                material = material.Replace(erase_str.ToString(), "");
                value = value.Replace(erase_str.ToString(), "");
                new_menu.need_material.Add(new KeyValuePair<string, int>(material, int.Parse(value)));
            }
            new_menu.get_skill_name = m.get_skill_name;
            new_menu.is_unlock_key = m.is_unlock_key;
            menu.Add(new_menu);

            cook_dic.Add_cook_dic(new_menu);
            menu_cookable.Add(false);
            cnt++;
        }
        for (int i = 0; i < (int)CookID.ItemNum; ++i) cookNum.Add(0);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddCook(CookID id)
    {
        cookNum[(int)id]++;
    }

    public void EatCook(CookID id)
    {
        cookNum[(int)id]--;
    }

    public int GetCook(CookID id)
    {
        return cookNum[(int)id];
    }

    public Menu Get_Menu(CookID id)
    {
        return cook_dic.Search_Menu(id);
    }

    /// <summary>
    /// ��MenuManager
    /// </summary>

    public Menu Get_Menu(int button_id)
    {
        return menu[button_id];
    }

    //���݂̗������j���[���̎擾
    public int Get_Menu_Num()
    {
        return menu.Count;
    }

    //���j���[�ɕK�v�ȑf�ވꗗ�̎擾
    public List<KeyValuePair<string, int>> Get_Need_material(int button_id)
    {
        return menu[button_id].need_material;
    }

    //���j���[�����鎞�Ɏg��
    public void Set_cookable(int button_id, bool cookable)
    {
        menu_cookable[button_id] = cookable;
        //if (cookable)
        //{
        //    menutext[button_id].color = new Color(1.0f, 0.67711f, 0.0f);
        //}
        //else
        //{
        //    menutext[button_id].color = new Color(0.3098039f, 0.3098039f, 0.3098039f);
        //}
    }

    //���̃��j���[�����邩�ǂ���������
    public bool Get_menu_cookable(CookID id)
    {
        return menu_cookable[(int)id];
    }

    public void New_Menu_unlock(string menu_name)
    {
        for (int i = 0; i < menu.Count; ++i)
        {
            if (menu[i].name == menu_name)
            {
                menu[i].is_unlock_key = true;
            }
        }
    }

    public bool Get_Menu_unlock(CookID id)
    {
        return menu[(int)id].is_unlock_key;
    }

    public Cook_Dictionary Get_Cook_Dictionary()
    {
        return cook_dic;
    }
}
