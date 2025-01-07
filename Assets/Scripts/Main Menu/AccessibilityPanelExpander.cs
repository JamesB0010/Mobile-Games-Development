using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AccessibilityPanelExpander : MonoBehaviour
{
    [SerializeField] private Transform accessibilityPanel;

    [SerializeField] private float expansionTime;
    
    public void ExpandPanel()
    {
        this.accessibilityPanel.localScale.LerpTo(new Vector3(1,1,1), this.expansionTime, val => this.accessibilityPanel.localScale = val, null, GlobalLerpProcessor.easeInOutCurve);
    }
}
