using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Enemy : MonoBehaviour
{
    //  �X�e�[�^�X�i�[�p
    protected EnemyStatas statas;
    [SerializeField, HeaderAttribute("�X�e�[�^�X")] int hp;
    [SerializeField] int atk;

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
        //  �̗͂�0�Ȃ�Death!�I�I
        if (statas.HP <= 0)
        {
            statas.death = true;
            Destroy(gameObject, 1.0f);
        }

        //  ����ł���Ȃ�߂�
        if (statas.death) return;

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

    //  ����ł���m�F
    public bool GetIsDath()
    {
        return statas.death;
    }
}
