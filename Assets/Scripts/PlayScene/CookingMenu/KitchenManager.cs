using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KitchenManager : MonoBehaviour
{

    private bool IsPlayerIn;
    [SerializeField] GameObject KitchenPanel;
    [SerializeField] private FirstButtonSelect first_button_select;
    [SerializeField] private Cook cook;

    private PauseManager pause_manager;

    // Start is called before the first frame update
    void Start()
    {
        IsPlayerIn = false;
        pause_manager = GameObject.Find("PauseManager").GetComponent<PauseManager>();
        //KitchenPanel = GameObject.Find("CookingCamvas");
    }

    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;
        if (pause_manager.Get_is_pause_active()) return;
        if(IsPlayerIn && keyboard.cKey.wasPressedThisFrame)
        {
            KitchenPanel.SetActive(!KitchenPanel.activeSelf);
            if (KitchenPanel.activeSelf)
            {
                Time.timeScale = 0f;
                first_button_select.First_Select();
                //cook.Menu_Visible_Check();
                //cook.Item_List_Create();
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerIn = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            IsPlayerIn = false;
        }
    }
}
