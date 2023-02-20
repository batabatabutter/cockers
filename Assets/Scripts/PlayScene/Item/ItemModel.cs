using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemModel : MonoBehaviour
{
    //  上下運動の速度
    private float modelSpeed;
    //  上下運動の幅
    private float modelRange;

    //  時間カウント用
    private float totalTime = 0.0f;

    // Update is called once per frame
    void Update()
    {
        //  時間カウント
        totalTime += Time.deltaTime;

        //  位置移動
        transform.position += Vector3.up * Mathf.Sin(totalTime * modelSpeed) * Time.deltaTime * modelRange;
    }

    //  速度設定
    public void SetModelSpeed(float speed)
    {
        modelSpeed = speed;
    }

    //  幅設定
    public void SetModelRange(float range)
    {
        modelRange = range;
    }
}
