using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

public class OwnedUpgradesCounter : MonoBehaviour
{
    private static OwnedUpgradesCounter instance = null;
    public static OwnedUpgradesCounter Instance => OwnedUpgradesCounter.instance;
    
    internal Dictionary<ShipGunUpgrade, int> upgradesCount = new Dictionary<ShipGunUpgrade, int>();

    [SerializeField] private ShipGunUpgrade[] allEquippableUpgrades;


    private SelectedCellHighlight cellHighlight;

    private void Awake()
    {
        if (OwnedUpgradesCounter.instance == null)
            OwnedUpgradesCounter.instance = this;
        else
            Destroy(this.gameObject);
            
    }

    private void Start()
    {
        this.cellHighlight = FindObjectOfType<SelectedCellHighlight>();
        foreach (ShipGunUpgrade upgrade in GenerateHashSet(this.allEquippableUpgrades))
        {
            this.upgradesCount.Add(upgrade, 0);
        }
    }

    private HashSet<T> GenerateHashSet<T>(T[] arrayIn)
    {
        HashSet<T> set = new HashSet<T>();
        foreach (T element in arrayIn)
        {
            set.Add(element);
        }

        return set;
    }


    //on upgrade purchased
    public void IncrementUpgradeCount(SelectedCellHighlight highlight)
    {
        this.upgradesCount[highlight.SelectedCell.Upgrade]++;
    }

    //on upgrade equipped
    public void OnUpgradeEquipped(EquipItem itemEquipper)
    {
        this.upgradesCount[this.cellHighlight.SelectedCell.Upgrade]--;
        this.upgradesCount[itemEquipper.PreviouslyOwnedLightWeapon]++;
    }

    public int GetUpgradeCount(ShipGunUpgrade upgrade)
    {
        return this.upgradesCount[upgrade];
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(OwnedUpgradesCounter))]
class OwnedUpgradesCounterCustomUI : Editor
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
            if (this.castedTarget.upgradesCount.Count == 0)
                Debug.Log("No Upgrades stored");
                
            foreach (var upgradePair in this.castedTarget.upgradesCount)
            {
                Debug.Log("Upgrade Name: " + upgradePair.Key.name + " Count: " + upgradePair.Value);
            }
        }
    }
}

#endif
