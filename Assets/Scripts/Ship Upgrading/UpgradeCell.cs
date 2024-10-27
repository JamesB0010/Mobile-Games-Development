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
    public ShipGunUpgrade Upgrade
    {
        get => this.upgrade;
        set
        {
            this.upgrade = value;
            UpdateUpgradeCellUI();
        }
    }

    private void UpdateUpgradeCellUI()
    {
        GetComponent<UnityEngine.UI.Image>().sprite = this.upgrade.Icon;
        string textToDisplay = this.upgrade.IsPurchaseable ? this.upgrade.name : "";
        transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = textToDisplay;
    }

    public int WeaponIndex { get; set; }

    public bool GunOwnedByThisSide()
    {
        //are there enough owned?
        return true;
    }
}
