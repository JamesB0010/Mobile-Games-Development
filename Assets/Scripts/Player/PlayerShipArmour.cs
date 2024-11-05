using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerShipArmour : MonoBehaviour
{
    private Armour armour;

    [SerializeField] private PlayerUpgradesState playerUpgradesState;

    private void Start()
    {
        this.armour = this.playerUpgradesState.Armour.Armour;
    }
}
