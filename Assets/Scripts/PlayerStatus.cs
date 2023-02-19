using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    //栄養素の構造体
    public struct Nutrients
    {
        public int carbohydrates;  //炭水化物
        public int proteins;       //タンパク質
        public int lipid;          //脂質
        public int vitamins;       //ビタミン
        public int minerals;       //無機質

        public void Zero()
        {
            carbohydrates = 80;
            proteins = 10;
            lipid = 50;
            vitamins = 20;
            minerals = 60;
        }
    }

    public static Nutrients nutrients;

    // Start is called before the first frame update
    void Start()
    {
        nutrients.Zero();
    }

    // Update is called once per frame
    //void Update()
    //{
        
    //}

    //値の設定
    public void Set_carbohydrates(int val) { nutrients.carbohydrates = val; }
    public void Set_proteins(int val) { nutrients.proteins = val; }
    public void Set_lipid(int val) { nutrients.lipid = val; }
    public void Set_vitamins(int val) { nutrients.vitamins = val; }
    public void Set_minerals(int val) { nutrients.minerals = val; }

    //値の加算
    public void Add_carbohydrates(int val) { nutrients.carbohydrates += val; }
    public void Add_proteins(int val) { nutrients.proteins += val; }
    public void Add_lipid(int val) { nutrients.lipid += val; }
    public void Add_vitamins(int val) { nutrients.vitamins += val; }
    public void Add_minerals(int val) { nutrients.minerals += val; }

    //値の減算
    public void Sub_carbohydrates(int val) { nutrients.carbohydrates -= val; }
    public void Sub_proteins(int val) { nutrients.proteins -= val; }
    public void Sub_lipid(int val) { nutrients.lipid -= val; }
    public void Sub_vitamins(int val) { nutrients.vitamins -= val; }
    public void Sub_minerals(int val) { nutrients.minerals -= val; }


    //値の取得
    public int Get_carbohydrates() { return nutrients.carbohydrates; }
    public int Get_proteins() { return nutrients.proteins; }
    public int Get_lipid() { return nutrients.lipid; }
    public int Get_vitamins() { return nutrients.vitamins; }
    public int Get_minerals() { return nutrients.minerals; }
}
