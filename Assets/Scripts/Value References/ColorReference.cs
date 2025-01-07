using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Object Value References/Color")]
public class ColorReference : SuperBaseScriptableValRef<Color>
{
    private Color value;

    public event Action<Color> valueChanged;
    public override Color GetValue()
    {
        return value;
    }

    public void SetValue(Color val)
    {
        if (val == this.value)
            return;

        this.value = val;
        this.valueChanged?.Invoke(this.value);
    }
}
