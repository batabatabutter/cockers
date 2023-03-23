using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Ordeal
{
    [SerializeField, Label("���")] EnemyID enemySpawnID;
    public EnemyID GetEnemyID() { return enemySpawnID; }         //  �o����ގ�n
}


public class Hunt : MonoBehaviour
{
    bool EnterFlag = false;
    [SerializeField, Label("�G�̐����f�[�^")] List<Ordeal> ordeals;

    bool IsSpawn = false;

    public List<Ordeal> GetOrdeals()
    {
        return ordeals;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        IsSpawn = EnterFlag;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            EnterFlag = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.name == "Player")
        {
            EnterFlag = false;
        }
    }

    public bool GetIsSpawn()
    {
        return IsSpawn;
    }

    public void SetIsSpawn(bool isSpawn)
    {
        this.IsSpawn = isSpawn;
    }
}
