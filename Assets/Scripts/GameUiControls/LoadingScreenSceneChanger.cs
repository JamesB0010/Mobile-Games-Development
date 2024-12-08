using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenSceneChanger : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene(LoadingScreenSceneIndexCounter.NextSceneIndex, LoadSceneMode.Single);

    }
}
