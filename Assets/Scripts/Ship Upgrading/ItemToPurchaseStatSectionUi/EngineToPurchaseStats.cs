using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class EngineToPurchaseStats : MonoBehaviour
{
    [FormerlySerializedAs("engineNameTitle")] [SerializeField] private TextMeshProUGUI BoostEnergyDrainRateTitle;
    [SerializeField] private TextMeshProUGUI engineNameValField,
        accelerationSpeedValField,
        topSpeedValField,
        energyDrainRateValField,
        boostEnergyDrainRateField;

    public void SetFields(string engineName, string accelerationSpeed, string topSpeed, string energyDrainRate, string boostEnergyDrainRate)
    {
        this.BoostEnergyDrainRateTitle.enabled = true;
        this.boostEnergyDrainRateField.enabled = true;
        this.engineNameValField.text = engineName;
        this.accelerationSpeedValField.text = accelerationSpeed;
        this.topSpeedValField.text = topSpeed;
        this.energyDrainRateValField.text = energyDrainRate;
        this.boostEnergyDrainRateField.text = boostEnergyDrainRate;
    }
    
    public void SetFields(string engineName, string accelerationSpeed, string topSpeed, string energyDrainRate)
    {
        this.BoostEnergyDrainRateTitle.enabled = false;
        this.boostEnergyDrainRateField.enabled = false;
        this.BoostEnergyDrainRateTitle.text = engineName;
        this.accelerationSpeedValField.text = accelerationSpeed;
        this.topSpeedValField.text = topSpeed;
        this.energyDrainRateValField.text = energyDrainRate;
    }
}
