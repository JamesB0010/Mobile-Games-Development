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
        this.money += 100f;
    }
}
