using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FollowCameraStatas
{
    [SerializeField, Label("追従範囲")] float range;
    [SerializeField, Label("追従速度")] float speed;

    public float GetRange() { return range; }    //  追従範囲受渡
    public float GetSpeed() { return speed; }    //  追従速度受渡
}

[System.Serializable]
public class Vector3Bool
{
    public bool x;
    public bool y;
    public bool z;
}

public class FollowCamera : MonoBehaviour
{
    [SerializeField, Label("カメラ追従ステータス")] List<FollowCameraStatas> followStatas;
    [SerializeField, Label("追従対象")] protected Transform followObject;

    [SerializeField, Label("追従位置差")] Vector3 followDifference; public Vector3 GetFollowDifference() { return followDifference; }
    [SerializeField, Label("無追従座標")] Vector3Bool dontFollowVec;

    // Start is called before the first frame update
    void Start()
    {
        CameraStart();
    }

    // Update is called once per frame
    void Update()
    {
        foreach (FollowCameraStatas follow in followStatas)
        {
            //  カメラと追従対象の位置関係比較
            Vector3 cameraPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            if (Vector3.Distance(cameraPos, followObject.position + followDifference) <= follow.GetRange())
            {
                //  近かったら指定された値で追従
                Vector3 followPos = new Vector3();
                //if (!dontFollowVec.GetX()) followPos.x = followObject.position.x;
                //if (!dontFollowVec.GetY()) followPos.y = followObject.position.y;
                //if (!dontFollowVec.GetZ()) followPos.z = followObject.position.z;
                if (!dontFollowVec.x) followPos.x = followObject.position.x;
                if (!dontFollowVec.y) followPos.y = followObject.position.y;
                if (!dontFollowVec.z) followPos.z = followObject.position.z;
                Vector3 targetPos = new Vector3(followPos.x, followPos.y, followPos.z) + followDifference;
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * follow.GetSpeed());
                break;
            }
        }
    }

    public virtual void CameraStart()
    {

    }
}
