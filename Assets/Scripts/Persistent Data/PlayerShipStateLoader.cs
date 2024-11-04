using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerShipStateLoader : MonoBehaviour
{
    [SerializeField] private TextAsset lightWeaponsJson;

    [SerializeField] private TextAsset heavyWeaponsJson;

    [SerializeField] private PlayerWeaponsState playerWeaponsState;

    void Start()
    {
        string jsonStringLight = this.lightWeaponsJson.text;
        SavedUpgradesJsonObject lightUpgradesObject = JsonUtility.FromJson<SavedUpgradesJsonObject>(jsonStringLight);

        this.playerWeaponsState.LightGuns = lightUpgradesObject.GetSavedGuns().OfType<LightGunUpgrade>().ToList();

        string jsonStringHeavy = this.heavyWeaponsJson.text;
        SavedUpgradesJsonObject heavyUpgradesObject = JsonUtility.FromJson<SavedUpgradesJsonObject>(jsonStringHeavy);
        this.playerWeaponsState.HeavyGuns = heavyUpgradesObject.GetSavedGuns().OfType<HeavyGunUpgrade>().ToList();
    }
}
