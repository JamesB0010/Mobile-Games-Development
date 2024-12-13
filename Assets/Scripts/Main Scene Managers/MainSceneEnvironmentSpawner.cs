using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MainSceneEnvironmentSpawner : MonoBehaviour
{
    [SerializeField] private AssetReference environmentAsset;

    private void Start()
    {
        this.environmentAsset.InstantiateAsync();
    }
}
