using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class EnemySpawnManager : MonoBehaviour
{
    [SerializeField] private ActiveEnemiesManager activeEnemiesManager;

    [SerializeField] private ColorReference enemyOutlineColor;
    
    [SerializeField]
    private Transform spawnBoundaryPositiveX,
        spawnBoundaryNegativeX,
        spawnBoundaryPositiveY,
        spawnBoundaryNegativeY,
        spawnBoundaryPositiveZ,
        spawnBoundaryNegativeZ;


    [SerializeField] private AssetReference enemy;

    [SerializeField] private AssetReference enemyHarb;


    public AsyncOperationHandle<GameObject> SpawnBasicEnemy()
    {
        float minX = this.spawnBoundaryNegativeX.position.x;
        float maxX = this.spawnBoundaryPositiveX.position.x;

        float minY = this.spawnBoundaryNegativeY.position.y;
        float maxY = this.spawnBoundaryPositiveY.position.y;

        float minZ = this.spawnBoundaryNegativeZ.position.z;
        float maxZ = this.spawnBoundaryPositiveZ.position.z;

        Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));

        var spawnEnemyHandle = this.enemy.InstantiateAsync(spawnPos, Random.rotation);

        spawnEnemyHandle.Completed += this.EnemySpawned;

        return spawnEnemyHandle;
    }

    public AsyncOperationHandle<GameObject> SpawnSpecialEnemy()
    {
         float minX = this.spawnBoundaryNegativeX.position.x;
         float maxX = this.spawnBoundaryPositiveX.position.x;
 
         float minY = this.spawnBoundaryNegativeY.position.y;
         float maxY = this.spawnBoundaryPositiveY.position.y;
 
         float minZ = this.spawnBoundaryNegativeZ.position.z;
         float maxZ = this.spawnBoundaryPositiveZ.position.z;
 
         Vector3 spawnPos = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), Random.Range(minZ, maxZ));

         var spawnEnemyHandle = this.enemyHarb.InstantiateAsync(spawnPos, Random.rotation);

         spawnEnemyHandle.Completed += this.SpecialEnemySpawned;
         
         return spawnEnemyHandle;
    }
    private void EnemySpawned(AsyncOperationHandle<GameObject> handle)
    {
        EnemyBase enemy = handle.Result.GetComponent<EnemyBase>();
        enemy.EnemiesManager = this.activeEnemiesManager;
        
        this.activeEnemiesManager.TrackEnemyAsActive(enemy);

        enemy.GetComponentInChildren<Outline>().OutlineColor = this.enemyOutlineColor.GetValue();
    }
    
    private void SpecialEnemySpawned(AsyncOperationHandle<GameObject> handle)
    {
        EnemyBase enemy = handle.Result.GetComponent<EnemyBase>();
        enemy.EnemiesManager = this.activeEnemiesManager;
            
        this.activeEnemiesManager.TrackEnemyAsActive(enemy);
        
        foreach (Outline outline in enemy.GetComponentsInChildren<Outline>())
        {
            outline.OutlineColor = this.enemyOutlineColor.GetValue();
        }
    }
}
