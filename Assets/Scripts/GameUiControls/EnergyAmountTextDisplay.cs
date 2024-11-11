using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Mime;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(TextMeshProUGUI))]
public class EnergyAmountTextDisplay : MonoBehaviour
{

    private TextMeshProUGUI energyAmountText;

    [SerializeField] private PlayerUpgradesState playerUpgradesState;
    private IEnumerator Start()
    {
        this.energyAmountText = GetComponent<TextMeshProUGUI>();
        this.energyAmountText.text = $"Energy Amount: \n {this.playerUpgradesState.EnergySystem.EnergySystem.MaxEnergy}";
        yield return new WaitForSeconds(0);
        FindObjectOfType<PlayerShipEnergySystem>().SubscribeToCurrentEnergyChanged(this.UpdateTextField);
        FindObjectOfType<PlayerShipEnergySystem>().SubscribeToEnergyPeaked(this.UpdateTextField);
    }

    private void UpdateTextField(float newEnergyValue)
    {
        this.energyAmountText.text = $"Energy Amount: \n {(int)newEnergyValue}";
    }
}
