using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUIManager : MonoBehaviour
{
    //  HP
    [SerializeField, Label("�{�XHP�Q�[�W")] GameObject bossHPBar;
    [SerializeField, Label("�{�XHP�ʒuY")] Vector2 barPos;
    [SerializeField] private List<GameObject> bossHPBars;

    //  UI����
    public void GenerateBossUI(GameObject boss)
    {
        //  HP�o�[
        GameObject hpBar = Instantiate(bossHPBar, new Vector3(barPos.x, barPos.y, 0.0f), Quaternion.identity);
        hpBar.transform.SetParent(this.transform);
        hpBar.GetComponent<BossHPBar>().SetBoss(boss);
        bossHPBars.Add(hpBar);

        //  ����
        
    }
}
