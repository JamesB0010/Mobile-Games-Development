using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object Value References/Boolean")]
public class BoolReference : SuperBaseScriptableValRef<bool>
{
    private bool value;

    public event Action<bool> valueChanged;

    public override bool GetValue()
    {
        return value;
    }

    public void SetValue(bool val)
    {
        if (val == this.value)
            return;
        
        this.value = val;
        this.valueChanged?.Invoke(this.value);
    }
}