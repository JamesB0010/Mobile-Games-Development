using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class NotEqualComparisonOperator : ComparisonOperator
{
    public override bool Test(bool source, bool target)
    {
        return source != target;
    }
    public override bool Test(float source, float target)
    {
        return source != target;
    }

}
