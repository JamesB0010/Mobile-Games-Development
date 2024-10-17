using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEnemiesManager
{
    //Attributes
    private short activeEnemyCount;
    public short ActiveEnemyCount
    {
        get => this.activeEnemyCount;

        set
        {
            this.activeEnemyCount = value;
            
            if(this.activeEnemyCount <= 0)
                this.AllEnemiesDead?.Invoke();
        }
    }
    
    //Events
    public event Action EnemyDeathEvent;
    public event Action AllEnemiesDead;
    
    //methods
    public void EnemyDied()
    {
        EnemyDeathEvent?.Invoke();

        this.ActiveEnemyCount--;
    }
}
