using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MisoFloor : MonoBehaviour
{
    [SerializeField, Label("�_���[�W")] int dmg;
    [SerializeField, Label("���x(s)")] float speed;
    [SerializeField, Label("�ő咷��")] float maxHeight;
    [SerializeField, Label("�ҋ@����")] float beforeTime;
    [SerializeField, Label("�_���[�W��������")] float dmgTime;

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
        //  �����O
        if (beforeTimeCount > 0.0f)
        {
            beforeTimeCount -= Time.deltaTime;
            beforeTimeCount = Mathf.Max(beforeTimeCount, 0.0f);
        }
        //  �_���[�W��
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
