using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = System.Object;

public abstract class SuperBaseScriptableValRef<T> : SuperBaseScriptableValRef
{
    public abstract T GetValue();

    public override object GetVal() => this.GetValue();

}
