using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenSceneIndexCounter
{
    private static LoadingScreenSceneIndexCounter instance = null;
    
    private int sceneIndex = 0;
    public static LoadingScreenSceneIndexCounter Instance {
        get
        {
            ConditionalInstantiation();
            
            
            return instance;
        }
    }

    private LoadingScreenSceneIndexCounter()
    {
        SceneManager.activeSceneChanged += this.OnSceneChanged;
    }

    public static int SceneIndex
    {
        get
        {
            ConditionalInstantiation();

            return instance.sceneIndex;
        }

        set
        {
            ConditionalInstantiation();

            instance.sceneIndex = value;
        }
    }

    private static void ConditionalInstantiation()
    {
        if (LoadingScreenSceneIndexCounter.instance == null)
            instance = new LoadingScreenSceneIndexCounter();
    }

    private void OnSceneChanged(Scene oldScene, Scene newScene)
    {
        if (newScene.name == "LoadingScreen")
        {
            //load next scene
            this.sceneIndex++;
            this.sceneIndex %= 3;
        }
    }
}
