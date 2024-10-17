using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveEnemiesManager
{
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

    public event Action EnemyDeathEvent;

    public event Action AllEnemiesDead;

    public void EnemyDied()
    {
        EnemyDeathEvent?.Invoke();

        this.ActiveEnemyCount--;
    }
}
