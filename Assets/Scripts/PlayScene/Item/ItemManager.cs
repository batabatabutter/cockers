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

public class ItemManager : MonoBehaviour
{
    //  �����A�C�e����
    public List<int> itemNum;

    //  �G�̃I�u�W�F�N�g���i�[���Ă���
    [SerializeField] List<GameObject> Items;

    //  �G�Ƃǂꂭ�炢����Ă����瓮����~�߂邩���f
    [SerializeField] float distance;

    //  �z�񂩉��i�[�p
    GameObject[] HolderArray;
    //  �G�̐������i�[
    public List<ItemSpawn> itemSpawns;
    //  �G���i�[
    public List<GameObject> itemObjects;

    //  �v���C���[���i�[���Ă���
    GameObject player;

    // Start is called before the first frame update
    private void Start()
    {
        //  �A�C�e�����i�[
        for (int i = 0; i < (int)ItemID.ItemNum; i++)
        {
            itemNum.Add(0);
        }

        //  �A�C�e���̃X�|�[�����i�[
        HolderArray = GameObject.FindGameObjectsWithTag("ItemSpawn");
        //  List�ɍĊi�[
        foreach (GameObject obj in HolderArray)
        {
            if (obj.GetComponent<ItemSpawn>() == null) Debug.Log("Error : �N���X ItemSpaen ��������܂���ł����B" + obj);
            itemSpawns.Add(obj.GetComponent<ItemSpawn>());
        }

        //  �v���C���[���i�[���Ă���
        player = GameObject.FindWithTag("Player");
        if (player == null) Debug.Log("Error : �v���C���[���݂���܂���");
    }

    // Update is called once per frame
    private void Update()
    {
        //  �G�̐����X�V
        foreach (ItemSpawn item in itemSpawns)
        {
            //  ��������Ă��Ȃ����
            if (!item.GetIsSpawn())
            {
                //  ����
                itemObjects.Add(Instantiate(Items[(int)item.GetItemID()], item.GetItemDefPos(), Quaternion.identity));
                item.SetIsSpawn(true);
            }
        }

        //  �G�̍X�V
        foreach (GameObject item in itemObjects)
        {
            //  �Ȃ�������X�L�b�v
            if (item == null) continue;

            //  �G�ƃv���C���[�̈ʒu�֌W��r
            if (Vector3.Distance(item.transform.position, player.transform.position) > distance)
            {
                //  ������������s��~
                item.SetActive(false);
            }
            else
            {
                //  �߂Â�������s
                item.SetActive(true);
            }
        }
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

    //  �A�C�e���擾����n
    public int GetItemNum(ItemID ID)
    {
        return itemNum[(int)ID];
    }
}
