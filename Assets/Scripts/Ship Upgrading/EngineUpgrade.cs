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

    public override EquipItemInteractorStrategy GenerateEquipItemInteractor(ShipPartLabel label, EquipItem itemEquipAction)
    {
        return new EquipEngineInteractorStrategy(label, itemEquipAction);
    }

    public override UpgradesCounterInteractorStrategy GenerateUpgradeCounterInteractor(OwnedUpgradesCounter upgradesCounter)
    {
        return new UpgradeCounterInteractorEngine(upgradesCounter);
    }
}
