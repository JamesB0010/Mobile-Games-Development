using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerShipEnergySystem : MonoBehaviour
{
    private EnergySystem energySystem;

    [FormerlySerializedAs("playerWeaponsState")] [SerializeField] private PlayerUpgradesState playerUpgradesState;


    private void Start()
    {
        this.energySystem = this.playerUpgradesState.EnergySystem.EnergySystem;
    }
}
