using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitialize : MonoBehaviour, IUnityAdsInitializationListener
{
    private static AdsInitialize instance = null;
    
    [SerializeField] private string gameId;

    [SerializeField] private bool testMode = true;

    private List<Action> waitingActionQueue = new List<Action>();
    
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        
        this.InitializeAds();
    }

    public static void ExecuteNowOrWhenInitialized(Action callback)
    {
        if (Advertisement.isInitialized)
        {
            callback();
        }
        else
        {
            instance.waitingActionQueue.Add(callback);
        }
    }

    private void InitializeAds()
    {
        if(!Advertisement.isInitialized && Advertisement.isSupported)
            Advertisement.Initialize(this.gameId, this.testMode, this);
    }
    
    
    
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads Initialization complete");
        for (int i = 0; i < this.waitingActionQueue.Count; ++i)
        {
            this.waitingActionQueue[i]();
        }
        
        this.waitingActionQueue.Clear();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization failed: {error.ToString()} - {message}");
    }
}
