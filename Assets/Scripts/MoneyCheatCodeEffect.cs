using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MoneyCheatCodeEffect : CheatCodeEffect
{
    [SerializeField] private FloatReference playerMoney;
    public override void Execute()
    {
        this.playerMoney.SetValue(522);
    }
}
