using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ShipItem : ScriptableObject, ICloneable
{
    [SerializeField] protected string itemName;

    public string ItemName => this.itemName;
    public abstract object Clone();
}
