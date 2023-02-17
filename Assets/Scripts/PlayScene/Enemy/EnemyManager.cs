using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  �G�̖��O
public enum EnemyID
{
    [InspectorName("�L���x�c���Y")]    CabbageTaro,
    [InspectorName("�j���W����")]      CarrotSpear,
    [InspectorName("�ؕ��m")]          PigSoldier,

    [InspectorName("")]                EnemyNum
}

public class EnemyManager : MonoBehaviour
{
    //  �G�̃I�u�W�F�N�g���i�[���Ă���
    [SerializeField] List<GameObject> Enemys;

    //  �z�񂩉��i�[�p
    GameObject[] HolderArray;
    //  �G�̐������i�[
    public List<EnemySpawn> enemySpawns;
    //  �G���i�[
    public List<GameObject> enemyObjects;

    //  �v���C���[���i�[���Ă���
    GameObject player;

    //  �ŏ��Ɏ��s�����
    private void Start()
    {
        //  �G�̃X�|�[�����i�[
        HolderArray = GameObject.FindGameObjectsWithTag("EnemySpawn");
        Debug.Log(HolderArray);
        //  List�ɍĊi�[
        foreach (GameObject obj in HolderArray)
        {
            if (obj.GetComponent<EnemySpawn>() == null) Debug.Log("�N���XEnemySpaen��������܂���ł����B" + obj);
            enemySpawns.Add(obj.GetComponent<EnemySpawn>());
        }

        //  �v���C���[���i�[���Ă���
        player = GameObject.FindWithTag("Player");
    }

    //  �X�V
    private void Update()
    {
        //  �G�̐����X�V
        foreach (EnemySpawn enemy in enemySpawns)
        {
            //  ��������Ă��Ȃ����
            if (!enemy.GetIsSpawn())
            {
                //  ����
                enemyObjects.Add(Instantiate(Enemys[(int)enemy.GetEnemyID()], enemy.GetEnemyDefPos(), Quaternion.identity));
                enemy.SetIsSpawn(true);
            }
        }

        //  �G�̍X�V
        foreach(GameObject enemy in enemyObjects)
        {

        }
    }
}
