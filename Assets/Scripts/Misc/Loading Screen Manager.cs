using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    void Start()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single);
    }
}
