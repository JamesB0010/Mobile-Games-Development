using System.Collections;
using System.Collections.Generic;
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
        SavedWeaponsJsonObject lightWeaponsObject = JsonUtility.FromJson<SavedWeaponsJsonObject>(jsonStringLight);

        this.playerWeaponsState.LightGuns = lightWeaponsObject.GetSavedGuns();

        string jsonStringHeavy = this.heavyWeaponsJson.text;
        SavedWeaponsJsonObject heavyWeaponsObject = JsonUtility.FromJson<SavedWeaponsJsonObject>(jsonStringHeavy);
        this.playerWeaponsState.HeavyGuns = heavyWeaponsObject.GetSavedGuns();
    }
}
