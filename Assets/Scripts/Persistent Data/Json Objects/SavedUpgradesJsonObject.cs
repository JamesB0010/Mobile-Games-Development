using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SavedUpgradesJsonObject
{
    public List<string> upgradeReferences;

    // Constructor to create from ShipItemUpgrade list
    public SavedUpgradesJsonObject(List<ShipItemUpgrade> items)
    {
        this.upgradeReferences = new List<string>();

        foreach (var item in items)
        {
            this.upgradeReferences.Add(item.name);  // Store only the name of the upgrade
        }
    }

    // Constructor for a single ShipItemUpgrade
    public SavedUpgradesJsonObject(ShipItemUpgrade item)
    {
        this.upgradeReferences = new List<string> { item.name };  // Store the name
    }

    // Convert the upgrade references back to actual ShipItemUpgrade objects
    public List<ShipItemUpgrade> GetSavedUpgrades()
    {
        
        List<ShipItemUpgrade> upgrades = new List<ShipItemUpgrade>();

        for (int i = 0; i < this.upgradeReferences.Count; i++)
        {
            var weaponUpgrade = Resources.Load<ShipItemUpgrade>("ShipUpgrades/" + this.upgradeReferences[i]);
            if (weaponUpgrade != null)
            {
                upgrades.Add(weaponUpgrade);
            }
            else
            {
                Debug.LogError("Upgrade not found: " + this.upgradeReferences[i]);
            }
        }

        return upgrades;
    }
}