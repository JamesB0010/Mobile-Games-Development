using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object Value References/Int")]
public class IntReference : SuperBaseScriptableValRef<int>
{
    private int value;

    public event Action<int> valueChanged;

    public override int GetValue()
    {
        return value;
    }

    public void SetValue(int val)
    {
        if (val == this.value)
            return;
        
        this.value = val;
        this.valueChanged?.Invoke(this.value);
    }

    public static IntReference operator +(IntReference to, int val)
    {
        to.SetValue(to.GetValue() + val);
        return to;
    }
}