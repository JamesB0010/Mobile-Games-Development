using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipPartLabelDirector : MonoBehaviour
{

    [Header("Dependencies")]
    [SerializeField] private CellsSetupHelper cellsSetupHelper;
    [SerializeField] private UIViewUpdater uiUpdater;
    [SerializeField] private EquipItem equipItemAction;

    [Space]

    [SerializeField] private ShipPartLabel[] shipPartLabels;



    [Space(2)]
    [Header("Events")]
    [SerializeField] private UnityEvent<ShipPartLabel> ShipPartLabelClicked = new UnityEvent<ShipPartLabel>();

    private void Start()
    {
        foreach (ShipPartLabel label in this.shipPartLabels)
        {
            label.clicked += OnShipPartClicked;
        }
    }

    private void OnShipPartClicked(ShipPartLabel label)
    {
        this.ShipPartLabelClicked?.Invoke(label);
        switch (label.ShipSection)
        {
            case ShipSections.lightWeapons:
                this.equipItemAction.UpdatePreviouslyOwnedLightWeapon(label.WeaponIndex);
                break;
            case ShipSections.heavyWeapons:
                this.equipItemAction.UpdatePreviouslyOwnedHeavyWeapon(label.WeaponIndex);
                break;
            case ShipSections.armour:
                this.equipItemAction.UpdatePreviouslyOwnedArmour();
                break;
            case ShipSections.energy:
                this.equipItemAction.UpdatePreviouslyOwnedEnergySystem();
                break;
            case ShipSections.engine:
                this.equipItemAction.UpdatePreviouslyOwnedEngine();
                break;
            default:
                break;
        }
        uiUpdater.UpdateItemDetailsText(label.ShipSection, label.WeaponIndex);

        this.cellsSetupHelper.SetupCells(label);
    }
}
