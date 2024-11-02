using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsSetupHelper : MonoBehaviour
{
    [SerializeField] private UpgradeCell[] cells;

    public void SetupCells(ShipPartLabel label)
    {
        int smallerMax;

        int numberOfUpgrades = label.Upgrades.GetShipUpgrades().Length;
        bool moreUpgradesThenCells = this.cells.Length < numberOfUpgrades;
        if (moreUpgradesThenCells)
        {
            smallerMax = this.cells.Length;
        }
        else
        {
            smallerMax = numberOfUpgrades;
        }
        
        for (int i = 0; i < smallerMax; i++)
        {
            this.cells[i].Upgrade = label.Upgrades.GetShipUpgrades()[i];
            this.cells[i].WeaponIndex = label.WeaponIndex;
            this.cells[i].ShipSection = label.ShipSection;
        }
    }
}
