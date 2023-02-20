using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using ServiceStack.Text;

[System.Serializable]
public class Menu
{
    public string name;  //������
    public int calory;          //�J�����[(�̗͉񕜗�)
    public int carbohydrates;   //�Y������
    public int proteins;        //�^���p�N��
    public int lipid;           //����
    public int vitamins;        //�r�^�~��
    public int minerals;        //���@��
    public int full_stomach;
    public List<KeyValuePair<string, int>> need_material = new List<KeyValuePair<string, int>>();
}

[System.Serializable]
public class InputMenu
{
    public string name;             //������
    public int calory;              //�J�����[(�̗͉񕜗�)
    public int carbohydrates;       //�Y������
    public int proteins;            //�^���p�N��
    public int lipid;               //����
    public int vitamins;            //�r�^�~��
    public int minerals;            //���@��
    public int full_stomach;        //�����x
    public string[] need_material;  //�f��
    public string[] need_value;     //�f�ނ̕K�v��
}

public class MenuManager : MonoBehaviour
{

    [SerializeField] public List<Menu> menu=new List<Menu>();
    [SerializeField] private InputMenu[] inputmenu;

    private static char erase_str = '"';

    // Start is called before the first frame update
    void Start()
    {
        TextAsset csv_file = new TextAsset();
        csv_file = Resources.Load("CookData",typeof(TextAsset)) as TextAsset;
        inputmenu = CSVSerializer.Deserialize<InputMenu>(csv_file.text);
        
        foreach(InputMenu m in inputmenu)
        {
            Menu new_menu = new Menu();
            new_menu.name = m.name;
            new_menu.calory = m.calory;
            new_menu.carbohydrates=m.carbohydrates;
            new_menu.proteins=m.proteins;
            new_menu.lipid=m.lipid;
            new_menu.vitamins=m.vitamins;
            new_menu.minerals=m.minerals;
            new_menu.full_stomach= m.full_stomach;
            for(int i = 0; i < m.need_material.Length; ++i)
            {
                Debug.Log(i);
                string material = m.need_material[i];
                string value = m.need_value[i];
                Debug.Log("before : " + material + " : " + value);
                material=material.Replace(erase_str.ToString(), "");
                value=value.Replace(erase_str.ToString(), "");
                Debug.Log("after : " + material + " : " + value);
                new_menu.need_material.Add(new KeyValuePair<string,int>(material,int.Parse(value)));
            }
            menu.Add(new_menu);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
