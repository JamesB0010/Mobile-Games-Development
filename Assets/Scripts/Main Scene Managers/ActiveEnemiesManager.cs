using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ActiveEnemiesManager : MonoBehaviour
{
    //Attributes
    private List<EnemyBase> activeEnemies = new List<EnemyBase>();

    public List<EnemyBase> ActiveEnemies => this.activeEnemies;
    //Events
    [SerializeField]
    public UnityEvent<EnemyBase> EnemyDeathEvent;

    [SerializeField] private UnityEvent AllEnemiesDead;

    //methods
    public void EnemyDied(EnemyBase enemy)
    {
        EnemyDeathEvent?.Invoke(enemy);
        
        activeEnemies.Remove(enemy);

        
        if(this.activeEnemies.Count == 0)
            this.AllEnemiesDead?.Invoke();
    }

    public void TrackEnemyAsActive(EnemyBase enemy)
    {
        this.activeEnemies.Add(enemy);
    }
}
