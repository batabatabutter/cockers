using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PopUpUIID
{
    [InspectorName("フィールド切り替え")] FieldChanger,

    [InspectorName("")] PopUpUINum
}

public class GameInUIManager : MonoBehaviour
{
    private List<GameObject> fieldChanger;
    private List<GameObject> fieldChangerUI;
    [SerializeField, Label("表示距離")] float distance;
    [SerializeField, Label("拡縮速度")] float ScalingSpeed;
    [SerializeField, Label("フィールド切り替えギミックUI")] GameObject fieldChangerUIPrefab;

    PlayManager playManager;

    //  初期化
    public void Initialize()
    {
        playManager = GameObject.FindGameObjectWithTag("PlayManager").GetComponent<PlayManager>();
        fieldChanger = new List<GameObject>();
        fieldChangerUI = new List<GameObject>();
    }

    //  リセット
    public void Reset()
    {
        //  初期化
        foreach (GameObject UI in fieldChangerUI)
        {
            Destroy(UI);
            UI.SetActive(false);
        }
        fieldChanger.Clear();
        fieldChangerUI.Clear();

        //  デストロイされる奴は消す
        GameObject[] Array = GameObject.FindGameObjectsWithTag("PopUpUIAdd");

        //  追加処理
        foreach (GameObject array in Array)
        {
            GameObject ui = Instantiate(fieldChangerUIPrefab);
            ui.transform.SetParent(this.transform);
            fieldChanger.Add(array);
            fieldChangerUI.Add(ui);
        }

        //  追加物初期化処理
        for (int i = 0; i < fieldChangerUI.Count; i++)
        {
            switch(fieldChanger[i].GetComponent<PopUpUIAdd>().GetPopUpUIID())
            {
                case PopUpUIID.FieldChanger:
                    fieldChangerUI[i].transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().text = fieldChanger[i].GetComponent<PopUpTextUIAdd>().GetPopUpText();
                    //fieldChangerUI[i].transform.GetChild(0).gameObject.GetComponent<UnityEngine.UI.Text>().color = fieldChanger[i].GetComponent<PopUpTextUIAdd>().GetPopUpColor();
                    //fieldChangerUI[i].GetComponent<UnityEngine.UI.RawImage>().color = fieldChanger[i].GetComponent<PopUpTextUIAdd>().GetPopUpBackColor();
                    break;
            }
        }
    }

    //  更新
    public void Update()
    {
        for (int i = 0; i < fieldChangerUI.Count; i++)
        {
            //  座標を追従させる
            //fieldChangerUI[i].transform.position = RectTransformUtility.WorldToScreenPoint(playManager.GetPlayerCam().GetComponent<Camera>(), fieldChanger[i].transform.position);
            fieldChangerUI[i].transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, fieldChanger[i].transform.position);

            //  距離が近いと拡大
            float scale = fieldChangerUI[i].transform.localScale.x;
            if (Vector3.Distance(fieldChanger[i].transform.position, playManager.GetPlayer().transform.position) < distance)
            {
                scale += ScalingSpeed * Time.deltaTime;
            }
            //  距離が遠いと縮小
            else
            {
                scale -= ScalingSpeed * Time.deltaTime;
            }
            //  距離修正
            scale = Mathf.Clamp(scale, 0.0f, 1.0f);
            fieldChangerUI[i].transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
