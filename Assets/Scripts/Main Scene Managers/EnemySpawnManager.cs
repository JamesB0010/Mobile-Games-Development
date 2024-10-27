using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private float enemiesToSpawn;
    [SerializeField]
    private Transform spawnBoundaryPositiveX,
        spawnBoundaryNegativeX,
        spawnBoundaryPositiveY,
        spawnBoundaryNegativeY,
        spawnBoundaryPositiveZ,
        spawnBoundaryNegativeZ;



    public void SpawnEnemies(GameManager gameManager, ActiveEnemiesManager enemiesManager)
    {
        short activeEnemies = 0;

        for (int i = 0; i < this.enemiesToSpawn; i++)
        {
            activeEnemies++;
            float minX = this.spawnBoundaryNegativeX.position.x;
            float maxX = this.spawnBoundaryPositiveX.position.x;

            float minY = this.spawnBoundaryNegativeY.position.y;
            float maxY = this.spawnBoundaryPositiveY.position.y;

            float minZ = this.spawnBoundaryNegativeZ.position.z;
            float maxZ = this.spawnBoundaryPositiveZ.position.z;

            Enemy enemy = Instantiate(enemyPrefab, new Vector3(Random.Range(minX, minY), Random.Range(minY, maxY), Random.Range(minZ, maxZ)), Random.rotation);
            enemy.EnemiesManager = enemiesManager;
        }

        enemiesManager.ActiveEnemyCount = activeEnemies;
    }
}
