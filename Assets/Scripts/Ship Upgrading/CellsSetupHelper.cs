using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellsSetupHelper : MonoBehaviour
{
    [SerializeField] private UpgradeCell[] cells;

    private void Start()
    {
        this.DisableCells();
    }

    public void DisableCells()
    {
        StartCoroutine(nameof(DisableCellsAfterSecond));
    }

    private IEnumerator DisableCellsAfterSecond()
    {
        yield return new WaitForSeconds(1);
        foreach (UpgradeCell cell in this.cells)
        {
            cell.gameObject.SetActive(false);
        }
    }

    public void SetupCells(ShipPartLabel label)
    {
        StopCoroutine(nameof(this.DisableCellsAfterSecond));
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
            this.cells[i].gameObject.SetActive(true);
        }
    }
}
