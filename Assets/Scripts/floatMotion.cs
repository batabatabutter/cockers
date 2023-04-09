using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatMotion : MonoBehaviour
{
    [SerializeField, Label("移動速度")] float floatSpeed;
    [SerializeField, Label("移動間隔")] float floatDelay;
    [SerializeField, Label("移動ベクトル")] Vector3 floatVec;

    float timer = 0.0f;
    bool up = true;

    bool move = true; 
    public void SetMove(bool val) { move = val; }

    private void Update()
    {
        if (!move) return;

        //  タイマーカウント
        timer += Time.deltaTime;

        //  移動
        if (up)
            this.transform.position += floatVec.normalized * floatSpeed * Time.deltaTime;
        else
            this.transform.position -= floatVec.normalized * floatSpeed * Time.deltaTime;

        //  時間経過で変更
        if(timer > floatDelay)
        {
            if (up) up = false;
            else up = true;
            timer = 0.0f;
        }
    }
}
