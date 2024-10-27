using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoneySaverReader : MonoBehaviour
{
    [SerializeField] private FloatReference playerMoney;
    void Start()
    {
        float pMoney = PlayerPrefs.GetFloat(PlayerPrefsKeys.PlayerMoneyKey);
        this.playerMoney.SetValue(pMoney);
        Debug.Log(pMoney);
    }


}
