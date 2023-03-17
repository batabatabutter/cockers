using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : FollowCamera
{
    public override void CameraStart()
    {
        followObject = GameObject.FindGameObjectWithTag("Player").transform;
    }
}
