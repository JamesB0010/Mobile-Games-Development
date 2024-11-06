using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Item Upgrades/Armour")]
public class ArmourUpgrade : ShipItemUpgrade
{
    [SerializeField] private Armour armour;

    public Armour Armour => this.armour;

    public override object GetUpgrade()
    {
        return armour;
    }

    public override UpdatePrevOwnedItemStrategy GenerateUpdatePrevOwnedStrategy(ShipPartLabel label, EquipItem itemEquipAction)
    {
        return new UpdatePrevOwnedArmor(label, itemEquipAction);
    }
}
