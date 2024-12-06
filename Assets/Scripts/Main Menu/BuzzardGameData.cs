using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// This is present in the first scene and is not destroyed on load.
/// it should be assumed that an instance of this class will be available in all game scenes
/// as it will be created in the main menu
/// </summary>
public class BuzzardGameData : MonoBehaviour
{
    [SerializeField] private TextAsset armourConfigFile,
        energySystemConfigFile,
        engineConfigFile,
        heavyWeaponsConfigFile,
        lightWeaponsConfigFile,
        ownedUpgradesConfigFile;

    [SerializeField] private FloatReference playerMoney;

    [SerializeField] private TextMeshProUGUI textElement, working1, working2, working3;

    private static BuzzardGameData instance = null;

    //has the save game been fetched yet?
    private bool saveGameFetched = false;
    
    private GameSaveData saveGameData;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Start()
    {
        G_SaveGameInteractor.AddReadEventCallback(this.OnSavedGameRead);
        
        DontDestroyOnLoad(this.gameObject);

        SceneManager.activeSceneChanged += ((sceneFrom, sceneTo) =>
        {
            this.SaveAllConfigFilesLocal();
            this.SaveLocalSaveGameToCloud();
        });

    }

    private void OnDestroy()
    {
        G_SaveGameInteractor.RemoveReadEventCallback(this.OnSavedGameRead);
    }

    public void OnSavedGameRead(string data)
    {
        this.saveGameFetched = true;
        
        textElement.text = data;
        if (data == "")
        {
            //First time reading the data create defaults
            G_SaveGameInteractor.Instance.SaveGame(Encoding.UTF8.GetBytes(this.CollapseConfigFilesToString()), TimeSpan.Zero);
        }
        else
        {
            //parse data and update config files
            saveGameData = JsonUtility.FromJson<GameSaveData>(data);
            
            __debug__UpdateUi(saveGameData);

            //Save to the different text files
            this.SaveAllConfigFilesLocal(saveGameData);
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

    public void SaveAllConfigFilesLocal()
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
    }

    private void SaveAllConfigFilesLocal(GameSaveData saveGameData)
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
    }

    private void SaveLocalSaveGameToCloud()
    {
        G_SaveGameInteractor.Instance.SaveGame(Encoding.UTF8.GetBytes(Resources.Load<TextAsset>("Json/SaveGame").text), TimeSpan.Zero);
    }

    public void Save()
    {
        this.SaveAllConfigFilesLocal();
        this.SaveLocalSaveGameToCloud();
    }

    //This is called once the user has been authenticated
    public void ReadSaveGame()
    {
        G_SaveGameInteractor.Instance.ReadSavedGame();
    }

    public float GetPlayerMoney()
    {
        return this.saveGameData.playerMoney;
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
