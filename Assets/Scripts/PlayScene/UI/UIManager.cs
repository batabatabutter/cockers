using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField, Header("��M�~�b�NUI"), Label("��ꂽ")] GameObject breakUI; public GameObject GetBreakUI() { return breakUI; }
    [SerializeField, Label("���Ȃ�����")] GameObject noBreakUI; public GameObject GetNoBreakUI() { return noBreakUI; }
}
