using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class EqualsComparisonOperator : ComparisonOperator
{
    public override bool Test(bool toTest, bool requirement)
    {
        return toTest == requirement;
    }

    public override bool Test(float source, float target)
    {
        return source == target;
    }
}
