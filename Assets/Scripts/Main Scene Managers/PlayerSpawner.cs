using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;


public class PlayerSpawner : MonoBehaviour
{
    public UnityEvent<GameObject> PlayerSpawnedEvent;

    [SerializeField] private AssetReference player;

    private void Start()
    {
        player.InstantiateAsync().Completed += handle =>
        {
            this.PlayerSpawnedEvent?.Invoke(handle.Result);
        };
    }
}
