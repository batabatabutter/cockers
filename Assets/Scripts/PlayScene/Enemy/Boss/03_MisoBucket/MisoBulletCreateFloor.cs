using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisoBulletCreateFloor : MonoBehaviour
{
    private float speed;
    [SerializeField, Label("落下速度")] float dropSpeed;
    [SerializeField, Label("ダメージ")] int dmg;
    [SerializeField, Label("オブジェクト")] GameObject obj;

    private Vector3 vec;

    private float destroyTimeCount;
    private const float DESTROY_TIME = 5.0f;

    private void Start()
    {
        destroyTimeCount = DESTROY_TIME;
    }

    void Update()
    {
        //  移動
        transform.position += dropSpeed * -transform.right * Time.deltaTime;

        //  落下
        transform.position += dropSpeed * Vector3.down * Time.deltaTime;

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
    public void SetBulletStatas(int dmg, float speed, Vector3 targetDis)
    {
        this.dmg = dmg;
        this.speed = speed;
        this.vec = targetDis;
    }
}
