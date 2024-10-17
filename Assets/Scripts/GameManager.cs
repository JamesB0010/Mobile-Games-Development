using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    //Attributes
    [Header("Enemy Spawning")]
    [SerializeField] EnemySpawnManager enemySpawnManager;
    
    
    [Header("Enemy Management")]
    ActiveEnemiesManager activeEnemiesManager;
    
    
    [Header("Events")]
    [SerializeField] private UnityEvent AlertRoundFinished = new UnityEvent();


    private void Start()
    {
        SpawnEnemies();
        SubscribeToEvents();
    }
    private void SpawnEnemies()
    {
        this.activeEnemiesManager = new ActiveEnemiesManager();
        enemySpawnManager.SpawnEnemies(this, this.activeEnemiesManager);
    }

    private void SubscribeToEvents()
    {
        this.activeEnemiesManager.AllEnemiesDead += this.OnAllEnemiesDead;
    }


    private void OnAllEnemiesDead()
    {
        this.AlertRoundFinished?.Invoke();
    }

}
