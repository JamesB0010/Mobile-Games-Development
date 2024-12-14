using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class ShipStatsArmourField : MonoBehaviour
{
    private TextMeshProUGUI text;

    [SerializeField] private PlayerUpgradesState playerUpgradesState;

    private void Awake()
    {
        this.text = GetComponent<TextMeshProUGUI>();
        this.playerUpgradesState.shipArmourChanged += this.OnArmourValueUpdated;
    }

    public void OnArmourValueUpdated(float newAbsorbtionMultiplier)
    {
        text.text = ((1 - newAbsorbtionMultiplier) * 100).ToString();
    }
}
