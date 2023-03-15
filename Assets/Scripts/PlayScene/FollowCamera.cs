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

public class FollowCamera : MonoBehaviour
{
    [SerializeField, Label("�J�����Ǐ]�X�e�[�^�X")] List<FollowCameraStatas> followStatas;
    [SerializeField, Label("�Ǐ]�Ώ�")] Transform followObject;

    [SerializeField, Label("�Ǐ]�ʒu��")] Vector3 followDifference;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (FollowCameraStatas follow in followStatas)
        {
            //  �J�����ƒǏ]�Ώۂ̈ʒu�֌W��r
            Vector3 cameraPos = new Vector3(this.transform.position.x, this.transform.position.y, 0.0f);
            if (Vector3.Distance(cameraPos, followObject.position + followDifference) <= follow.GetRange())
            {
                //  �߂�������w�肳�ꂽ�l�ŒǏ]
                Vector3 targetPos = new Vector3(followObject.position.x, followObject.position.y, this.transform.position.z) + followDifference;
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * follow.GetSpeed());
                break;
            }
        }
    }
}
