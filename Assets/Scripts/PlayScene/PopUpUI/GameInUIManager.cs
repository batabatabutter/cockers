using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PopUpUIID
{
    [InspectorName("�t�B�[���h�؂�ւ�")] FieldChanger,

    [InspectorName("")] PopUpUINum
}

public class GameInUIManager : MonoBehaviour
{
    private List<GameObject> fieldChanger;
    private List<GameObject> fieldChangerUI;
    [SerializeField, Label("�\������")] float distance;
    [SerializeField, Label("�g�k���x")] float ScalingSpeed;
    [SerializeField, Label("�t�B�[���h�؂�ւ��M�~�b�NUI")] GameObject fieldChangerUIPrefab;

    PlayManager playManager;

    //  ������
    public void Initialize()
    {
        playManager = GameObject.FindGameObjectWithTag("PlayManager").GetComponent<PlayManager>();
        fieldChanger = new List<GameObject>();
        fieldChangerUI = new List<GameObject>();
    }

    //  ���Z�b�g
    public void Reset()
    {
        //  ������
        foreach (GameObject UI in fieldChangerUI)
        {
            Destroy(UI);
            UI.SetActive(false);
        }
        fieldChanger.Clear();
        fieldChangerUI.Clear();

        //  �f�X�g���C�����z�͏���
        GameObject[] Array = GameObject.FindGameObjectsWithTag("PopUpUIAdd");

        //  �ǉ�����
        foreach (GameObject array in Array)
        {
            GameObject ui = Instantiate(fieldChangerUIPrefab);
            ui.transform.SetParent(this.transform);
            fieldChanger.Add(array);
            fieldChangerUI.Add(ui);
        }

        //  �ǉ�������������
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

    //  �X�V
    public void Update()
    {
        for (int i = 0; i < fieldChangerUI.Count; i++)
        {
            //  ���W��Ǐ]������
            //fieldChangerUI[i].transform.position = RectTransformUtility.WorldToScreenPoint(playManager.GetPlayerCam().GetComponent<Camera>(), fieldChanger[i].transform.position);
            fieldChangerUI[i].transform.position = RectTransformUtility.WorldToScreenPoint(Camera.main, fieldChanger[i].transform.position);

            //  �������߂��Ɗg��
            float scale = fieldChangerUI[i].transform.localScale.x;
            if (Vector3.Distance(fieldChanger[i].transform.position, playManager.GetPlayer().transform.position) < distance)
            {
                scale += ScalingSpeed * Time.deltaTime;
            }
            //  �����������Ək��
            else
            {
                scale -= ScalingSpeed * Time.deltaTime;
            }
            //  �����C��
            scale = Mathf.Clamp(scale, 0.0f, 1.0f);
            fieldChangerUI[i].transform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
