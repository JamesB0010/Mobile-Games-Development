using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NotEqualComparisonOperator : ComparisonOperator
{
    public override bool Test(bool source, bool target) 
    {
        return !source.Equals(target);
    }

    public override bool Test<T>(IComparable<T> source, T target)
    {
        return source.CompareTo(target) != 0;
    }

}
