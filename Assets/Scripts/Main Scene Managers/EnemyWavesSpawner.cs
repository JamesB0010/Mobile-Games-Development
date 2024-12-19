using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWavesSpawner : MonoBehaviour
{
    [SerializeField] private EnemySpawnManager spawner;

    [SerializeField] private int baseNumberOfEnemies;

    [SerializeField] private float roundScaler;

    private int round = 1;

    public void AllEnemiesKilled()
    {
        this.round++;
    }

    public void SpawnRound()
    {
        for (int i = 0; i < this.CalculateEnemyCountForRound(); i++)
        {
            this.spawner.SpawnBasicEnemy();
        }
    }

    private int CalculateEnemyCountForRound()
    {
        return (int)(this.baseNumberOfEnemies * (this.round * roundScaler));
    }

    private void Start()
    {
        this.SpawnRound();
    }
}
