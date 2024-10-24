using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActiveEnemiesManager: MonoBehaviour
{
    //Attributes
    private short activeEnemyCount;
    public short ActiveEnemyCount
    {
        get => this.activeEnemyCount;

        set
        {
            this.activeEnemyCount = value;
        }
    }
    
    //Events
    [SerializeField]
    public UnityEvent EnemyDeathEvent;
    
    //methods
    public void EnemyDied()
    {
        EnemyDeathEvent?.Invoke();

        this.ActiveEnemyCount--;
    }
}
