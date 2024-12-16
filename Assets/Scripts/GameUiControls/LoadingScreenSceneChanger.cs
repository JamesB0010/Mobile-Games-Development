using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenSceneChanger : MonoBehaviour
{
    private IEnumerator Start()
    {
        yield return new WaitForSeconds(0);
        FindObjectOfType<BannerAdExample>().AddSpawnListener(this.ChangeScene);
    }

    public void ChangeScene()
    {
        SceneManager.LoadSceneAsync(LoadingScreenSceneIndexCounter.NextSceneIndex, LoadSceneMode.Single);
    }
}
