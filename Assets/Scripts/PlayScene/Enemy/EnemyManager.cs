using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//  �G�̖��O
public enum EnemyID
{
    [InspectorName("�L���x�c���Y")]       CabbageTaro,
    [InspectorName("�j���W����")]         CarrotSpear,
    [InspectorName("�ؕ��m")]             PigSoldier,
    [InspectorName("�ʍ�")]               Onionnu,
    [InspectorName("���イ��~�T�C��")]   CucumberMissile,
    [InspectorName("�A�b�v���c���[")]     AppleTree,
    [InspectorName("�T���f�B��")]         Saladhin,
    [InspectorName("�`�L�`�L�o�[")]       ChikiChiki,
    [InspectorName("�|�e�c")]             Potetu,
    [InspectorName("�X�g���[���x���[")]   Strawberry,
    [InspectorName("���S")]               CowDemon,
    [InspectorName("�r�[��")]             Beans,

    [InspectorName("")]                EnemyNum
}

//  �{�X�̖��O
public enum BossID
{
    [InspectorName("�≖�}��")] SaltRockMan,
    [InspectorName("�ݖ��a��")] SoySaucePrince,
    [InspectorName("�}�����[")] Mayonnaiser,
    [InspectorName("���X�m��")] MisoBucket,
    [InspectorName("�p�����}��")] SugerCubesMan,

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
    [SerializeField, Label("�G")] List<GameObject> Enemys;

    //  �G�̃I�u�W�F�N�g���i�[���Ă���
    [SerializeField, Label("�{�X")] List<GameObject> Bosses;

    //  �G�Ƃǂꂭ�炢����Ă����瓮����~�߂邩���f
    [SerializeField, Label("�`�拗��")] float distance;

    //  �G�̖��G���Ԑݒ�
    [SerializeField, HeaderAttribute("�ꗥ�X�e�[�^�X"), Label("���G����")] float invincibilityTime;
    //  �d�͐ݒ�
    [SerializeField, Label("�d��")] float gravity;

    //  �G�t�F�N�g���i�[
    [SerializeField, Label("�G���S�G�t�F�N�g")] GameObject enemyDeathEffect; public GameObject GetEnemyDeathEffect() { return enemyDeathEffect; }

    //  �{�X
    [SerializeField, Label("�{�X���S�G�t�F�N�g")] GameObject bossDeathEffect; public GameObject GetBossDeathEffect() { return bossDeathEffect; }

    //  �{�X�q�b�g�X�g�b�v
    [SerializeField, Label("�{�X���S�q�b�g�X�g�b�v�b��")] float hitStopSec;
    [SerializeField, Label("�{�X���S�q�b�g�X�g�b�v�{��")] float hitStopRate;
    private bool nowHitStop = false;
    private float hitStopTimer = 0.0f;

    //  �z�񂩉��i�[�p
    GameObject[] HolderArray;
    //  �G�̐������i�[
    [HideInInspector] public List<EnemySpawn> enemySpawns;
    //  �G���i�[
    [HideInInspector] public List<GameObject> enemyObjects;

    //  �G�̐������i�[
    [HideInInspector] public List<BossSpawn> bossSpawns;
    //  �G���i�[
    [HideInInspector] public List<GameObject> bossObjects;

    //  �v���C���[���i�[���Ă���
    GameObject player;

    //  �X�V
    private void Update()
    {
        //  �q�b�g�X�g�b�v�v�Z
        if (nowHitStop)
        {
            hitStopTimer -= Time.deltaTime / hitStopRate;
            if (hitStopTimer <= 0.0f)
            {
                nowHitStop = false;
                Time.timeScale = 1.0f;
            }
        }

        //  �G�̐����X�V
        foreach (EnemySpawn enemy in enemySpawns)
        {
            //  ��������Ă��Ȃ����
            if (!enemy.GetIsSpawn())
            {
                //  �����擾
                float rand = Random.Range(0.0f, 1.0f);

                float totalPer = 0.0f;
                int ID = 0;

                //  ����
                foreach (EnemySpawnStatas statas in enemy.GetEnemySpawnStatas())
                {
                    totalPer += statas.GetEnemySpaenPer();
                    if (rand <= totalPer)
                    {
                        GenerateEnemy(statas.GetEnemyID(), enemy.transform.position);
                        break;
                    }
                    ID++;
                }
                enemy.SetIsSpawn(true);
            }
        }

        //  �{�X�̐����X�V
        foreach (BossSpawn boss in bossSpawns)
        {
            //  ��������Ă��Ȃ����
            if (!boss.GetIsSpawn())
            {
                //  �����擾
                float rand = Random.Range(0.0f, 1.0f);

                float totalPer = 0.0f;
                int ID = 0;

                //  ����
                foreach (BossSpawnStatas statas in boss.GetBossSpawnStatas())
                {
                    totalPer += statas.GetBossSpaenPer();
                    if (rand <= totalPer)
                    {
                        GenerateBoss(statas.GetBossID(), boss.transform.position);
                        break;
                    }
                    ID++;
                }
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

    //  ���Z�b�g
    public void EnemyReset()
    {
        //  �ϐ����Z�b�g
        HolderArray = null;
        enemySpawns.Clear();
        bossSpawns.Clear();
        foreach (GameObject enemy in enemyObjects)
        {
            Destroy(enemy);
        }
        foreach (GameObject boss in bossObjects)
        {
            Destroy(boss);
        }
        enemyObjects.Clear();
        bossObjects.Clear();

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

    //  �G����
    public void GenerateEnemy(EnemyID ID, Vector3 pos)
    {
        GameObject enemy = Instantiate(Enemys[(int)ID], pos, Quaternion.identity);
        enemyObjects.Add(enemy);
    }

    //  �G�����Ԃ�l�t��
    public GameObject GenerateEnemyReturn(EnemyID ID, Vector3 pos)
    {
        GameObject enemy = Instantiate(Enemys[(int)ID], pos, Quaternion.identity);
        enemyObjects.Add(enemy);
        return enemy;
    }

    //  �{�X����
    public void GenerateBoss(BossID ID, Vector3 pos)
    {
        GameObject boss = Instantiate(Bosses[(int)ID], pos, Quaternion.identity);
        enemyObjects.Add(boss);
    }

    //  �q�b�g�X�g�b�v����
    public void HitStop()
    {
        nowHitStop = true;
        Time.timeScale = hitStopRate;
        hitStopTimer = hitStopSec;
    }
}
