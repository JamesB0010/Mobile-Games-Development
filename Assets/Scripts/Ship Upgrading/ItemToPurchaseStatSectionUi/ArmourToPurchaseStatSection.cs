using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class ArmourToPurchaseStatSection : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI armourNameValField, energyUsedPerHitValField, minEnergyReqValField, shieldStrengthValField;

    public void SetFields(string armourName, string energyUsedPerHit, string minEnergyReq, string shieldStrength)
    {
        this.armourNameValField.text = armourName;
        this.energyUsedPerHitValField.text = energyUsedPerHit;
        this.minEnergyReqValField.text = minEnergyReq;
        this.shieldStrengthValField.text = shieldStrength;
    }
}
