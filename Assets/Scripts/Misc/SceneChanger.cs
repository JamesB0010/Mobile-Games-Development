using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : ScriptableObject
{
    public void LoadSceneSingle(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex, LoadSceneMode.Single);
    }

    public void LoadSceneWithLoadingScreen(int sceneIndex)
    {
        LoadingScreenSceneIndexCounter.NextSceneIndex = sceneIndex;
        SceneManager.LoadScene(3);
    }
}
