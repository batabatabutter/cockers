using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisoBulletCreateFloor : MonoBehaviour
{
    [SerializeField, Label("落下速度")] float dropSpeed;
    [SerializeField, Label("オブジェクト")] GameObject obj;

    private Vector3 targetVec;
    private int dmg;
    private Vector3 vec;

    private float destroyTimeCount;
    private const float DESTROY_TIME = 10.0f;

    private void Start()
    {
        destroyTimeCount = DESTROY_TIME;
    }

    void Update()
    {
        //  落下
        vec += dropSpeed * Vector3.down * Time.deltaTime;

        //  移動
        transform.position += vec * Time.deltaTime;

        //  消滅時間
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

    //  ステータス設定
    public void SetBulletStatas(int dmg, float speed, float upSpeed, Vector3 targetPos)
    {
        this.dmg = dmg;
        if (targetPos.x > transform.position.x) targetVec = Vector3.right;
        else targetVec = Vector3.left;

        //float xSec = Vector3.Distance(new Vector3(targetPos.x, 0.0f, 0.0f), new Vector3(transform.position.x, 0.0f, 0.0f)) / speed;
        vec = speed * targetVec;
        vec += upSpeed * Vector3.up;
    }
}
