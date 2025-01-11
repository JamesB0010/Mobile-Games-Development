using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerShipStateLoader : MonoBehaviour
{
    [FormerlySerializedAs("playerWeaponsState")] [SerializeField] private PlayerUpgradesState playerUpgradesState;

    void Start()
    {
        string jsonStringLight = BuzzardGameData.LightWeaponsConfigFile.text;
        SavedUpgradesJsonObject lightUpgradesObject = JsonUtility.FromJson<SavedUpgradesJsonObject>(jsonStringLight);
        this.playerUpgradesState.LightGuns = lightUpgradesObject.GetSavedUpgrades().OfType<LightGunUpgrade>().ToList();

        string jsonStringHeavy = BuzzardGameData.HeavyWeaponsConfigFile.text;
        SavedUpgradesJsonObject heavyUpgradesObject = JsonUtility.FromJson<SavedUpgradesJsonObject>(jsonStringHeavy);
        this.playerUpgradesState.HeavyGuns = heavyUpgradesObject.GetSavedUpgrades().OfType<HeavyGunUpgrade>().ToList();


        string jsonStringArmour = BuzzardGameData.ArmourConfigFile.text;
        SavedUpgradesJsonObject armourUpgradesObject = JsonUtility.FromJson<SavedUpgradesJsonObject>(jsonStringArmour);
        this.playerUpgradesState.Armour = armourUpgradesObject.GetSavedUpgrades().OfType<ArmourUpgrade>().ToList()[0];

        string jsonStringEnergy = BuzzardGameData.EnergySystemsConfigFile.text;
        SavedUpgradesJsonObject energyUpgradesObject = JsonUtility.FromJson<SavedUpgradesJsonObject>(jsonStringEnergy);
        this.playerUpgradesState.EnergySystem =
            energyUpgradesObject.GetSavedUpgrades().OfType<EnergySystemsUpgrade>().ToList()[0];


        string jsonStringEngine = BuzzardGameData.EngineConfigFile.text;
        SavedUpgradesJsonObject engineUpgradesObject = JsonUtility.FromJson<SavedUpgradesJsonObject>(jsonStringEngine);
        this.playerUpgradesState.Engine = engineUpgradesObject.GetSavedUpgrades().OfType<EngineUpgrade>().ToList()[0];
        
        this.playerUpgradesState.SetupComplete();
    }
}
