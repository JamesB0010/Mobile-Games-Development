using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActiveEnemiesManager : MonoBehaviour
{
    //Attributes
    private short activeEnemyCount;

    //Events
    [SerializeField]
    public UnityEvent<EnemyBase> EnemyDeathEvent;

    [SerializeField] private UnityEvent AllEnemiesDead;

    //methods
    public void EnemyDied(EnemyBase enemy)
    {
        EnemyDeathEvent?.Invoke(enemy);

        this.activeEnemyCount--;
        
        if(this.activeEnemyCount == 0)
            this.AllEnemiesDead?.Invoke();
    }

    public void IncrementEnemyCount()
    {
        this.activeEnemyCount++;
    }
}
