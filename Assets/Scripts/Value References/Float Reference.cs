using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object Value References/Float")]
public class FloatReference : SuperBaseScriptableValRef<float>
{
    private float value;

    public event Action<float> valueChanged;

    public override float GetValue()
    {
        return value;
    }

    public void SetValue(float val)
    {
        if (val == this.value)
            return;
        
        this.value = val;
        this.valueChanged?.Invoke(this.value);
    }

    public static FloatReference operator +(FloatReference to, float val)
    {
        to.SetValue(to.GetValue() + val);
        return to;
    }
}