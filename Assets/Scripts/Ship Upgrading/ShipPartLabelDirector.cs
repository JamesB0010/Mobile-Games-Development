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
        label.GenerateItemEquipInteractor(this.equipItemAction).UpdatePrevOwned(label.WeaponIndex);

        label.GenerateUiUpdaterInteractor(this.uiUpdater).UpdateItemDetailsText(label.WeaponIndex);
        
        this.cellsSetupHelper.SetupCells(label);
    }
}








