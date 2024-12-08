using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenSceneIndexCounter
{
    private static LoadingScreenSceneIndexCounter instance = null;
    
    private int nextSceneIndex = 0;
    public static LoadingScreenSceneIndexCounter Instance {
        get
        {
            ConditionalInstantiation();
            
            
            return instance;
        }
    }

    private LoadingScreenSceneIndexCounter()
    {
    }

    public static int NextSceneIndex
    {
        get
        {
            ConditionalInstantiation();

            return instance.nextSceneIndex;
        }

        set
        {
            ConditionalInstantiation();

            instance.nextSceneIndex = value;
        }
    }

    private static void ConditionalInstantiation()
    {
        if (LoadingScreenSceneIndexCounter.instance == null)
            instance = new LoadingScreenSceneIndexCounter();
    }
}
