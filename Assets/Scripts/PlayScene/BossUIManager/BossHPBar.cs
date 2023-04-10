using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHPBar : MonoBehaviour
{
    [System.Serializable]
    private class HPBarColor
    {
        [Label("��{�F")] public Color BarColor;
        [Label("���ŐF")] public Color BarDecreaseColor;
    };

    [Header("HP�o�[�̌�����")]
    [SerializeField, Label("HP�o�[OBJ")] GameObject hpBarObj;
    [SerializeField, Label("�Q�[�W�F�ꗗ")] List<HPBarColor> hpBarColors;
    [SerializeField, Label("�Q�[�W����x")] float decreaseHpSpeed;
    [SerializeField, Label("1�Q�[�W��HP")] int oneGaugeHP;
    [Header("HP�o�[�{���\��")]
    [SerializeField, Label("�o�[���\��OBJ")] GameObject hpBarNumObj;
    [SerializeField, Label("�o�[�\���Ԋu")] Vector2 numBetWeen;

    //  HP�Q�[�W�̐�
    private int hpGaugeNum;
    
    //  HP�o�[�I�u�W�F�N�g
    private List<GameObject> hpBars;
    //  ����HP�o�[�I�u�W�F�N�g
    private List<GameObject> decreaseHpBars;
    //  HP�o�[�\���I�u�W�F�N�g
    private List<GameObject> hpBarNumObjs;

    //  ����HP���l
    private float decreaseHpBarVal;

    //  �{�X
    private Boss boss;

    private void Awake()
    {
        decreaseHpBars = new List<GameObject>();
        hpBars = new List<GameObject>();
        hpBarNumObjs = new List<GameObject>();
    }

    //  �X�V
    void Update()
    {
        //  �ʏ�F
        for (int i = 0; i < hpGaugeNum; i++)
        {
            //  �o�[�̈ʒu����
            decreaseHpBars[i].transform.position = this.transform.position;
            hpBars[i].transform.position = this.transform.position;

            //  ����HP�o�[
            if (boss.GetNowBossHP() < decreaseHpBarVal)
                decreaseHpBarVal -= decreaseHpSpeed * Time.deltaTime;
            decreaseHpBarVal = Mathf.Max(decreaseHpBarVal, boss.GetNowBossHP());
            decreaseHpBarVal = Mathf.Max(decreaseHpBarVal, 0.0f);
            {
                //  �܂��O�̃Q�[�W������Ƃ�
                if (oneGaugeHP * (i + 1) < decreaseHpBarVal)
                    decreaseHpBars[i].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                //  �������̃Q�[�W�ɍs���Ă���Ƃ�
                else if (oneGaugeHP * i >= decreaseHpBarVal)
                    decreaseHpBars[i].transform.localScale = new Vector3(0.0f, 1.0f, 1.0f);

                else
                {
                    //  ����
                    float hpBarPer = (float)(decreaseHpBarVal - oneGaugeHP * (float)((int)decreaseHpBarVal / oneGaugeHP)) / (float)oneGaugeHP;
                    decreaseHpBars[i].transform.localScale = new Vector3(hpBarPer, 1.0f, 1.0f);
                    Vector3 pos = decreaseHpBars[i].transform.position;

                    //  �ړ�
                    pos.x -= gameObject.GetComponent<RectTransform>().rect.width / 2 * (1 - hpBarPer);
                    decreaseHpBars[i].transform.position = pos;
                }
            }

            //  �ʏ�o�[
            //  �܂��O�̃Q�[�W������Ƃ�
            if (oneGaugeHP * (i + 1) < boss.GetNowBossHP())
                hpBars[i].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            //  �������̃Q�[�W�ɍs���Ă���Ƃ�
            else if (oneGaugeHP * i >= boss.GetNowBossHP())
                hpBars[i].transform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
            //  ���̃Q�[�W�̊����v�Z
            else
            {
                //  ����
                float hpBarPer = (float)(boss.GetNowBossHP() - oneGaugeHP * i) / (float)oneGaugeHP;
                hpBars[i].transform.localScale = new Vector3(hpBarPer, 1.0f, 1.0f);
                Vector3 pos = hpBars[i].transform.position;

                //  �ړ�
                pos.x -= gameObject.GetComponent<RectTransform>().rect.width / 2 * (1 - hpBarPer);
                hpBars[i].transform.position = pos;
            }

            //  �o�[�̐��\��
            //  �������̃Q�[�W�ɍs���Ă���Ƃ�
            if (oneGaugeHP * i >= boss.GetNowBossHP())
                hpBarNumObjs[i].SetActive(false);
            else
                hpBarNumObjs[i].SetActive(true);

        }
    }

    //  �ݒ�
    public void SetBoss(GameObject boss)
    {
        //  ���Z�b�g
        decreaseHpBars.Clear();
        hpBars.Clear();
        hpBarNumObjs.Clear();

        this.boss = boss.GetComponent<Boss>();

        //  �l�Z�b�g
        hpGaugeNum = this.boss.GetMaxBossHP() / oneGaugeHP + 1;
        decreaseHpBarVal = this.boss.GetMaxBossHP();

        //  HP�o�[
        for (int i = 0; i < hpGaugeNum; i++)
        {
            //  ���Ńo�[�쐬
            GameObject bar = Instantiate(hpBarObj, gameObject.transform);
            decreaseHpBars.Add(bar);
            decreaseHpBars[i].GetComponent<UnityEngine.UI.RawImage>().color = hpBarColors[i].BarDecreaseColor;

            //  �ʏ�o�[�쐬
            bar = Instantiate(hpBarObj, gameObject.transform);
            hpBars.Add(bar);
            hpBars[i].GetComponent<UnityEngine.UI.RawImage>().color = hpBarColors[i].BarColor;

            //  �Q�[�W�\���ǉ�
            bar = Instantiate(hpBarNumObj, gameObject.transform);
            hpBarNumObjs.Add(bar);
            hpBarNumObjs[i].GetComponent<UnityEngine.UI.RawImage>().color = hpBarColors[i].BarColor;
            //  �ʒu����
            Vector3 pos = hpBarNumObjs[i].transform.position;
            pos.x -= gameObject.GetComponent<RectTransform>().rect.width / 2 - hpBarNumObjs[i].GetComponent<RectTransform>().rect.width / 2 - numBetWeen.x;
            pos.x += (hpBarNumObjs[i].GetComponent<RectTransform>().rect.width + numBetWeen.x) * i;
            pos.y -= gameObject.GetComponent<RectTransform>().rect.height / 2 + hpBarNumObjs[i].GetComponent<RectTransform>().rect.height / 2 + numBetWeen.y;
            hpBarNumObjs[i].transform.position = pos;
        }
    }
}
