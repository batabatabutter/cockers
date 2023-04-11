using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Stomach : MonoBehaviour
{
    GameObject Player;                  //ÉvÉåÉCÉÑÅ[äiî[óp
    GameObject image;                   //Imageäiî[óp
    float stomach;                      //ãÛï†
    float adjustment = 0.01f;           //í≤êÆ


    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("PlayManager").GetComponent<PlayManager>().GetPlayer(); 
        image = GameObject.Find("Stomach");
    }

    // Update is called once per frame
    void Update()
    {
        stomach = Player.GetComponent<PlayerStatus>().Get_full_stomach();
        image.GetComponent<Image>().fillAmount = stomach * adjustment;
    }
}
