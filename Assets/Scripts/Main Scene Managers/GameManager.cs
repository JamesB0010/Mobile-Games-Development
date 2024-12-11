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
    
    private Transform player;

    public Transform Player => this.player;

    
    public void OnPlayerSpawned(GameObject player)
    {
        //get the moving part of the player
        Transform movingPart = player.transform.GetChild(0);
        this.player = movingPart;
        
        this.SpawnEnemies();
    }
    
    private void SpawnEnemies()
    {
        enemySpawnManager.SpawnEnemies(this, this.activeEnemiesManager);
    }

}
