using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

///////////////////////
/// �{�X�̌p�����N���X///
///////////////////////

public class Boss : MonoBehaviour
{
    //  �X�e�[�^�X�i�[�p
    protected EnemyStatas statas;

    //  ���G����
    protected float invincibilityTime;

    //  �v���C���[
    protected GameObject player;

    //  �����ړ�
    protected Vector3 vec;

    //  ��������
    protected Rigidbody rb;

    //  �U���t���O
    protected bool nowAttack;

    //  �G�̎�ނ�ݒ�
    [SerializeField, Label("�{�X���")] BossID bossID;

    [SerializeField, HeaderAttribute("�X�e�[�^�X")] int hp;
    [SerializeField] int atk;

    //  �h���b�v�A�C�e��
    [SerializeField] List<ItemID> dropItem;
    [SerializeField] List<int> dropItemNum;

    ItemManager itemManager;
    EnemyManager enemyManager;

    // Start is called before the first frame update
    void Start()
    {
        enemyManager = GameObject.FindGameObjectWithTag("EnemyManager").GetComponent<EnemyManager>();
        itemManager = GameObject.FindGameObjectWithTag("ItemManager").GetComponent<ItemManager>();
        player = GameObject.FindGameObjectWithTag("Player");
        nowAttack = false;
        rb = gameObject.GetComponent<Rigidbody>();
        //  �X�e�[�^�X�i�[
        statas.HP = hp;
        statas.ATK = atk;
        statas.death = false;
        vec = Vector3.zero;
        BossStart();
    }

    // Update is called once per frame
    void Update()
    {
        //  ����ł���Ȃ�߂�
        if (statas.death) return;

        //  �̗͂�0�Ȃ�Death!�I�I
        if (statas.HP <= 0)
        {
            Death();
        }

        //  ���G���ԍX�V
        invincibilityTime -= Time.deltaTime;
        if (invincibilityTime < 0.0f) invincibilityTime = 0.0f;

        //  ��������
        rb.MovePosition(rb.position + (Vector3.down * Time.deltaTime * enemyManager.GetGravity()));

        //  �G�X�V
        BossUpdate();

        ///////////////////////////////////////////////////////////////////////////

        //  �f�o�b�O�p
        // ���݂̃L�[�{�[�h���
        var current = Keyboard.current;

        // �L�[�{�[�h�ڑ��`�F�b�N
        if (current == null)
        {
            // �L�[�{�[�h���ڑ�����Ă��Ȃ���
            // Keyboard.current��null�ɂȂ�
            Debug.Log("�L�[�{�[�h�Ȃ�");
            return;
        }

        // A�L�[�̓��͏�Ԏ擾
        var aKey = current.aKey;
        var bKey = current.bKey;

        // A�L�[�������ꂽ�u�Ԃ��ǂ���
        if (aKey.wasPressedThisFrame)
        {
            Damage(5);
        }
        if(bKey.wasPressedThisFrame)
        {
            rb.velocity = Vector3.zero;
        }
    }

    //  �����蔻��
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && nowAttack)
        {
            collision.gameObject.GetComponent<PlayerStatus>().Damage(statas.ATK);
        }
        rb.velocity = Vector3.zero;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //rb.velocity = Vector3.zero;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        //rb.velocity = Vector3.zero;
    }

    //  �G�̏����ݒ�
    public virtual void BossStart()
    {

    }

    //  �G�̍X�V
    public virtual void BossUpdate()
    {

    }

    //  �_���[�W����
    public void Damage(int dmg)
    {
        if (invincibilityTime > 0.0f) return;
        statas.HP -= dmg;
        invincibilityTime = enemyManager.GetInvincibilityTime();
    }

    //  ����ł���m�F
    public bool GetIsDath()
    {
        return statas.death;
    }

    //  ���񂾏���
    public virtual void Death()
    {
        statas.death = true;
        Destroy(gameObject, 0.3f);
        
        //  �A�C�e������
        for (int i = 0; i < dropItem.Count; i++)
        {
            for (int j = 0; j < dropItemNum[i]; j++)
            {
                Instantiate(itemManager.GetItemObject(dropItem[i]), gameObject.transform.position, Quaternion.identity);
            }
        }
    }
}
