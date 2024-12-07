using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoneySaverReader : MonoBehaviour
{
    [SerializeField] private FloatReference playerMoney;
    void Start()
    {
        #if UNITY_EDITOR
        
        #else
        float pMoney = FindObjectOfType<BuzzardGameData>().GetPlayerMoney();
        this.playerMoney.SetValue(pMoney);
        #endif
    }
}
