using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class StageSelectManager : MonoBehaviour
{ 

    public static StageID stageID; public StageID GetStageID() { return stageID; }

    // Update is called once per frame
    void Update()
    {
        var keyboad = Keyboard.current;
        Debug.Log(stageID);

        if (keyboad != null)
        {
            if (keyboad.leftArrowKey.wasPressedThisFrame)
            {
                stageID--;
            }
            if (keyboad.rightArrowKey.wasPressedThisFrame)
            {
                stageID++;
            }
            stageID = (StageID)Mathf.Clamp((float)stageID, (float)StageID.Stage1, (float)StageID.StageNum - 1);

            if (keyboad.zKey.wasPressedThisFrame)
            {
                SceneManager.LoadScene("StageWorks");
            }
        }
    }
}
