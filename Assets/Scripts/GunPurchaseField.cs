using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GunPurchaseField : MonoBehaviour
{
    private TextMeshProUGUI gunNameAndCostDisplay;

    [SerializeField] private Gun gunReference;

    private UpgradeSceneManager upgradeSceneManager;
    
    private void Start()
    {
        this.upgradeSceneManager = FindObjectOfType<UpgradeSceneManager>();
        this.gunNameAndCostDisplay = GetComponent<TextMeshProUGUI>();
        this.gunNameAndCostDisplay.text = this.gunNameAndCostDisplay.text + this.gunReference.Cost;
    }

    public void MakePurchase()
    {
        if (this.upgradeSceneManager.PurchaseGun(this.gunReference, gunReference.Cost))
        {
            this.gunNameAndCostDisplay.color = Color.green;
            this.gunReference.OwnedByPlayer = true;
        }
    }
}
