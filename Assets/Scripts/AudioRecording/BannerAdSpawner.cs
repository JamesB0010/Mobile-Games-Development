using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class BannerAdSpawner : MonoBehaviour
{
    [SerializeField] BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;
 
    [SerializeField] string _adUnitId = "Banner_Android"; // This will remain null for unsupported platforms.

    private event Action BannerAdSpawned, Error;
    
    private static BannerAdSpawner instance = null;
    
    public void AddSpawnListener(Action handler)
    {
        this.BannerAdSpawned += handler;
    }

    public void AddFailureListener(Action handler)
    {
        this.Error += handler;
    }

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    void Start()
    {
        // Set the banner position:
        Advertisement.Banner.SetPosition(_bannerPosition);
 
        this.LoadBanner();

        SceneManager.sceneLoaded += this.OnSceneChange;
    }

    private void OnSceneChange(Scene to, LoadSceneMode mode)
    {
        this.BannerAdSpawned = null;
        this.HideBannerAd();
        if (to.name == "LoadingScreen" || to.name == "Voicelines Recorder")
        {
            StartCoroutine(nameof(this.ShowBannerAdAfter2Frames));
        }
    }

    private IEnumerator ShowBannerAdAfter2Frames()
    {
        yield return new WaitForSeconds(0);
        yield return new WaitForSeconds(0);
        this.ShowBannerAd();
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= this.OnSceneChange;
    }

    // Implement a method to call when the Load Banner button is clicked:
    public void LoadBanner()
    {
        // Set up options to notify the SDK of load events:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };
 
        // Load the Ad Unit with banner content:
        Advertisement.Banner.Load(_adUnitId, options);
    }
 
    // Implement code to execute when the loadCallback event triggers:
    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");
 
        this.ShowBannerAd();
    }
 
    // Implement code to execute when the load errorCallback event triggers:
    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
        
        this.Error?.Invoke();
    }
 
    // Implement a method to call when the Show Banner button is clicked:
    void ShowBannerAd()
    {
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };
 
        // Show the loaded Banner Ad Unit:
        Advertisement.Banner.Show(_adUnitId, options);
        
        this.BannerAdSpawned?.Invoke();
    }
 
    // Implement a method to call when the Hide Banner button is clicked:
    public void HideBannerAd()
    {
        // Hide the banner:
        Advertisement.Banner.Hide();
    }
 
    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }
}