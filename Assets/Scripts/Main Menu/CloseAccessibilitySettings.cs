using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseAccessibilitySettings : MonoBehaviour
{
    [SerializeField] private Transform panel;

    [SerializeField] private float transitionTime;

    public void ClosePanel()
    {
        panel.transform.localScale.LerpTo(Vector3.zero, this.transitionTime, val => this.panel.localScale = val, null, GlobalLerpProcessor.easeInOutCurve);
    }
}
