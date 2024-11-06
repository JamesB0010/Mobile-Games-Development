using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ship Item Upgrades/Engine")]
public class EngineUpgrade : ShipItemUpgrade
{
    [SerializeField] private Engine engine;
    public Engine Engine => this.engine;
    public override object GetUpgrade()
    {
        return engine;
    }

    public override UpdatePrevOwnedItemStrategy GenerateUpdatePrevOwnedStrategy(ShipPartLabel label, EquipItem itemEquipAction)
    {
        return new UpdatePrevOwnedEngine(label, itemEquipAction);
    }
}
