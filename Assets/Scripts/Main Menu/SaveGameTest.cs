using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
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
            
            __debug__UpdateUi(saveGameData);

            //Save to the different text files
            this.SaveAllConfigFiles(saveGameData);
        }
    }

    private void __debug__UpdateUi(GameSaveData saveGameData)
    {
        if (saveGameData.configs[0].upgradeReferences != null)
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

    public void ReadSaveData()
    {
        saveGameInteractor.ReadSavedGame();
    }
    public void SaveAllConfigFiles()
    {
        var saveGameData = JsonUtility.FromJson<GameSaveData>(this.CollapseConfigFilesToString());
        //Save to the different text files
        File.WriteAllText(Application.dataPath + "/Resources/Json/armourConfiguration.txt", JsonUtility.ToJson(saveGameData.configs[0], true));
        File.WriteAllText(Application.dataPath + "/Resources/Json/energySystemConfiguration.txt",JsonUtility.ToJson(saveGameData.configs[1], true));
        File.WriteAllText(Application.dataPath + "/Resources/Json/engineConfiguration.txt", JsonUtility.ToJson(saveGameData.configs[2], true));
        File.WriteAllText(Application.dataPath + "/Resources/Json/heavyWeaponConfiguration.txt", JsonUtility.ToJson(saveGameData.configs[3], true));
        File.WriteAllText(Application.dataPath + "/Resources/Json/lightWeaponConfiguration.txt", JsonUtility.ToJson(saveGameData.configs[4], true));
        File.WriteAllText(Application.dataPath + "/Resources/Json/ownedUpgradesCounter.txt",JsonUtility.ToJson(saveGameData.ownedUpgrades, true));
        this.playerMoney.SetValue(saveGameData.playerMoney);
        saveGameData.WriteToSaveGameJsonFile();

        saveGameInteractor.SaveGame(Encoding.UTF8.GetBytes(Resources.Load<TextAsset>("Json/SaveGame").text),TimeSpan.Zero);
    }

    public void SaveAllConfigFiles(GameSaveData saveGameData)
    {
        //Save to the different text files
        File.WriteAllText(Application.dataPath + "/Resources/Json/armourConfiguration.txt", JsonUtility.ToJson(saveGameData.configs[0], true));
        File.WriteAllText(Application.dataPath + "/Resources/Json/energySystemConfiguration.txt", JsonUtility.ToJson(saveGameData.configs[1], true));
        File.WriteAllText(Application.dataPath + "/Resources/Json/engineConfiguration.txt", JsonUtility.ToJson(saveGameData.configs[2], true));
        File.WriteAllText(Application.dataPath + "/Resources/Json/heavyWeaponConfiguration.txt", JsonUtility.ToJson(saveGameData.configs[3], true));
        File.WriteAllText(Application.dataPath + "/Resources/Json/lightWeaponConfiguration.txt", JsonUtility.ToJson(saveGameData.configs[4], true));
        File.WriteAllText(Application.dataPath + "/Resources/Json/ownedUpgradesCounter.txt", JsonUtility.ToJson(saveGameData.ownedUpgrades, true));
        this.playerMoney.SetValue(saveGameData.playerMoney);
        saveGameData.WriteToSaveGameJsonFile();

        saveGameInteractor.SaveGame(Encoding.UTF8.GetBytes(Resources.Load<TextAsset>("Json/SaveGame").text), TimeSpan.Zero);
    }

    //This is called once the user has been authenticated
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
