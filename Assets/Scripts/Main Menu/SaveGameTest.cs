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

    [SerializeField] private TextMeshProUGUI textElement, working1, working2, working3;

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
            var saveGameData = JsonUtility.FromJson<GameSaveData>(data);
            
            if (saveGameData.configs != null)
            {
                this.working1.gameObject.SetActive(true);
            }

            if (saveGameData.ownedUpgrades != null)
            {
                this.working2.gameObject.SetActive(true);
            }

            if (saveGameData.playerMoney == 0)
            {
                this.working3.gameObject.SetActive(true);
            }
        }
    }

    public void ReadSaveData()
    {
        saveGameInteractor.ReadSavedGame();
    }
    public void SaveCollapsedConfigFiles()
    {
        var saveGameData = JsonUtility.FromJson<GameSaveData>(this.CollapseConfigFilesToString());
        Debug.Log(saveGameData.configs);
        Debug.Log(saveGameData.ownedUpgrades);
        Debug.Log(saveGameData.playerMoney);
        
        saveGameData.WriteToSaveGameJsonFile();
        
        saveGameInteractor.SaveGame(Encoding.UTF8.GetBytes(Resources.Load<TextAsset>("Json/SaveGame").text),TimeSpan.Zero);
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
        return output;
    }
}
