using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public abstract class ComparisonOperator : ScriptableObject
{
    public virtual bool Test<T>(IComparable<T> source, T target)
    {
        return false;
    }

    public virtual bool Test(bool source, bool target)
    {
        return false;
    }
}
