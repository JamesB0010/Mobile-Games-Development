using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerShipStateLoader : MonoBehaviour
{
    [SerializeField] private TextAsset lightWeaponsJson;

    [SerializeField] private PlayerWeaponsState playerWeaponsState;

    void Start()
    {
        string jsonString = this.lightWeaponsJson.text;
        SavedLightWeaponsJsonObject lightWeaponsObject = JsonUtility.FromJson<SavedLightWeaponsJsonObject>(jsonString);

        this.playerWeaponsState.LightGuns = lightWeaponsObject.GetSavedGuns();
    }
}
