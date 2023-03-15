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

public class FollowCamera : MonoBehaviour
{
    [SerializeField, Label("カメラ追従ステータス")] List<FollowCameraStatas> followStatas;
    [SerializeField, Label("追従対象")] Transform followObject;

    [SerializeField, Label("追従位置差")] Vector3 followDifference;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (FollowCameraStatas follow in followStatas)
        {
            //  カメラと追従対象の位置関係比較
            Vector3 cameraPos = new Vector3(this.transform.position.x, this.transform.position.y, 0.0f);
            if (Vector3.Distance(cameraPos, followObject.position + followDifference) <= follow.GetRange())
            {
                //  近かったら指定された値で追従
                Vector3 targetPos = new Vector3(followObject.position.x, followObject.position.y, this.transform.position.z) + followDifference;
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * follow.GetSpeed());
                break;
            }
        }
    }
}
