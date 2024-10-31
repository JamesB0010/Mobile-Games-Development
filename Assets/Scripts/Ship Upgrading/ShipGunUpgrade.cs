using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class ShipGunUpgrade : ScriptableObject
{
    public abstract object GetGun();

    [SerializeField] private float cost;
    public float Cost => this.cost;

    [SerializeField] private Sprite icon;
    public Sprite Icon => this.icon;

    [SerializeField] private bool isPurchaseable;
    public bool IsPurchaseable => this.isPurchaseable;

    public bool OwnedByDefault;

}
