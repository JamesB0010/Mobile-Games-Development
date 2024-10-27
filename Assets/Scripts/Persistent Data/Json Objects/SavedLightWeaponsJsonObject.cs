using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct SavedLightWeaponsJsonObject
{
    public List<string> lightGunReferences;
    public SavedLightWeaponsJsonObject(List<ShipGunUpgrade> guns)
    {
        this.lightGunReferences = new List<string>();

        foreach (var gun in guns)
        {
            this.lightGunReferences.Add(gun.name);
        }
    }


    public List<ShipGunUpgrade> GetSavedGuns()
    {
        List<ShipGunUpgrade> upgrades = new List<ShipGunUpgrade>();


        for (int i = 0; i < this.lightGunReferences.Count; i++)
        {
            object weaponUpgrade = Resources.Load("ShipUpgrades/" + this.lightGunReferences[i]);

            upgrades.Add((ShipGunUpgrade)weaponUpgrade);
        }

        return upgrades;
    }
}
