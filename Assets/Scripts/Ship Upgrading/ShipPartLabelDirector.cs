using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipPartLabelDirector : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField] private ShipPartLabel[] shipPartLabels;

    [SerializeField] private UIViewUpdater uiUpdater;

    [SerializeField] private UpgradeCell[] cells;

    [Space(2)]
    [Header("Events")]
    [SerializeField] private UnityEvent ShipPartLabelClicked = new UnityEvent();

    private void Start()
    {
        foreach (ShipPartLabel label in this.shipPartLabels)
        {
            label.clicked += OnShipPartClicked;
        }
    }

    private void OnShipPartClicked(ShipPartLabel label)
    {
        this.ShipPartLabelClicked?.Invoke();
        uiUpdater.UpdateItemDetailsText(label.ShipSection, label.WeaponIndex);
        
        for (int i = 0; i < this.cells.Length; i++)
        {
            this.cells[i].Upgrade = label.Upgrades.shipGunUpgrades[i];
            this.cells[i].WeaponIndex = label.WeaponIndex;
            this.cells[i].ShipSection = label.ShipSection;
        }
    }
    
}
