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

//  �{�X�̖��O
public enum BossID
{
    [InspectorName("�≖�}��")] SaltRockMan,

    [InspectorName("")] BossNum
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

    //  �G�̃I�u�W�F�N�g���i�[���Ă���
    [SerializeField] List<GameObject> Bosses;

    //  �G�Ƃǂꂭ�炢����Ă����瓮����~�߂邩���f
    [SerializeField] float distance;

    //  �G�̖��G���Ԑݒ�
    [SerializeField, HeaderAttribute("�ꗥ�X�e�[�^�X")] float invincibilityTime;
    //  �d�͐ݒ�
    [SerializeField] float gravity;

    //  �z�񂩉��i�[�p
    GameObject[] HolderArray;
    //  �G�̐������i�[
    public List<EnemySpawn> enemySpawns;
    //  �G���i�[
    public List<GameObject> enemyObjects;

    //  �G�̐������i�[
    public List<BossSpawn> bossSpawns;
    //  �G���i�[
    public List<GameObject> bossObjects;

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

        //  �{�X�̃X�|�[�����i�[
        HolderArray = GameObject.FindGameObjectsWithTag("BossSpawn");
        //  List�ɍĊi�[
        foreach (GameObject obj in HolderArray)
        {
            if (obj.GetComponent<BossSpawn>() == null) Debug.Log("Error : �N���X BossSpaen ��������܂���ł����B" + obj);
            bossSpawns.Add(obj.GetComponent<BossSpawn>());
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

        //  �{�X�̐����X�V
        foreach (BossSpawn boss in bossSpawns)
        {
            //  ��������Ă��Ȃ����
            if (!boss.GetIsSpawn())
            {
                //  ����
                bossObjects.Add(Instantiate(Bosses[(int)boss.GetBossID()], boss.GetEnemyDefPos(), Quaternion.identity));
                boss.SetIsSpawn(true);
            }
        }

        //  �G�̍X�V
        foreach (GameObject enemy in enemyObjects)
        {
            //  �Ȃ�������X�L�b�v
            if (enemy == null) continue;

            //  �G�ƃv���C���[�̈ʒu�֌W��r
            if (Vector3.Distance(enemy.transform.position, player.transform.position) > distance)
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

        //  �{�X�̍X�V
        foreach (GameObject boss in bossObjects)
        {
            //  �Ȃ�������X�L�b�v
            if (boss == null) continue;

            //  �G�ƃv���C���[�̈ʒu�֌W��r
            if (Vector3.Distance(boss.transform.position, player.transform.position) > distance)
            {
                //  ������������s��~
                boss.SetActive(false);
            }
            else
            {
                //  �߂Â�������s
                boss.SetActive(true);
            }
        }
    }

    //  ���G���Ԏ擾
    public float GetInvincibilityTime()
    {
        return invincibilityTime;
    }

    //  �d�͎擾
    public float GetGravity()
    {
        return gravity;
    }

    //  �v���C���[�擾
    public GameObject GetPlayer()
    {
        return player;
    }
}
