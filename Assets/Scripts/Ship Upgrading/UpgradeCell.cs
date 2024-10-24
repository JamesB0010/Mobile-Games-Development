using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    private Inventory inventory; 
    public int WeaponIndex { get; set; }
    private void Start() { this.inventory = FindObjectOfType<Inventory>(); }
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
    public void SetAsSelectedUpgradeCell()
    {
        this.inventory.SelectCell(this);
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
}
