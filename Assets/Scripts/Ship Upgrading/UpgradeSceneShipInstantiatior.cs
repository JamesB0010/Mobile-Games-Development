using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class UpgradeSceneShipInstantiatior : MonoBehaviour
{
    [SerializeField] private AssetReference playerShip;

    [SerializeField] private Transform spawnParent;
    [SerializeField] private Vector3 spawnPos;


    private void Start()
    {
        this.playerShip.InstantiateAsync(this.spawnPos, Quaternion.identity, this.spawnParent).Completed += handle =>
        {
            handle.Result.transform.localScale = new Vector3(500.0f,500.0f,500.0f);
        };
    }
}
