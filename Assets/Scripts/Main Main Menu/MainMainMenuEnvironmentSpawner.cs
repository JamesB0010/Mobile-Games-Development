using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MainMainMenuEnvironmentSpawner : MonoBehaviour
{
    [SerializeField] private AssetReference asterioids;

    [SerializeField] private Transform[] asteriodPlaces;

    [SerializeField] private AssetReference buzzardShip;

    [SerializeField] private Transform buzzardShipSpawn;

    [SerializeField] private AssetReference harb;

    [SerializeField] private Transform[] harbSpawns;

    private void Start()
    {
        SpawnThings(asterioids, asteriodPlaces);
        SpawnThings(buzzardShip, new Transform[]{buzzardShipSpawn});
        SpawnThings(harb, harbSpawns);
    }

    private void SpawnThings(AssetReference thingToSpawn, Transform[] spawnTransforms)
    {
        for (int i = 0; i < spawnTransforms.Length; i++)
        {
            int j = i;
            thingToSpawn.InstantiateAsync().Completed += handle =>
            {
                Transform trans = handle.Result.transform;
                trans.position = spawnTransforms[j].position;
                trans.rotation = spawnTransforms[j].rotation;
                trans.localScale = spawnTransforms[j].localScale;
            };
        }
    }
}
