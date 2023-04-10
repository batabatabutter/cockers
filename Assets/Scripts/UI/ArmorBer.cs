using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArmorBer : MonoBehaviour
{
    private float currentArmor;
    public Slider slider;
    GameObject PlayerStatus;
    GameObject HPber;
    Vector3 tmp;
    float HPberPos;
    int adjustment = 100;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStatus = GameObject.Find("PlayManager").GetComponent<PlayManager>().GetPlayer();
        HPber = GameObject.Find("HPber");
        tmp = this.GetComponent<RectTransform>().position;

    }

    // Update is called once per frame
    void Update()
    {
        HPberPos = HPber.GetComponent<HPber>().Get_HPberPos();
        currentArmor = PlayerStatus.GetComponent<Armor>().Get_Value();
        slider.value = currentArmor;

        tmp.x = HPberPos + adjustment;

        this.GetComponent<RectTransform>().position = tmp;
    }

    public float Get_currentArmor() { return currentArmor; }

}
