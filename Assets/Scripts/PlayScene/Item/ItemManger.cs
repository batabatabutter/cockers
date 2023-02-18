using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  �A�C�e���̖��O
public enum ItemID
{
    [InspectorName("�L���x�c")] Cabbage,
    [InspectorName("�j���W��")] Carrot,
    [InspectorName("�ؓ�")] Pork,

    [InspectorName("")] ItemNum
}

public class ItemManger : MonoBehaviour
{
    //  �����A�C�e����
    public List<int> itemNum;

    // Start is called before the first frame update
    private void Start()
    {
        for (int i = 0; i < (int)ItemID.ItemNum; i++)
        {
            itemNum.Add(0);
        }
    }

    // Update is called once per frame
    private void Update()
    {
        
    }

    //  �A�C�e���擾����
    public void GetItem(ItemID ID, int num)
    {
        itemNum[(int)ID] += num;
    }

    //  �A�C�e�������
    public bool SpendItem(ItemID ID, int num)
    {
        //  ��������Ȃ������玸�s
        if (itemNum[(int)ID] - num < 0) return false;
        itemNum[(int)ID] -= num;
        return true;
    }
}
