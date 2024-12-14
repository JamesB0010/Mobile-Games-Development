using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiUpdaterInteractorHeavy : UiUpdaterInteractorStrategy
{
    public UiUpdaterInteractorHeavy(UIViewUpdater ui) : base(ui)
    {
    }

    public override void UpdateCurrentEquippedItemDetailsText(int index = 0)
    {
        PlayerUpgradesState upgradesState = ui.PlayerUpgradesState;
        HeavyGun heavyGun = (HeavyGun)upgradesState.HeavyGuns[index].Gun;
        this.ui.SetItemStatsUi(heavyGun);
    }

    public override void UpdateUi(ShipItem item)
    {
        this.ui.SetItemStatsUi((HeavyGun)item);
    }

    public override void UpdateItemToPurchaseStats(ShipItem item)
    {
        HeavyGun hGun = (HeavyGun)item;

        if (!hGun.AbleToShoot)
        {
            this.ui.HeavyWeaponToPurchaseIsNoGun();
            return;
        }
            
        
        this.ui.UpdateHeavyWeaponToPurchase(hGun.ItemName, 
            hGun.TimeBetweenBullets.ToString(), 
            hGun.BulletDamage.ToString(), 
            hGun.MaxAmmoCount.ToString(), 
            hGun.Firepower.ToString()
            );
    }
}
