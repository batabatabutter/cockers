using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FollowCameraStatas
{
    [SerializeField, Label("�Ǐ]�͈�")] float range;
    [SerializeField, Label("�Ǐ]���x")] float speed;

    public float GetRange() { return range; }    //  �Ǐ]�͈͎�n
    public float GetSpeed() { return speed; }    //  �Ǐ]���x��n
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
    [SerializeField, Label("�J�����Ǐ]�X�e�[�^�X")] List<FollowCameraStatas> followStatas;
    [SerializeField, Label("�Ǐ]�Ώ�")] protected Transform followObject;

    [SerializeField, Label("�Ǐ]�ʒu��")] Vector3 followDifference; public Vector3 GetFollowDifference() { return followDifference; }
    [SerializeField, Label("���Ǐ]���W")] Vector3Bool dontFollowVec;

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
            //  �J�����ƒǏ]�Ώۂ̈ʒu�֌W��r
            Vector3 cameraPos = new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z);
            if (Vector3.Distance(cameraPos, followObject.position + followDifference) <= follow.GetRange())
            {
                //  �߂�������w�肳�ꂽ�l�ŒǏ]
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
