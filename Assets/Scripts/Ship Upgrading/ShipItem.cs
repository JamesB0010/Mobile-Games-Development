using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipItem : ScriptableObject, ICloneable
{
    public abstract object Clone();
}
