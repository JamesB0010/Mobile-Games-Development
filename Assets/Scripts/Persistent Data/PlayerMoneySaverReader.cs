using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoneySaverReader : MonoBehaviour
{
    [SerializeField] private FloatReference playerMoney;
    void Start()
    {
        float pMoney = FindObjectOfType<BuzzardGameData>().GetPlayerMoney();
        this.playerMoney.SetValue(pMoney);
    }
}
