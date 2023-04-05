using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField, Header("��M�~�b�NUI"), Label("��ꂽ")] GameObject breakUI; public GameObject GetBreakUI() { return breakUI; }
    [SerializeField, Label("���Ȃ�����")] GameObject noBreakUI; public GameObject GetNoBreakUI() { return noBreakUI; }

    [SerializeField, Header("�����̎��"), Label("��V")] GameObject rewardUI; public GameObject GetRewardUI() { return rewardUI; }

    [SerializeField, Label("����")] GameObject successUI; public GameObject GetSuccessUI() { return successUI; }
}
