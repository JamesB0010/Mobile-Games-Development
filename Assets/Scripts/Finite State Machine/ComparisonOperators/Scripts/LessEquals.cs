using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LessEquals : ComparisonOperator
{
    public override bool Test(float source, float target)
    {
        return source <= target;
    }
}
