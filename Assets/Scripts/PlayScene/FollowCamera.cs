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

public class FollowCamera : MonoBehaviour
{
    [SerializeField, Label("ƒJƒƒ‰’Ç]ƒXƒe[ƒ^ƒX")] List<FollowCameraStatas> followStatas;
    [SerializeField, Label("’Ç]‘ÎÛ")] Transform followObject;

    [SerializeField, Label("’Ç]ˆÊ’u·")] Vector3 followDifference;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        foreach (FollowCameraStatas follow in followStatas)
        {
            //  ƒJƒƒ‰‚Æ’Ç]‘ÎÛ‚ÌˆÊ’uŠÖŒW”äŠr
            Vector3 cameraPos = new Vector3(this.transform.position.x, this.transform.position.y, 0.0f);
            if (Vector3.Distance(cameraPos, followObject.position + followDifference) <= follow.GetRange())
            {
                //  ‹ß‚©‚Á‚½‚çw’è‚³‚ê‚½’l‚Å’Ç]
                Vector3 targetPos = new Vector3(followObject.position.x, followObject.position.y, this.transform.position.z) + followDifference;
                transform.position = Vector3.MoveTowards(transform.position, targetPos, Time.deltaTime * follow.GetSpeed());
                break;
            }
        }
    }
}
