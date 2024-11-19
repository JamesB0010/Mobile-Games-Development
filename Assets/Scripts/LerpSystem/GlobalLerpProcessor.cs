using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalLerpProcessor : MonoBehaviour
{

    private static GlobalLerpProcessor instance = null;

    public static AnimationCurve linearCurve = AnimationCurve.Linear(0, 0, 1, 1);
    public static AnimationCurve easeInOutCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    private LerpPackageProcessor lerpProcessor = new LerpPackageProcessor();

    public static void AddLerpPackage(LerpPackage pkg)
    {
        if (instance == null)
        {
           GameObject obj = new GameObject("Global Lerp Processor");
           GlobalLerpProcessor component = obj.AddComponent<GlobalLerpProcessor>();
           GlobalLerpProcessor.instance = component; 
        }
        
        pkg.AddToProcessor(ref instance.lerpProcessor);
    }
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
        Destroy(this.gameObject);
    }
    
    //Method to handle this scene getting unloaded
    private void InvalidateInstance()
    {
        GlobalLerpProcessor.instance = null;
    }
    
    //when instance is destroyed unbind from SceneManager.SceneUnloaded
    ~GlobalLerpProcessor()
    {
        this.InvalidateInstance();
    }

    void Update()
    {
        this.lerpProcessor.Update();
    }

    public static bool RemovePackage(LerpPackage package)
    {
        return instance.lerpProcessor.RemovePackage(package);
    }
}
