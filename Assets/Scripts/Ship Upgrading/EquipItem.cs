using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EquipItem : ItemStoreAction
{
    public event Action SelectedCellEquipped;
    
    public void EquipCell(UpgradeCell cell)
    {
        SaveToJson(cell);
    }

    protected override void SaveToJson(UpgradeCell cell)
    {
        switch (cell.ShipSection)
                {
                    case ShipSections.lightWeapons:
                        base.SaveLightWeaponAction(cell);
                        this.SelectedCellEquipped?.Invoke();
                        break;
                    case ShipSections.heavyWeapons:
                        break;
                    case ShipSections.armour:
                        break;
                    case ShipSections.engine:
                        break;
                    default:
                        break;
                }
    }
}
