using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CookID
{
    [InspectorName("��݂��L���x�c")] GoodCabegge,
    [InspectorName("�؂ƃL���x�c�̗₵���")] PorkAndCabeggeSyabu,
    [InspectorName("�ؓ��ƃL���x�c�̊Ðh�݂��u��")] PorkCabeggeAndMiso,
    [InspectorName("�J�b�g�����S")] CutApple,



    [InspectorName("")] ItemNum
}

public class CookManager : MonoBehaviour
{

    //  ����������
    [SerializeField, ReadOnly, Label("�A�C�e����")] List<int> cookNum;

    private MenuManager menu_manager;

    // Start is called before the first frame update
    void Start()
    {
        //menu_manager = GameObject.Find("CookingMenu").GetComponent<MenuManager>();
        //for (int i = 0; i < (int)CookID.ItemNum; ++i) cookNum.Add(0);
    }

    public void CookManager_Reset() {
        menu_manager = GameObject.Find("CookingMenu").GetComponent<MenuManager>();
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

    public Menu GetMenu(CookID id)
    {
        return menu_manager.Get_Menu((int)id);
    }
}
