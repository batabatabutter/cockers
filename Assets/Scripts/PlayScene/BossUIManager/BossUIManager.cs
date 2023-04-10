using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossUIManager : MonoBehaviour
{
    //  HP
    [SerializeField, Label("ボスHPゲージ")] GameObject bossHPBar;
    [SerializeField, Label("ボスHP位置Y")] Vector2 barPos;
    [SerializeField] private List<GameObject> bossHPBars;

    //  UI生成
    public void GenerateBossUI(GameObject boss)
    {
        //  HPバー
        GameObject hpBar = Instantiate(bossHPBar, new Vector3(barPos.x, barPos.y, 0.0f), Quaternion.identity);
        hpBar.transform.SetParent(this.transform);
        hpBar.GetComponent<BossHPBar>().SetBoss(boss);
        bossHPBars.Add(hpBar);

        //  名称
        
    }
}
