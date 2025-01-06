using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenSceneChanger : MonoBehaviour
{
    private IEnumerator Start()
    {
        var adSpawners = FindObjectsOfType<BannerAdSpawner>();
        if (adSpawners.Length == 1)
        {
            adSpawners[0].AddSpawnListener(this.ChangeScene);
            adSpawners[0].AddFailureListener(this.ChangeScene);
            yield break;
        }
        yield return new WaitForSeconds(0);
        FindObjectOfType<BannerAdSpawner>().AddSpawnListener(this.ChangeScene);
        FindObjectOfType<BannerAdSpawner>().AddFailureListener(this.ChangeScene);
    }

    public void ChangeScene()
    {
        SceneManager.LoadSceneAsync(LoadingScreenSceneIndexCounter.NextSceneIndex, LoadSceneMode.Single);
    }
}
