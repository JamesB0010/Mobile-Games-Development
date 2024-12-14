using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ShipStatsEnergyField : MonoBehaviour
{
    private TextMeshProUGUI text;

    [SerializeField] private PlayerUpgradesState playerUpgradesState;

    private void Awake()
    {
        this.text = GetComponent<TextMeshProUGUI>();
        this.playerUpgradesState.shipEnergySystemChanged += this.UpdateTextValue;
    }

    private void UpdateTextValue(float newVal)
    {
        this.text.text = newVal.ToString();
    }
}
