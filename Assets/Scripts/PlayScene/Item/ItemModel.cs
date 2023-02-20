using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemModel : MonoBehaviour
{
    //  �㉺�^���̑��x
    private float modelSpeed;
    //  �㉺�^���̕�
    private float modelRange;

    //  ���ԃJ�E���g�p
    private float totalTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        //  ���ԃJ�E���g
        totalTime += Time.deltaTime;

        //  �ʒu�ړ�
        transform.position += Vector3.up * Mathf.Sin(totalTime * modelSpeed) * Time.deltaTime * modelRange;
    }

    //  ���x�ݒ�
    public void SetModelSpeed(float speed)
    {
        modelSpeed = speed;
    }

    //  ���ݒ�
    public void SetModelRange(float range)
    {
        modelRange = range;
    }
}
