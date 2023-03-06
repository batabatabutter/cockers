using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    //  弾の速さ
    [SerializeField] private float bulletSpeed;
    //  飛ばすミサイルオブジェクト
    [SerializeField] private GameObject missilePrefab;
    //  頭
    [SerializeField] private GameObject head;
    //  クールタイム
    [SerializeField] private float coolTime;
    //  カウント用
    private float coolTimer;

    private void Start()
    {
        coolTimer = coolTime;
    }

    private void Update()
    {
        //  時間を減らす
        if (coolTimer > 0.0f)
        {
            coolTimer -= Time.deltaTime;
            return;
        }

        //  クールタイムを設定
        coolTimer = coolTime;

        //  生成位置を算出
        Vector3 createPos = head.transform.position + gameObject.transform.forward;
        //  発射方向を算出
        Vector3 bulletDir = gameObject.transform.forward * bulletSpeed;

        //  弾を生成
        GameObject bullet = Instantiate(missilePrefab, createPos, Quaternion.Euler(new Vector3(0.0f, 0.0f, 90.0f)));
        //  弾の発射方向を設定
        bullet.GetComponent<CucumberMissile>().Shot(bulletDir);
    }
}
