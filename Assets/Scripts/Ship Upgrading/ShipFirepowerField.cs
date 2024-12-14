using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ShipFirepowerField : MonoBehaviour
{
    private TextMeshProUGUI text;

    [SerializeField] private PlayerUpgradesState playerUpgradesState;

    private void Awake()
    {
        this.text = GetComponent<TextMeshProUGUI>();
        this.playerUpgradesState.shipFirepowerChanged += this.OnShipFirepowerUpdated;
    }

    private void OnShipFirepowerUpdated(float newFirepower)
    {
        this.text.text = newFirepower.ToString();
    }
}
