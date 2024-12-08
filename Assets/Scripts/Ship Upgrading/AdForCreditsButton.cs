using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class AdForCreditsButton : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [FormerlySerializedAs("adUnityId")] [SerializeField] private string adUnitId = "Rewarded_Android";
    [SerializeField] private int creditsAwarded;
    [SerializeField] private FloatReference playerMoney;
    private Button button;
    [SerializeField] private UnityEvent<int> displayAward = new UnityEvent<int>();

    private void Awake()
    {
        this.button = GetComponent<Button>();
        this.button.interactable = false;
        this.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"Watch Ad For {this.creditsAwarded} Credits";
        this.button.onClick.AddListener(this.ShowAdd);
    }

    private void Start()
    {
        this.LoadAd();
        this.gameObject.SetActive(false);
    }

    private void LoadAd()
    {
        AdsInitialize.ExecuteNowOrWhenInitialized(() =>
        {
            Debug.Log("Try load ad");
            this.button.interactable = false;
            Advertisement.Load(this.adUnitId, this);
        });
    }

    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad loaded");
        if (adUnitId.Equals(this.adUnitId))
        {


            this.button.interactable = true;
        }
    }

    private void ShowAdd()
    {
        this.button.interactable = false;
        
        Advertisement.Show(this.adUnitId, this);
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}"); 
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {this.adUnitId}: {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowStart(string placementId)
    {
    }

    public void OnUnityAdsShowClick(string placementId)
    {
    }

    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        this.LoadAd();
        bool eligableForAward = adUnitId.Equals(this.adUnitId) && showCompletionState.Equals(UnityAdsCompletionState.COMPLETED);
        
        #if UNITY_EDITOR
        eligableForAward = true;
        #endif
        
        if (eligableForAward)
        {
            //grant reward
            Debug.Log("Rewareded");
            this.playerMoney.SetValue(this.playerMoney.GetValue() + this.creditsAwarded);
            //TODO debut this save function BuzzardGameData seems to be running into a null ref exception
            BuzzardGameData.Save();
            //display screen
            this.displayAward?.Invoke(this.creditsAwarded);
        }
        
    }

    private void OnDestroy()
    {
        this.button.onClick.RemoveListener(this.ShowAdd);
    }
}
