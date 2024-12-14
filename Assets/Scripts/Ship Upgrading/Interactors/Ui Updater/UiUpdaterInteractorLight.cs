using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UiUpdaterInteractorLight : UiUpdaterInteractorStrategy
{
    public UiUpdaterInteractorLight(UIViewUpdater ui) : base(ui)
    {
    }

    public override void UpdateCurrentEquippedItemDetailsText(int index = 0)
    {
        PlayerUpgradesState playerUpgradesState = this.ui.PlayerUpgradesState;
        LightGun lightGun = playerUpgradesState.LightGuns[index].Gun;
        this.ui.SetItemStatsUi(lightGun);
    }

    public override void UpdateUi(ShipItem item)
    {
        this.ui.SetItemStatsUi((LightGun)item);
    }

    public override void UpdateItemToPurchaseStats(ShipItem item)
    {
        LightGun lGun = (LightGun)item;

        if (!lGun.AbleToShoot)
        {
            this.ui.LightWeaponToPurchaseIsNoGun();
            return;
        }
        
        this.ui.UpdateLightGunWeaponToPurchase(lGun.ItemName, 
            lGun.TimeBetweenBullets.ToString(), 
            lGun.BulletDamage.ToString(),
            lGun.EnergyExpensePerShot.ToString(),
            lGun.Firepower.ToString()
            );
    }
}
