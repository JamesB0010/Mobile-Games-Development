using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class EquipItem : ItemShopAction
{
    public event Action SelectedCellEquipped;


    [SerializeField] private ShipGunUpgrade previouslyOwnedLightWeapon;
    public ShipGunUpgrade PreviouslyOwnedLightWeapon => this.previouslyOwnedLightWeapon;

    public void EquipCell(UpgradeCell cell)
    {
        SaveToJson(cell);
    }

    protected override void SaveToJson(UpgradeCell cell)
    {
        switch (cell.ShipSection)
        {
            case ShipSections.lightWeapons:
                this.UpdatePreviouslyOwnedLightWeapon(cell.WeaponIndex);
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
    public void UpdatePreviouslyOwnedLightWeapon(ShipPartLabel label)
    {
        this.previouslyOwnedLightWeapon = base.playerWeaponsState.LightGuns[label.WeaponIndex];
    }

    public void UpdatePreviouslyOwnedLightWeapon(int side)
    {
        this.previouslyOwnedLightWeapon = base.playerWeaponsState.LightGuns[side];
    }
}
