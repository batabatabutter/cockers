using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floatMotion : MonoBehaviour
{
    [SerializeField, Label("�ړ����x")] float floatSpeed;
    [SerializeField, Label("�ړ��Ԋu")] float floatDelay;
    [SerializeField, Label("�ړ��x�N�g��")] Vector3 floatVec;

    float timer = 0.0f;
    bool up = true;

    bool move = true; 
    public void SetMove(bool val) { move = val; }

    private void Update()
    {
        if (!move) return;

        //  �^�C�}�[�J�E���g
        timer += Time.deltaTime;

        //  �ړ�
        if (up)
            this.transform.position += floatVec.normalized * floatSpeed * Time.deltaTime;
        else
            this.transform.position -= floatVec.normalized * floatSpeed * Time.deltaTime;

        //  ���Ԍo�߂ŕύX
        if(timer > floatDelay)
        {
            if (up) up = false;
            else up = true;
            timer = 0.0f;
        }
    }
}
