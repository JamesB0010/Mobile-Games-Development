using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class EnergySystemToPurchaseStats : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI energySysNameValField, maxEnergyValField, rechargeRateValField;

    public void SetFields(string energySysName, string maxEnergy, string rechargeRate)
    {
        this.energySysNameValField.text = energySysName;
        this.maxEnergyValField.text = maxEnergy;
        this.rechargeRateValField.text = rechargeRate;
    }
}
