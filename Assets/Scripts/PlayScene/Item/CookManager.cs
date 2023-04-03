using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CookID
{
    [InspectorName("やみつきキャベツ")] GoodCabegge,
    [InspectorName("豚とキャベツの冷しゃぶ")] PorkAndCabeggeSyabu,
    [InspectorName("豚肉とキャベツの甘辛みそ炒め")] PorkCabeggeAndMiso,
    [InspectorName("カットリンゴ")] CutApple,



    [InspectorName("")] ItemNum
}

public class CookManager : MonoBehaviour
{

    //  所持料理数
    [SerializeField, ReadOnly, Label("アイテム数")] List<int> cookNum;

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
