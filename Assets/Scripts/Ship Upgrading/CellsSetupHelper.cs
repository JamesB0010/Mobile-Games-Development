using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsSetupHelper : MonoBehaviour
{
    [SerializeField] private UpgradeCell[] cells;

    public void SetupCells(ShipPartLabel label)
    {
        for (int i = 0; i < this.cells.Length; i++)
        {
            this.cells[i].Upgrade = label.Upgrades.GetShipUpgrades()[i];
            this.cells[i].WeaponIndex = label.WeaponIndex;
            this.cells[i].ShipSection = label.ShipSection;
        }
    }
}
