using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class KitchenManager : MonoBehaviour
{

    private bool IsPlayerIn;
    [SerializeField] GameObject KitchenPanel;

    // Start is called before the first frame update
    void Start()
    {
        IsPlayerIn = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(IsPlayerIn);
        var keyboard = Keyboard.current;
        if(IsPlayerIn && keyboard.cKey.wasPressedThisFrame)
        {
            KitchenPanel.SetActive(!KitchenPanel.activeSelf);
            if (KitchenPanel.activeSelf)
            {
                Time.timeScale = 0f;
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