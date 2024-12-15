using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenSceneChanger : MonoBehaviour
{
    public void ChangeScene()
    {
        SceneManager.LoadSceneAsync(LoadingScreenSceneIndexCounter.NextSceneIndex, LoadSceneMode.Single);
    }
}
