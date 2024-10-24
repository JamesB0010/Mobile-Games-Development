using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public abstract class ScriptableObjectValueReference : ScriptableObject
{
    public abstract object GetValue();

    public abstract void SetValue(object val);
}
