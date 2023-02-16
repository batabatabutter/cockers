using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    //�h�{�f�̍\����
    public struct Nutrients
    {
        public int carbohydrates;  //�Y������
        public int proteins;       //�^���p�N��
        public int lipid;          //����
        public int vitamins;       //�r�^�~��
        public int minerals;       //���@��

        public void Zero()
        {
            carbohydrates = 0;
            proteins = 0;
            lipid = 0;
            vitamins = 0;
            minerals = 0;
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

    //�l�̐ݒ�
    public void Set_carbohydrates(int val) { nutrients.carbohydrates = val; }
    public void Set_proteins(int val) { nutrients.proteins = val; }
    public void Set_lipid(int val) { nutrients.lipid = val; }
    public void Set_vitamins(int val) { nutrients.vitamins = val; }
    public void Set_minerals(int val) { nutrients.minerals = val; }

    //�l�̉��Z
    public void Add_carbohydrates(int val) { nutrients.carbohydrates += val; }
    public void Add_proteins(int val) { nutrients.proteins += val; }
    public void Add_lipid(int val) { nutrients.lipid += val; }
    public void Add_vitamins(int val) { nutrients.vitamins += val; }
    public void Add_minerals(int val) { nutrients.minerals += val; }

    //�l�̌��Z
    public void Sub_carbohydrates(int val) { nutrients.carbohydrates -= val; }
    public void Sub_proteins(int val) { nutrients.proteins -= val; }
    public void Sub_lipid(int val) { nutrients.lipid -= val; }
    public void Sub_vitamins(int val) { nutrients.vitamins -= val; }
    public void Sub_minerals(int val) { nutrients.minerals -= val; }


    //�l�̎擾
    public int Get_carbohydrates() { return nutrients.carbohydrates; }
    public int Get_proteins() { return nutrients.proteins; }
    public int Get_lipid() { return nutrients.lipid; }
    public int Get_vitamins() { return nutrients.vitamins; }
    public int Get_minerals() { return nutrients.minerals; }
}
