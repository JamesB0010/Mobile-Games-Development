using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public struct SavedUpgradesJsonObject
{
    public List<string> upgradeReferences;
    public SavedUpgradesJsonObject(List<ShipItemUpgrade> items)
    {
        this.upgradeReferences = new List<string>();

        foreach (var item in items)
        {
            this.upgradeReferences.Add(item.name);
        }
    }

    public SavedUpgradesJsonObject(ShipItemUpgrade item)
    {
        this.upgradeReferences = new List<string>();
        this.upgradeReferences.Add(item.name);
    }


    public List<ShipItemUpgrade> GetSavedGuns()
    {
        List<ShipItemUpgrade> upgrades = new List<ShipItemUpgrade>();


        for (int i = 0; i < this.upgradeReferences.Count; i++)
        {
            object weaponUpgrade = Resources.Load("ShipUpgrades/" + this.upgradeReferences[i]);

            upgrades.Add((ShipItemUpgrade)weaponUpgrade);
        }

        return upgrades;
    }
}
