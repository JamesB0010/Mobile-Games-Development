using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


[CustomEditor(typeof(OwnedUpgradesCounter))]
public class Inspector_OwnedUpgradesCounter : Editor
{
    private OwnedUpgradesCounter castedTarget;
    private void OnEnable()
    {
        this.castedTarget = (OwnedUpgradesCounter)this.target;
    }

    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();

        GUILayout.Space(15);

        if (GUILayout.Button("Print Stored Upgrades and Counts"))
        {
            Debug.Log("Upgrade Pair Counts:");
            if (this.castedTarget.UpgradesCount.Count == 0)
                Debug.Log("No Upgrades stored");

            foreach (var upgradePair in this.castedTarget.UpgradesCount)
            {
                Debug.Log("Upgrade Name: " + upgradePair.Key.name + " Count: " + upgradePair.Value);
            }
        }


        if (GUILayout.Button("Save state To json"))
        {
            this.castedTarget.SaveToJson();
        }


        if (GUILayout.Button("Generate Default Save File"))
        {
            new UpgradesCounterJsonObject().GenerateDefaultSafeFile(BuzzardGameData.OwnedUpgradesConfigFile);
        }
    }
}
