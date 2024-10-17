using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Less : ComparisonOperator
{
    public override bool Test(float source, float target)
    {
        return source < target;
    }
}
