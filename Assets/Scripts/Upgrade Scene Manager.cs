using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSceneManager : MonoBehaviour
{
    [SerializeField] private FloatReference playerMoney;
    
    public bool PurchaseGun(Gun gun, float price)
    {
        if (price <= (float)playerMoney.GetValue())
        {
            float newBalance = (float)playerMoney.GetValue() - price;
            this.playerMoney.SetValue(newBalance);
            
            //Do gun purchasing logic here
            return true;
        }
        return false;
    }
}
