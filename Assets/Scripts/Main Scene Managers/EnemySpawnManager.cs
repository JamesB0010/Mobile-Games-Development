using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private ActiveEnemiesManager activeEnemiesManager;
    
    [SerializeField]
    private Transform spawnBoundaryPositiveX,
        spawnBoundaryNegativeX,
        spawnBoundaryPositiveY,
        spawnBoundaryNegativeY,
        spawnBoundaryPositiveZ,
        spawnBoundaryNegativeZ;


    [SerializeField] private AssetReference enemy;

    [SerializeField] private AssetReference enemyHarb;


    public void SpawnBasicEnemy()
    {
        float minX = this.spawnBoundaryNegativeX.position.x;
        float maxX = this.spawnBoundaryPositiveX.position.x;

        float minY = this.spawnBoundaryNegativeY.position.y;
        float maxY = this.spawnBoundaryPositiveY.position.y;

        float minZ = this.spawnBoundaryNegativeZ.position.z;
        float maxZ = this.spawnBoundaryPositiveZ.position.z;

        Vector3 spawnPos = new Vector3(Random.Range(minX, minY), Random.Range(minY, maxY), Random.Range(minZ, maxZ));

        var spawnEnemyHandle = this.enemy.InstantiateAsync(spawnPos, Random.rotation);

        spawnEnemyHandle.Completed += this.EnemySpawned;
    }

    private void SpawnHarb()
    {
        //spawn harb
        this.enemyHarb.InstantiateAsync();
    }

    private void EnemySpawned(AsyncOperationHandle<GameObject> handle)
    {
        handle.Result.GetComponent<Enemy>().EnemiesManager = this.activeEnemiesManager;
        this.activeEnemiesManager.IncrementEnemyCount();
    }
}
