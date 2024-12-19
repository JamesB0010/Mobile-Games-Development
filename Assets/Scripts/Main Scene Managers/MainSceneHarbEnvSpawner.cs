using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MainSceneHarbEnvSpawner : MonoBehaviour
{
    [SerializeField] private AssetReference harb;

    [SerializeField] private Transform harbSpawnLocations;

    [SerializeField] private Transform environmentParent;

    private void Start()
    {
        for (int i = 0; i < harbSpawnLocations.childCount; i++)
        {
            Vector3 harbSpawnPosition = this.harbSpawnLocations.GetChild(i).position;
            Quaternion harbSpawnRotation = this.harbSpawnLocations.GetChild(i).rotation;
            harb.InstantiateAsync(harbSpawnPosition, harbSpawnRotation, this.environmentParent);
        }
    }
}
