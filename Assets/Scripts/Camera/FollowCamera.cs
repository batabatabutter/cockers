using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FollowCameraStatas
{
    [SerializeField, Label("’Ç]”ÍˆÍ")] float range;
    [SerializeField, Label("’Ç]‘¬“x")] float speed;

    public float GetRange() { return range; }    //  ’Ç]”ÍˆÍó“n
    public float GetSpeed() { return speed; }    //  ’Ç]‘¬“xó“n
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
    [SerializeField, Label("ƒJƒƒ‰’Ç]ƒXƒe[ƒ^ƒX")] List<FollowCameraStatas> followStatas;
    [SerializeField, Label("’Ç]‘ÎÛ")] protected Transform followObject;

    [SerializeField, Label("’Ç]ˆÊ’u·")] Vector3 followDifference; public Vector3 GetFollowDifference() { return followDifference; }
    [SerializeField, Label("–³’Ç]À•W")] Vector3Bool dontFollowVec;

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
            //  ƒJƒƒ‰‚Æ’Ç]‘ÎÛ‚ÌˆÊ’uŠÖŒW”äŠr
            Vector3 cameraPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            if (Vector3.Distance(cameraPos, followObject.position + followDifference) <= follow.GetRange())
            {
                //  ‹ß‚©‚Á‚½‚çw’è‚³‚ê‚½’l‚Å’Ç]
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
