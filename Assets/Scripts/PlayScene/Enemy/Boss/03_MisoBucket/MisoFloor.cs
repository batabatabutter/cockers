using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisoFloor : MonoBehaviour
{
    [SerializeField, Label("ダメージ")] int dmg;
    [SerializeField, Label("速度(s)")] float speed;
    [SerializeField, Label("最大長さ")] float maxHeight;
    [SerializeField, Label("待機時間")] float beforeTime;
    [SerializeField, Label("ダメージ発生時間")] float dmgTime;

    float beforeTimeCount = 0.0f;
    float dmgTimeCount    = 0.0f;

    private void Start()
    {
        beforeTimeCount = beforeTime;
        dmgTimeCount = dmgTime;
    }

    // Update is called once per frame
    void Update()
    {
        //  発生前
        if (beforeTimeCount > 0.0f)
        {
            beforeTimeCount -= Time.deltaTime;
            beforeTimeCount = Mathf.Max(beforeTimeCount, 0.0f);
        }
        //  ダメージ中
        else if(dmgTimeCount > 0.0f)
        {
            dmgTimeCount -= Time.deltaTime;
            dmgTimeCount = Mathf.Max(dmgTimeCount, 0.0f);

            transform.localScale = new Vector3(transform.localScale.x, Mathf.Min(gameObject.transform.localScale.y + (speed * Time.deltaTime), maxHeight), transform.localScale.z);

        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && dmgTimeCount > 0.0f)
        {
            other.GetComponent<PlayerStatus>().Damage(dmg);
        }
    }
}
