using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private UnityEvent AlertEnemyKilled = new UnityEvent();

    [SerializeField] private UnityEvent AlertRoundFinished = new UnityEvent();
    
    [SerializeField]
    private float enemiesToSpawn;

    [SerializeField] private Transform spawnBoundaryPositiveZ,
        spawnBoundaryNegativeZ,
        spawnBoundaryPositiveX,
        spawnBoundaryNegativeX,
        spawnBoundaryPositiveY,
        spawnBoundaryNegativeY;

    [SerializeField] private Enemy enemyPrefab;


    private short activeEnemies = 0;


    private void Start()
    {
        this.SpawnEnemies();
    }

    private void SpawnEnemies()
    {
        this.activeEnemies = 0;
        for (int i = 0; i < this.enemiesToSpawn; i++)
        {
            this.activeEnemies++;
            float minX = this.spawnBoundaryNegativeX.position.x;
            float maxX = this.spawnBoundaryPositiveX.position.x;

            float minY = this.spawnBoundaryNegativeY.position.y;
            float maxY = this.spawnBoundaryPositiveY.position.y;

            float minZ = this.spawnBoundaryNegativeZ.position.z;
            float maxZ = this.spawnBoundaryPositiveZ.position.z;
            
            Enemy enemy = Instantiate(enemyPrefab, new Vector3(Random.Range(minX, minY), Random.Range(minY, maxY), Random.Range(minZ, maxZ)), Random.rotation);
            enemy.GameManager = this;
        }
    }

    public void EnemyKilled()
    {
        this.AlertEnemyKilled?.Invoke();
        this.activeEnemies--;

        if (this.activeEnemies == 0)
        {
            this.AlertRoundFinished?.Invoke();
        }
    }
}
