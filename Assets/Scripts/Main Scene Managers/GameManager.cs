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
    [SerializeField] private ActiveEnemiesManager activeEnemiesManager;
    
    
    private void Start()
    {
        SpawnEnemies();
    }
    private void SpawnEnemies()
    {
        enemySpawnManager.SpawnEnemies(this, this.activeEnemiesManager);
    }

}
