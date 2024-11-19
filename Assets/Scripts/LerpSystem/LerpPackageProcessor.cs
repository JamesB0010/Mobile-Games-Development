using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpPackageProcessor
{
    private List<LerpPackage> packageList = new List<LerpPackage>();

    public void AddPackage(LerpPackage pkg)
    {
        this.packageList.Add(pkg);
    }

    public void Update()
    {
        for (int i = this.packageList.Count - 1; i >= 0; i--)
        {
            this.ProcessPackage(this.packageList[i], i);
        }
    }

    private void ProcessPackage(LerpPackage pkg, int pos)
    {
        LerpValue(pkg);


        RemovePackageIfComplete(pkg, pos);
    }


    private void LerpValue(LerpPackage pkg)
    {
        UpdateCurrentLerpPercentage(pkg);

        pkg.RunStepCallback();
    }

    private void UpdateCurrentLerpPercentage(LerpPackage pkg)
    {
        //Update the current lerp percentage
        pkg.elapsedtime += Time.deltaTime;

        pkg.currentPercentage = pkg.animCurve.Evaluate(pkg.elapsedtime / pkg.timeToLerp);

        pkg.currentPercentage = Mathf.Clamp01(pkg.currentPercentage);
    }
    private void RemovePackageIfComplete(LerpPackage pkg, int pos)
    {
        //remove package if it has finished lerping
        if (pkg.currentPercentage == 1.0f)
        {
            this.packageList.RemoveAt(pos);
            pkg.finalCallback(pkg);
        }
    }
}
