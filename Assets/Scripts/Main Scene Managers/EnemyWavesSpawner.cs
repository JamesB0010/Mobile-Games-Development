using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.Serialization;

public class EnemyWavesSpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawnManager spawner;

    [SerializeField] private int baseNumberOfNormalEnemies;
    

    [SerializeField] private float normalEnemyRoundScalar;
    
    [SerializeField] private int baseNumberOfSpecialEnemies;
    [SerializeField] private float specialEnemyRoundScalar;

    private List<AsyncOperationHandle<GameObject>> spawningEnemies = new();

    public UnityEvent WaveSpawned;

    private int round = 1;


    [Header("Debug")] 
    [SerializeField] private bool harbOnRound1;

    public void AllEnemiesKilled()
    {
        this.round++;
    }

    public void SpawnRound()
    {
        if (this.harbOnRound1)
        {
            if (round == 1)
            {
                this.SpawnSpecialRound();
                for (int i = 0; i < this.spawningEnemies.Count; i++)
                {
                    this.spawningEnemies[i].Completed += handle =>
                    {
                        this.spawningEnemies.Remove(handle);

                        if (this.spawningEnemies.Count == 0)
                            this.WaveSpawned?.Invoke();
                    };
                }

                return;
            }
        }
        
        if (this.round % 5 != 0)
            this.SpawnNormalRound();
        else
            this.SpawnSpecialRound();

        for (int i = 0; i < this.spawningEnemies.Count; i++)
        {
            this.spawningEnemies[i].Completed += handle =>
            {
                this.spawningEnemies.Remove(handle);

                if (this.spawningEnemies.Count == 0)
                    this.WaveSpawned?.Invoke();
            };
        }
    }

    private void SpawnSpecialRound()
    {
        for (int i = 0; i < this.CalculateEnemyCountForSpecialRound(); i++)
        {
            this.spawningEnemies.Add(this.spawner.SpawnSpecialEnemy());
        }
    }   

    private void SpawnNormalRound()
    {
        for (int i = 0; i < this.CalculateEnemyCountForNormalRound(); i++)
        {
            this.spawningEnemies.Add(this.spawner.SpawnBasicEnemy());
        }
    }

    private int CalculateEnemyCountForNormalRound()
    {
        int count = (int)(this.baseNumberOfNormalEnemies * (this.round * normalEnemyRoundScalar));
        if (count < this.baseNumberOfNormalEnemies)
            return this.baseNumberOfNormalEnemies;
        else
            return count;
    }

    private int CalculateEnemyCountForSpecialRound()
    {
        int count = (int)(this.baseNumberOfSpecialEnemies * (this.round * specialEnemyRoundScalar));
        if (count < this.baseNumberOfSpecialEnemies)
            return this.baseNumberOfSpecialEnemies;
        else
            return count;
    }
}
