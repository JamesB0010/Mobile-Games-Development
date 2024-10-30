using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Greater : ComparisonOperator
{
    public override bool Test<T>(IComparable<T> source, T target)
    {
        return source.CompareTo(target) > 0;
    }

}
