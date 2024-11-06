using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

//Responsibilities
//1. Notify others when clicked
//2. Hold a selection of upgrades for this part
public class ShipPartLabel : MonoBehaviour
{
    [SerializeField] private ShipUpgradesPlatterBase upgrades;
    public ShipUpgradesPlatterBase Upgrades => this.upgrades;

    [SerializeField]
    private CinemachineVirtualCamera shipSectionCamera;

    [SerializeField] private ShipSections shipSection;
    public ShipSections ShipSection => this.shipSection;

    public event Action<ShipPartLabel> clicked;

    [SerializeField] private int weaponIndex;

    public int WeaponIndex => this.weaponIndex;

    public void Clicked()
    {
        clicked?.Invoke(this);

        this.shipSectionCamera.gameObject.SetActive(true);
    }

    public EquipItemInteractorStrategy GenerateItemEquipInteractor(EquipItem equipItemAction)
    {
        return this.upgrades.GetShipUpgrades()[0].GenerateEquipItemInteractor(equipItemAction);
    }
}
