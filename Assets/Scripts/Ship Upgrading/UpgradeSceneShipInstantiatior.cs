using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;

public class UpgradeSceneShipInstantiatior : MonoBehaviour
{
    [SerializeField] private AssetReference playerShip;

    [SerializeField] private Transform spawnParent;
    [SerializeField] private Vector3 spawnPos;

    [SerializeField] private UnityEvent modelLoaded;
    private void Awake()
    {
        this.playerShip.InstantiateAsync(this.spawnPos, Quaternion.identity, this.spawnParent).Completed += handle =>
        {
            handle.Result.transform.localScale = new Vector3(500.0f,500.0f,500.0f);
            this.modelLoaded?.Invoke();
        };
    }
}
