using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class ComparisonOperator : ScriptableObject
{
    public virtual bool Test(float source, float target)
    {
        return false;
    }

    public virtual bool Test(bool source, bool target)
    {
        return false;
    }
}
