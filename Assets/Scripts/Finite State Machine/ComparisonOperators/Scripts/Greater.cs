using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Greater : ComparisonOperator
{
    public override bool Test(float source, float target)
    {
        return source > target;
    }

}
