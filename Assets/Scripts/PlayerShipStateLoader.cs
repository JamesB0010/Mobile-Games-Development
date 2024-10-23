using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;


struct SavedLightWeaponsJsonObject
{
    public List<string> lightGunReferences;
    public SavedLightWeaponsJsonObject(List<Gun> guns)
    {
        this.lightGunReferences = new List<string>();

        foreach (var gun in guns)
        {
            this.lightGunReferences.Add(gun.name);
        }
    }
    

    public List<Gun> GetSavedGuns()
    {
        List<Gun> upgrades = new List<Gun>();


        for (int i = 0; i < this.lightGunReferences.Count; i++)
        {
            object weaponUpgrade = Resources.Load("Guns/" + this.lightGunReferences[i]);

            upgrades.Add((Gun)weaponUpgrade);
        }
        
        return upgrades;
    }
}

public class PlayerShipStateLoader : MonoBehaviour
{
    [SerializeField] private TextAsset lightWeaponsJson;

    [SerializeField] private PlayerWeaponsState playerWeaponsState;
    
    void Start()
    {
        string jsonString = this.lightWeaponsJson.text;
        SavedLightWeaponsJsonObject lightWeaponsObject = JsonUtility.FromJson<SavedLightWeaponsJsonObject>(jsonString);

        this.playerWeaponsState.Guns = lightWeaponsObject.GetSavedGuns();
    }
}
