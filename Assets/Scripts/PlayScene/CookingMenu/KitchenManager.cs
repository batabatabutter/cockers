using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KitchenManager : MonoBehaviour
{

    private bool IsPlayerIn;
    GameObject KitchenPanel;
    private Cook cook;

    private PauseManager pause_manager;

    // Start is called before the first frame update
    void Start()
    {
        IsPlayerIn = false;
        pause_manager = GameObject.Find("PauseManager").GetComponent<PauseManager>();
        KitchenPanel = GameObject.FindGameObjectWithTag("PlayManager").GetComponent<PlayManager>().GetCookingCamvas();
        cook = KitchenPanel.GetComponentInChildren<Cook>();
        KitchenPanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        var keyboard = Keyboard.current;
        if (pause_manager.Get_is_pause_active()) return;
        if (IsPlayerIn && keyboard.cKey.wasPressedThisFrame)
        {
            KitchenPanel.SetActive(!KitchenPanel.activeSelf);
            if (KitchenPanel.activeSelf)
            {
                Time.timeScale = 0f;
                //cook.Menu_Visible_Check();
                //cook.Item_List_Create();
            }
            else
            {
                Time.timeScale = 1f;
            }
        }
        if (KitchenPanel.activeSelf)
        {
            Time.timeScale = 0f;
        }
    }



    /// <summary>
    /// キッチンの画面表示、非表示
    /// </summary>

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
