using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ShipStatsSpeedField : MonoBehaviour
{
    private TextMeshProUGUI text;

    [SerializeField] private PlayerUpgradesState playerUpgradesState;

    private void Awake()
    {
        this.text = GetComponent<TextMeshProUGUI>();
        this.playerUpgradesState.shipMaxSpeedChanged += this.MaxSpeedChanged;
    }

    private void MaxSpeedChanged(float newMaxSpeed)
    {
        this.text.text = newMaxSpeed.ToString();
    }
}
