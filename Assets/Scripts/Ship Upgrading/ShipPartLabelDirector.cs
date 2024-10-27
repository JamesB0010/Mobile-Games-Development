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
        uiUpdater.UpdateItemDetailsText(label.ShipSection, label.WeaponIndex);

        this.cellsSetupHelper.SetupCells(label);
    }
}
