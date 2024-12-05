using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;

public class SaveGameTest : MonoBehaviour
{
    [SerializeField] private SaveGameInteractor saveGameInteractor;

    [SerializeField] private TextAsset armourConfigFile,
        energySystemConfigFile,
        engineConfigFile,
        heavyWeaponsConfigFile,
        lightWeaponsConfigFile,
        ownedUpgradesConfigFile;

    [SerializeField] private FloatReference playerMoney;

    [SerializeField] private TextMeshProUGUI textElement;

    private void Start()
    {
        var saveGameData = JsonUtility.FromJson<GameSaveData>(this.CollapseConfigFilesToString());
        Debug.Log(saveGameData.configs);
        Debug.Log(saveGameData.ownedUpgrades);
        Debug.Log(saveGameData.playerMoney);
    }

    public void OnSavedGameRead(string data)
    {
        textElement.text = data;
        if (data == "")
        {
            //First time reading the data create defaults
            saveGameInteractor.SaveGame(Encoding.UTF8.GetBytes(this.CollapseConfigFilesToString()), TimeSpan.Zero);
        }
        else
        {
            //parse data and update config files
        }
    }

    public void TestSaveGame()
    {
        saveGameInteractor.SaveGame(Encoding.UTF8.GetBytes(this.CollapseConfigFilesToString()), TimeSpan.Zero);
        saveGameInteractor.ReadSavedGame();
    }

    private string CollapseConfigFilesToString()
    {
        string output = "{\"configs\":[";
        output += this.armourConfigFile.text + ",";
        output += this.energySystemConfigFile.text + ",";
        output += this.engineConfigFile.text + ",";
        output += this.heavyWeaponsConfigFile.text + ",";
        output += lightWeaponsConfigFile.text + "], \n \"ownedUpgrades\": ";
        output += ownedUpgradesConfigFile.text + ",";
        output += "\"playerMoney\": " + this.playerMoney.GetValue();
        output += "}";
        Debug.Log(output);
        return output;
    }
}
