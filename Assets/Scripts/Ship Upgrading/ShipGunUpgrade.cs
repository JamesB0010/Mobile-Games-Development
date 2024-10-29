using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu]
public class ShipGunUpgrade : ScriptableObject
{
    [FormerlySerializedAs("gun")][SerializeField] private LightGun lightGun;
    public LightGun LightGun => this.lightGun;

    [SerializeField] private float cost;
    public float Cost => this.cost;

    [SerializeField] private Sprite icon;
    public Sprite Icon => this.icon;

    [SerializeField] private bool isPurchaseable;
    public bool IsPurchaseable => this.isPurchaseable;

    public bool OwnedByDefault;

}