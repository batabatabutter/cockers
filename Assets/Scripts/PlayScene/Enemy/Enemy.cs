using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

///////////////////////
/// �G�̌p�����N���X///
///////////////////////

public class Enemy : MonoBehaviour
{
    //  �X�e�[�^�X�i�[�p
    protected EnemyStatas statas;

    //  �G�̎�ނ�ݒ�
    [SerializeField, HeaderAttribute("�G���")] EnemyID enemyID;

    [SerializeField, HeaderAttribute("�X�e�[�^�X")] int hp;
    [SerializeField] int atk;

    //  �h���b�v�A�C�e��
    [SerializeField] List<GameObject> dropItem;

    // Start is called before the first frame update
    void Start()
    {
        //  �X�e�[�^�X�i�[
        statas.HP = hp;
        statas.ATK = atk;
        statas.death = false;
        EnemyStart();
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

        //  �G�X�V
        EnemyUpdate();

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

        // A�L�[�������ꂽ�u�Ԃ��ǂ���
        if (aKey.wasPressedThisFrame)
        {
            statas.HP -= 5;
            Debug.Log(statas.HP);
        }
    }

    //  �G�̏����ݒ�
    public virtual void EnemyStart()
    {

    }

    //  �G�̍X�V
    public virtual void EnemyUpdate()
    {

    }

    //  �_���[�W����
    public void Damage(int dmg)
    {
        statas.HP -= dmg;
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
        foreach(GameObject obj in dropItem)
        {
            Instantiate(obj, gameObject.transform.position, Quaternion.identity);
        }
    }
}
