using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CabbageTaro : MonoBehaviour
{
    //  �X�e�[�^�X�p
    [SerializeField] EnemyStatas statas;

    //  �ړ����x
    [SerializeField] float speed;

    //  �؂�ւ����^�C�~���O
    [SerializeField] float inversionTime;

    //  �Ԃ��������̃_���[�W�{��
    [SerializeField] float collisionDMG;

    //  ���ԃJ�E���g�p
    private float time;

    //  �ŏ��Ɏ��s
    private void Start()
    {
        time = inversionTime;
    }


    //  �X�V
    void Update()
    {
        //  ���Ԍ���
        time -= Time.deltaTime;
        //  �����������΍��E���]
        if (time < 0.0f)
        {
            this.gameObject.transform.Rotate(new Vector3(0.0f, 180.0f, 0.0f));
            time = inversionTime;
        }

        //  �ړ�
        this.gameObject.transform.position += speed * Time.deltaTime * this.gameObject.transform.right;
    }
}
