using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisoBullet : MonoBehaviour
{
    private float speed;
    private int dmg;

    private Vector3 targetVec;

    private float destroyTimeCount;
    private const float DESTROY_TIME = 10.0f;

    private void Start()
    {
        destroyTimeCount = DESTROY_TIME;
    }

    void Update()
    {
        //  �ړ�
        transform.position += speed * targetVec * Time.deltaTime;

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
    }

    //  �X�e�[�^�X�ݒ�
    public void SetBulletStatas(int dmg, float speed, Vector3 targetVec)
    {
        this.dmg = dmg;
        this.speed = speed;
        this.targetVec = targetVec;
    }
}
