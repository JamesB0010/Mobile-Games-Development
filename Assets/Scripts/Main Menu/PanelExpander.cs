using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PanelExpander : MonoBehaviour
{
    [SerializeField] private Transform panel;

    [SerializeField] private float expansionTime;
    
    public void ExpandPanel()
    {
        this.panel.localScale.LerpTo(new Vector3(1,1,1), this.expansionTime, val => this.panel.localScale = val, null, GlobalLerpProcessor.easeInOutCurve);
    }
}
