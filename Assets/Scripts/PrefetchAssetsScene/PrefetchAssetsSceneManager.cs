using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.Events;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class PrefetchAssetsSceneManager : MonoBehaviour
{
    private AsyncOperationHandle downloadHandle;
    [SerializeField] private Text percentageText;

    [SerializeField] private Image bar;

    [SerializeField] private float fakeLoadTime;

    [SerializeField] private PrefetchAssetsLoadingText quipText;


    [SerializeField] private AssetLabelReference labelToLoad;
    
    
    

    IEnumerator Start()
    {
        AsyncOperationHandle<long> getDownloadSize = Addressables.GetDownloadSizeAsync(this.labelToLoad);
        yield return getDownloadSize;

        if (getDownloadSize.Result == 0)
        {
            float progress = 0;
            
            
            //make loading bar go to 100 then start
            0.0f.LerpTo(1.0f, this.fakeLoadTime, value =>
            {
                if (value > progress * 1.1)
                {
                    progress = value;
                    this.bar.fillAmount = progress;
                    this.percentageText.text = $"{progress * 100}%";
                }
            }, pkg =>
            {
                this.percentageText.text = "100%";
                StartCoroutine(nameof(this.ChangeScene));
            }, GlobalLerpProcessor.easeInOutCurve);
        }
        else
        {
            downloadHandle = Addressables.DownloadDependenciesAsync(this.labelToLoad, false);
            float progress = 0;

            while (downloadHandle.Status == AsyncOperationStatus.None)
            {
                float percentageComplete = downloadHandle.GetDownloadStatus().Percent;
                if (percentageComplete > progress * 1.1) // Report at most every 10% or so
                {
                    progress = percentageComplete; // More accurate %
                    this.bar.fillAmount = progress;
                    this.percentageText.text = $"{progress * 100}%";
                }
                yield return null;
            }
            
            this.percentageText.text = "100%";
            StartCoroutine(nameof(this.ChangeScene));
            Addressables.Release(downloadHandle); //Release the operation handle
        }
    }

    private IEnumerator ChangeScene()
    {
        this.percentageText.text = "0";
        this.bar.fillAmount = 0;
        var handle = SceneManager.LoadSceneAsync(1);
        this.quipText.GetComponent<Text>().text = "Loading Main Menu";
        this.quipText.enabled = false;

        float progress = 0;
        while (!handle.isDone)
        {
            float percentageComplete = handle.progress;
            if (percentageComplete > progress * 1.1)
            {
                progress = handle.progress;
                this.bar.fillAmount = progress;
                this.percentageText.text = $"{progress * 100}%";
            }
            
            if (percentageComplete > 0.9f)
                this.percentageText.text = "100%";

            yield return null;
        }
    }
}
