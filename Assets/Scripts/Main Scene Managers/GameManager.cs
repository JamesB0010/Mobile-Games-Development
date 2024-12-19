using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField] private EnemyWavesSpawner waveSpawner;
    [SerializeField] private ActiveEnemiesManager enemiesManager;
    
    private Transform player;

    public Transform Player => this.player;

    
    public void OnPlayerSpawned(GameObject player)
    {
        //get the moving part of the player
        Transform movingPart = player.transform.GetChild(0);
        this.player = movingPart;
        
        this.SetupPlayerDependencies(player.GetComponent<UsefulPlayerComponents>());
        
        this.waveSpawner.SpawnRound();
    }


    private void SetupPlayerDependencies(UsefulPlayerComponents player)
    {
        DirectionalMarkersManager markerManager = player.DirectionalMarkersManager;
        this.waveSpawner.WaveSpawned.AddListener(markerManager.RoundSpawned);
        markerManager.Enemies = this.enemiesManager.ActiveEnemies;
        
        
        this.enemiesManager.EnemyDeathEvent.AddListener(markerManager.OnEnemyKilled);
    }
}
