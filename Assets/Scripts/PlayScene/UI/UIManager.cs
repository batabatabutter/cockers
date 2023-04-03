using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField, Header("岩ギミックUI"), Label("壊れた")] GameObject breakUI; public GameObject GetBreakUI() { return breakUI; }
    [SerializeField, Label("壊れなかった")] GameObject noBreakUI; public GameObject GetNoBreakUI() { return noBreakUI; }
}
