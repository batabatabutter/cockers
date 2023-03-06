using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    //  �e�̑���
    [SerializeField] private float bulletSpeed;
    //  ��΂��~�T�C���I�u�W�F�N�g
    [SerializeField] private GameObject missilePrefab;
    //  ��
    [SerializeField] private GameObject head;
    //  �N�[���^�C��
    [SerializeField] private float coolTime;
    //  �J�E���g�p
    private float coolTimer;

    private void Start()
    {
        coolTimer = coolTime;
    }

    private void Update()
    {
        //  ���Ԃ����炷
        if (coolTimer > 0.0f)
        {
            coolTimer -= Time.deltaTime;
            return;
        }

        //  �N�[���^�C����ݒ�
        coolTimer = coolTime;

        //  �����ʒu���Z�o
        Vector3 createPos = head.transform.position + gameObject.transform.forward;
        //  ���˕������Z�o
        Vector3 bulletDir = gameObject.transform.forward * bulletSpeed;

        //  �e�𐶐�
        GameObject bullet = Instantiate(missilePrefab, createPos, Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f)));
        //  �e�̔��˕�����ݒ�
        bullet.GetComponent<CucumberMissile>().Shot(bulletDir);
    }
}
