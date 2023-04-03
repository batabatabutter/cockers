using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField, Header("ŠâƒMƒ~ƒbƒNUI"), Label("‰ó‚ê‚½")] GameObject breakUI; public GameObject GetBreakUI() { return breakUI; }
    [SerializeField, Label("‰ó‚ê‚È‚©‚Á‚½")] GameObject noBreakUI; public GameObject GetNoBreakUI() { return noBreakUI; }
}
