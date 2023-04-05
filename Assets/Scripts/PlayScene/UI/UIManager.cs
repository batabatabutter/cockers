using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField, Header("ŠâƒMƒ~ƒbƒNUI"), Label("‰ó‚ê‚½")] GameObject breakUI; public GameObject GetBreakUI() { return breakUI; }
    [SerializeField, Label("‰ó‚ê‚È‚©‚Á‚½")] GameObject noBreakUI; public GameObject GetNoBreakUI() { return noBreakUI; }

    [SerializeField, Header("—û‚Ìëê"), Label("•ñV")] GameObject rewardUI; public GameObject GetRewardUI() { return rewardUI; }

    [SerializeField, Label("¬Œ÷")] GameObject successUI; public GameObject GetSuccessUI() { return successUI; }
}
