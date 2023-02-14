using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialManager : MonoBehaviour
{
    [SerializeField] Material iris;

    public bool in_out_check;
    public bool iris_flg = false;
    public bool finish_flg = false;

    [SerializeField] private float speed = 1.0f;

    //���̃N���X�𑼂Ŏg����悤�ɂ��邽�߂̃C���X�^���X
    public static MaterialManager instance;

    //�C���X�^���X��null�̎��A�V������������
    public void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (iris_flg)
        {
            float value = iris.GetFloat("_Threshold");
            value += speed * Time.deltaTime;
            
            if (in_out_check && value >= 1.0f)
            {
                value = 1.0f;
                iris_flg = false;
                finish_flg = true;
            }
            else if(!in_out_check && value <= 0.0f)
            {
                value = 0.0f;
                iris_flg = false;
                finish_flg = true;
            }

            iris.SetFloat("_Threshold", value);
        }
    }

    public void Iris_Start()
    {
        iris_flg = true;
        finish_flg = false;
        float value = iris.GetFloat("_Threshold");
        if (value >= 0.5f)
        {
            in_out_check = true;
            speed = -1;
        }
        else
        {
            in_out_check = false;
            speed = 1;
        }
    }

    public float Get_val()
    {
        return iris.GetFloat("_Threshold");
    }
}
