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

//  �G�̃X�e�[�^�X
public struct EnemyStatas
{
    public int HP;
    public int ATK;
    public bool death;
}

public class EnemyManager : MonoBehaviour
{
    //  �G�̃I�u�W�F�N�g���i�[���Ă���
    [SerializeField] List<GameObject> Enemys;

    //  �G�Ƃǂꂭ�炢����Ă����瓮����~�߂邩���f
    [SerializeField] float distance;

    //  �G�̖��G���Ԑݒ�
    [SerializeField, HeaderAttribute("�ꗥ�X�e�[�^�X")] float invincibilityTime;

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
        //  List�ɍĊi�[
        foreach (GameObject obj in HolderArray)
        {
            if (obj.GetComponent<EnemySpawn>() == null) Debug.Log("Error : �N���X EnemySpaen ��������܂���ł����B" + obj);
            enemySpawns.Add(obj.GetComponent<EnemySpawn>());
        }

        //  �v���C���[���i�[���Ă���
        player = GameObject.FindWithTag("Player");
        if (player == null) Debug.Log("Error : �v���C���[���݂���܂���");
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
            //  �Ȃ�������X�L�b�v
            if (enemy == null) continue;

            //  �G�ƃv���C���[�̈ʒu�֌W��r
            if(Vector3.Distance(enemy.transform.position,player.transform.position) > distance)
            {
                //  ������������s��~
                enemy.SetActive(false);
            }
            else
            {
                //  �߂Â�������s
                enemy.SetActive(true);
            }
        }
    }

    //  ���G���Ԏ擾
    public float GetInvincibilityTime()
    {
        return invincibilityTime;
    }
}
