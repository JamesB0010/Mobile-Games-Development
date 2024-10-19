using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerWallet : MonoBehaviour
{
    [SerializeField]
    private FloatReference money;

    [SerializeField] private UnityEvent<string> OnMoneyChanged = new UnityEvent<string>();

    public void Start()
    {
        this.OnMoneyChanged?.Invoke("Credits: " + (float)this.money.GetValue());
    }

    public void OnEnemyKilled()
    {
        this.money += 10000f;
        this.OnMoneyChanged?.Invoke("Credits: " + (float)this.money.GetValue());
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
