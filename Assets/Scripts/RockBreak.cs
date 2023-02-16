using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RockBreak : MonoBehaviour
{
    bool Rock = true;
    GameObject Object_Rock;
    [SerializeField] GameObject text;
    [SerializeField] GameObject text2;
    float time = 0.0f;


    // Start is called before the first frame update
    void Start()
    {
        Object_Rock = GameObject.Find("Rock");
    }

    // Update is called once per frame
    void Update()
    {
        var current = Keyboard.current;

        if (current.fKey.wasPressedThisFrame)
        {
            Debug.Log("‰ó‚ê‚½");
            text.SetActive(true);
            Destroy();
        }
        else if (current.eKey.wasPressedThisFrame)
        {
            Debug.Log("‰ó‚ê‚È‚¢");
            text2.SetActive(true);
            DontDestroy();
        }

        //2•bŒã‚ÉÁ‚¦‚é
        if (text.gameObject.activeSelf == true) time += Time.deltaTime;
        if (time >= 2.0f)
        {
            text.SetActive(false);
            time = 0f;
        }
        if (text2.gameObject.activeSelf == true) time += Time.deltaTime;
        if (time >= 2.0f)
        {
            text2.SetActive(false);
            time = 0f;
        }

    }

    //Šâ‚Ì”j‰ó
    public void Destroy()
    {
        Object_Rock.SetActive(false);
    }

    //Šâ‚ª”j‰óo—ˆ‚È‚¢
    public void DontDestroy()
    {
        Object_Rock.SetActive(true);
    }
}
