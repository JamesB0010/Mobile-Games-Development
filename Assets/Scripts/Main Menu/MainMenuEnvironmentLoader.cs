using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MainMenuEnvironmentLoader : MonoBehaviour
{
    [SerializeField] private AssetReference environemnt;

    private void Start()
    {
        environemnt.InstantiateAsync();
    }
}
