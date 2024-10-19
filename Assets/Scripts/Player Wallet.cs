using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField]
    private FloatReference money;

    public void OnEnemyKilled()
    {
        this.money += 10000f;
    }

    public void OnApplicationQuit()
    {
        PlayerPrefs.SetFloat(PlayerPrefsKeys.PlayerMoneyKey, (float)this.money.GetValue());
    }

    public void OnApplicationFocus(bool hasFocus)
    {
        if (hasFocus)
        {
            PlayerPrefs.SetFloat(PlayerPrefsKeys.PlayerMoneyKey, (float)this.money.GetValue());
        }
    }
}
