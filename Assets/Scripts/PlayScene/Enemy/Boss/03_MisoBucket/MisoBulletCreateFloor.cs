using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisoBulletCreateFloor : MonoBehaviour
{
    private float speed;
    [SerializeField, Label("�������x")] float dropSpeed;
    [SerializeField, Label("�_���[�W")] int dmg;
    [SerializeField, Label("�I�u�W�F�N�g")] GameObject obj;

    private Vector3 vec;

    private float destroyTimeCount;
    private const float DESTROY_TIME = 5.0f;

    private void Start()
    {
        destroyTimeCount = DESTROY_TIME;
    }

    void Update()
    {
        //  �ړ�
        transform.position += dropSpeed * -transform.right * Time.deltaTime;

        //  ����
        transform.position += dropSpeed * Vector3.down * Time.deltaTime;

        //  ���Ŏ���
        destroyTimeCount -= Time.deltaTime;
        destroyTimeCount = Mathf.Clamp(destroyTimeCount, 0.0f, float.MaxValue);
        if (destroyTimeCount <= 0.0f) Destroy(gameObject);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerStatus>().Damage(dmg);
            Destroy(gameObject);
        }
        else if (other.CompareTag("Floor"))
        {
            Instantiate(obj, new Vector3(transform.position.x, transform.position.y, 0.0f), Quaternion.identity);
            Destroy(gameObject);
        }
    }

    //  �X�e�[�^�X�ݒ�
    public void SetBulletStatas(int dmg, float speed, Vector3 targetDis)
    {
        this.dmg = dmg;
        this.speed = speed;
        this.vec = targetDis;
    }
}
