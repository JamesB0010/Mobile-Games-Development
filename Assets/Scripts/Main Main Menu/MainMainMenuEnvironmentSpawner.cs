using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MainMainMenuEnvironmentSpawner : MonoBehaviour
{
    [SerializeField] private AssetReference mainMainMenuAsteriods;

    [SerializeField] private Transform spawnPos;
    
    [SerializeField] private AssetReference buzzardShip;

    [SerializeField] private Transform buzzardShipSpawn;

    [SerializeField] private AssetReference harb;

    [SerializeField] private Transform[] harbSpawns;


    private void Start()
    {
        SpawnThings(this.mainMainMenuAsteriods, new[]{spawnPos});
        SpawnThings(buzzardShip, new Transform[]{buzzardShipSpawn});
        SpawnThings(harb, harbSpawns, new Vector3(-90, 0,0));
    }

    private void SpawnThings(AssetReference thingToSpawn, Transform[] spawnTransforms, Vector3 rotationToAdd = new Vector3())
    {
        for (int i = 0; i < spawnTransforms.Length; i++)
        {
            int j = i;
            thingToSpawn.InstantiateAsync().Completed += handle =>
            {
                Transform trans = handle.Result.transform;
                trans.position = spawnTransforms[j].position;
                trans.rotation = spawnTransforms[j].rotation;
                trans.localRotation = trans.localRotation * Quaternion.Euler(rotationToAdd);
                trans.localScale = spawnTransforms[j].localScale;

                if (trans.GetChild(0).TryGetComponent(out LODGroup group))
                    group.enabled = false;
            };
        }
    }
}
