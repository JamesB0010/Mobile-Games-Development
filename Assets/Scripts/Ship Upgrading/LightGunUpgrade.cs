using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Item Upgrades/Light Ship Gun")]
public class LightGunUpgrade : ShipItemUpgrade
{
    [SerializeField] private LightGun gun;
    public LightGun Gun => this.gun;

    public override object GetUpgrade()
    {
        return this.gun;
    }

    public override UpdatePrevOwnedItemStrategy GenerateUpdatePrevOwnedStrategy(ShipPartLabel label, EquipItem itemEquipAction)
    {
        return new UpdatePrevOwnedLight(label, itemEquipAction);
    }
}
