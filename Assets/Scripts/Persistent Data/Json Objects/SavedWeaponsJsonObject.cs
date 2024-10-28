using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public struct SavedWeaponsJsonObject
{
    public List<string> gunReferences;
    public SavedWeaponsJsonObject(List<ShipGunUpgrade> guns)
    {
        this.gunReferences = new List<string>();

        foreach (var gun in guns)
        {
            this.gunReferences.Add(gun.name);
        }
    }


    public List<ShipGunUpgrade> GetSavedGuns()
    {
        List<ShipGunUpgrade> upgrades = new List<ShipGunUpgrade>();


        for (int i = 0; i < this.gunReferences.Count; i++)
        {
            object weaponUpgrade = Resources.Load("ShipUpgrades/" + this.gunReferences[i]);

            upgrades.Add((ShipGunUpgrade)weaponUpgrade);
        }

        return upgrades;
    }
}
