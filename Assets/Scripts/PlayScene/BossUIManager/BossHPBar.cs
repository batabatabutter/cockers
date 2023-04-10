using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHPBar : MonoBehaviour
{
    [System.Serializable]
    private class HPBarColor
    {
        [Label("基本色")] public Color BarColor;
        [Label("消滅色")] public Color BarDecreaseColor;
    };

    [Header("HPバーの見た目")]
    [SerializeField, Label("HPバーOBJ")] GameObject hpBarObj;
    [SerializeField, Label("ゲージ色一覧")] List<HPBarColor> hpBarColors;
    [SerializeField, Label("ゲージ消費速度")] float decreaseHpSpeed;
    [SerializeField, Label("1ゲージのHP")] int oneGaugeHP;
    [Header("HPバー本数表示")]
    [SerializeField, Label("バー数表示OBJ")] GameObject hpBarNumObj;
    [SerializeField, Label("バー表示間隔")] Vector2 numBetWeen;

    //  HPゲージの数
    private int hpGaugeNum;
    
    //  HPバーオブジェクト
    private List<GameObject> hpBars;
    //  消滅HPバーオブジェクト
    private List<GameObject> decreaseHpBars;
    //  HPバー表示オブジェクト
    private List<GameObject> hpBarNumObjs;

    //  消滅HP数値
    private float decreaseHpBarVal;

    //  ボス
    private Boss boss;

    private void Awake()
    {
        decreaseHpBars = new List<GameObject>();
        hpBars = new List<GameObject>();
        hpBarNumObjs = new List<GameObject>();
    }

    //  更新
    void Update()
    {
        //  通常色
        for (int i = 0; i < hpGaugeNum; i++)
        {
            //  バーの位置調整
            decreaseHpBars[i].transform.position = this.transform.position;
            hpBars[i].transform.position = this.transform.position;

            //  消滅HPバー
            if (boss.GetNowBossHP() < decreaseHpBarVal)
                decreaseHpBarVal -= decreaseHpSpeed * Time.deltaTime;
            decreaseHpBarVal = Mathf.Max(decreaseHpBarVal, boss.GetNowBossHP());
            decreaseHpBarVal = Mathf.Max(decreaseHpBarVal, 0.0f);
            {
                //  まだ前のゲージがあるとき
                if (oneGaugeHP * (i + 1) < decreaseHpBarVal)
                    decreaseHpBars[i].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
                //  もう次のゲージに行っているとき
                else if (oneGaugeHP * i >= decreaseHpBarVal)
                    decreaseHpBars[i].transform.localScale = new Vector3(0.0f, 1.0f, 1.0f);

                else
                {
                    //  割合
                    float hpBarPer = (float)(decreaseHpBarVal - oneGaugeHP * (float)((int)decreaseHpBarVal / oneGaugeHP)) / (float)oneGaugeHP;
                    decreaseHpBars[i].transform.localScale = new Vector3(hpBarPer, 1.0f, 1.0f);
                    Vector3 pos = decreaseHpBars[i].transform.position;

                    //  移動
                    pos.x -= gameObject.GetComponent<RectTransform>().rect.width / 2 * (1 - hpBarPer);
                    decreaseHpBars[i].transform.position = pos;
                }
            }

            //  通常バー
            //  まだ前のゲージがあるとき
            if (oneGaugeHP * (i + 1) < boss.GetNowBossHP())
                hpBars[i].transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
            //  もう次のゲージに行っているとき
            else if (oneGaugeHP * i >= boss.GetNowBossHP())
                hpBars[i].transform.localScale = new Vector3(0.0f, 1.0f, 1.0f);
            //  今のゲージの割合計算
            else
            {
                //  割合
                float hpBarPer = (float)(boss.GetNowBossHP() - oneGaugeHP * i) / (float)oneGaugeHP;
                hpBars[i].transform.localScale = new Vector3(hpBarPer, 1.0f, 1.0f);
                Vector3 pos = hpBars[i].transform.position;

                //  移動
                pos.x -= gameObject.GetComponent<RectTransform>().rect.width / 2 * (1 - hpBarPer);
                hpBars[i].transform.position = pos;
            }

            //  バーの数表示
            //  もう次のゲージに行っているとき
            if (oneGaugeHP * i >= boss.GetNowBossHP())
                hpBarNumObjs[i].SetActive(false);
            else
                hpBarNumObjs[i].SetActive(true);

        }
    }

    //  設定
    public void SetBoss(GameObject boss)
    {
        //  リセット
        decreaseHpBars.Clear();
        hpBars.Clear();
        hpBarNumObjs.Clear();

        this.boss = boss.GetComponent<Boss>();

        //  値セット
        hpGaugeNum = this.boss.GetMaxBossHP() / oneGaugeHP + 1;
        decreaseHpBarVal = this.boss.GetMaxBossHP();

        //  HPバー
        for (int i = 0; i < hpGaugeNum; i++)
        {
            //  消滅バー作成
            GameObject bar = Instantiate(hpBarObj, gameObject.transform);
            decreaseHpBars.Add(bar);
            decreaseHpBars[i].GetComponent<UnityEngine.UI.RawImage>().color = hpBarColors[i].BarDecreaseColor;

            //  通常バー作成
            bar = Instantiate(hpBarObj, gameObject.transform);
            hpBars.Add(bar);
            hpBars[i].GetComponent<UnityEngine.UI.RawImage>().color = hpBarColors[i].BarColor;

            //  ゲージ表示追加
            bar = Instantiate(hpBarNumObj, gameObject.transform);
            hpBarNumObjs.Add(bar);
            hpBarNumObjs[i].GetComponent<UnityEngine.UI.RawImage>().color = hpBarColors[i].BarColor;
            //  位置調整
            Vector3 pos = hpBarNumObjs[i].transform.position;
            pos.x -= gameObject.GetComponent<RectTransform>().rect.width / 2 - hpBarNumObjs[i].GetComponent<RectTransform>().rect.width / 2 - numBetWeen.x;
            pos.x += (hpBarNumObjs[i].GetComponent<RectTransform>().rect.width + numBetWeen.x) * i;
            pos.y -= gameObject.GetComponent<RectTransform>().rect.height / 2 + hpBarNumObjs[i].GetComponent<RectTransform>().rect.height / 2 + numBetWeen.y;
            hpBarNumObjs[i].transform.position = pos;
        }
    }
}
