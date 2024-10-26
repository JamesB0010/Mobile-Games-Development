using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class UpgradeCell : MonoBehaviour
{
    private ShipSections shipSection;

    public ShipSections ShipSection
    {
        get => this.shipSection;
        set => this.shipSection = value;
    }
    private ShipGunUpgrade upgrade; 
    private PurchaseItem _purchaseItem; 
    
    
    public int WeaponIndex { get; set; }

    private void Start()
    {
        this._purchaseItem = FindObjectOfType<PurchaseItem>();
        
    }
    public ShipGunUpgrade Upgrade
    {
        set
        {
            this.upgrade = value;
            GetComponent<UnityEngine.UI.Image>().sprite = this.upgrade.Icon;
            string textToDisplay = this.upgrade.IsPurchaseable ? this.upgrade.name : "";
            transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = textToDisplay;
        }
    }
    public float Cost()
    {
        return this.upgrade.Cost;
    }
    public string UpgradeName()
    {
        return this.upgrade.name;
    }
    
    public bool Purchaseable()
    {
        return this.upgrade.IsPurchaseable;
    }

    public bool IsOwned
    {
        get => this.upgrade.Gun.OwnedByPlayer;

        set => this.upgrade.Gun.OwnedByPlayer = true;
    }

    public Gun GetGun()
    {
        return this.upgrade.Gun;
    }

    public bool GunOwnedByThisSide()
    {
        return this.upgrade.ownedSides.Contains(WeaponIndex);
    }

    public void AddThisSideToUpgradeOwnedSides()
    {
        this.upgrade.ownedSides.Add(this.WeaponIndex);
    }
}
