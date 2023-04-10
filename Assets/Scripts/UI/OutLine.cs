using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OutLine : MonoBehaviour
{
    Vector2 tmp;
    float ArmorLength;
    float HPLength;
    GameObject ArmorBer;
    GameObject HPber;
    public float HP = 100;
    // Start is called before the first frame update
    void Start()
    {
        ArmorBer = GameObject.Find("ArmorBer");
        HPber = GameObject.Find("HPber");
        tmp = GetComponent<RectTransform>().offsetMax;
    }

    // Update is called once per frame
    void Update()
    {
        ArmorLength = ArmorBer.GetComponent<ArmorBer>().Get_currentArmor();
        HPLength = HPber.GetComponent<HPber>().Get_HP();

        //Debug.Log(HPLength + ArmorLength);

        if (HPLength + ArmorLength <= HP)
        {
            GetComponent<RectTransform>().offsetMax = new Vector2(0f, 0f);
        }
        else if (HPLength + ArmorLength >= HP)
        {
            tmp.x = ArmorLength + HPLength - HP;
            tmp.x = tmp.x * 1f;
            GetComponent<RectTransform>().offsetMax = tmp;
        }
    }
}
