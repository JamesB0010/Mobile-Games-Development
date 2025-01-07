using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This is present in the first scene and is not destroyed on load.
/// it should be assumed that an instance of this class will be available in all game scenes
/// as it will be created in the main menu
/// </summary>
public class BuzzardGameData : MonoBehaviour
{
    private TextAsset armourConfigFile,
        energySystemConfigFile,
        engineConfigFile,
        heavyWeaponsConfigFile,
        lightWeaponsConfigFile,
        ownedUpgradesConfigFile;

    private FloatReference playerMoney;

    private IntReference playerKills;

    [SerializeField] private SaveGameDebugInfo debugInfo;

    private static BuzzardGameData instance = null;

    private static BuzzardGameData Instance
    {
        get
        {
            if (instance == null)
            {
                //create instance first 
                GameObject obj = new GameObject("BuzzardGameData");
                instance = obj.AddComponent<BuzzardGameData>();
                instance.SetupDependencies();
            }
            return instance;
        }
    }

    //has the save game been fetched yet?
    private bool saveGameFetched = false;
    
    private GameSaveData saveGameData;
    private IntReference gamesPlayed;
    private BoolReference gyroEnabled;
    private BoolReference pitchInverted;
    private ColorReference enemyOutlineColor;
    

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
        this.SetupDependencies();
        
        G_SaveGameInteractor.AddReadEventCallback(this.OnSavedGameRead);
        
        DontDestroyOnLoad(this.gameObject);

        SceneManager.activeSceneChanged += ((sceneFrom, sceneTo) =>
        {
            this.SaveAllConfigFilesLocal();
            #if UNITY_EDITOR
            AssetDatabase.Refresh();
            #else
            this.SaveLocalSaveGameToCloud();
            #endif
        });

    }

    private void SetupDependencies()
    {
        this.armourConfigFile = Resources.Load<TextAsset>("Json/armourConfiguration");
        this.energySystemConfigFile = Resources.Load<TextAsset>("Json/energySystemConfiguration");
        this.engineConfigFile = Resources.Load<TextAsset>("Json/engineConfiguration");
        this.heavyWeaponsConfigFile = Resources.Load<TextAsset>("Json/heavyWeaponConfiguration");
        this.lightWeaponsConfigFile = Resources.Load<TextAsset>("Json/lightWeaponConfiguration");
        this.ownedUpgradesConfigFile = Resources.Load<TextAsset>("Json/ownedUpgradesCounter");
        this.playerMoney = Resources.Load<FloatReference>("Json/Player Money");
        this.playerKills = Resources.Load<IntReference>("Json/EliminationCount");
        this.gamesPlayed = Resources.Load<IntReference>("Json/Games Played");
        this.gyroEnabled = Resources.Load<BoolReference>("Json/GyroEnabled");
        this.pitchInverted = Resources.Load<BoolReference>("Json/InvertPitch");
        this.enemyOutlineColor = Resources.Load<ColorReference>("Json/EnemyShipOutlineColor");
    }

    private void OnDestroy()
    {
        G_SaveGameInteractor.RemoveReadEventCallback(this.OnSavedGameRead);
    }

    public static void ReadLocalSaveFile()
    {
        if(File.Exists(Application.dataPath + "/Resources/Json/SaveGame.txt"))
            Instance.OnSavedGameRead(File.ReadAllText(Application.dataPath + "/Resources/Json/SaveGame.txt"));
    }

    public void OnSavedGameRead(string data)
    {
        this.saveGameFetched = true;
        
        this.debugInfo?.SetDataString(data);
        
        if (data == "")
        {
            //First time reading the data create defaults
            G_SaveGameInteractor.Instance.SaveGame(Encoding.UTF8.GetBytes(this.CollapseConfigFilesToString()), TimeSpan.Zero);
            BuzzardGameData.ReadSaveGame();
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
        this.debugInfo?.UpdateUi(saveGameData);
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
        this.playerKills.SetValue(saveGameData.playerKills);
        this.gamesPlayed.SetValue(saveGameData.gamesPlayed);
        this.gyroEnabled.SetValue(saveGameData.gyroEnabled);
        this.pitchInverted.SetValue(saveGameData.pitchInverted);
        this.enemyOutlineColor.SetValue(this.saveGameData.enemyOutlineColor);
        if (saveGameData.userSound != "noSoundRecorded")
        {
            WavUtility.CreateEmpty(AudioRecorder.GetFullRecordingFilepath()).Close();
            File.WriteAllBytes(AudioRecorder.GetFullRecordingFilepath(),WavUtility.StringFileContentsToBytes(saveGameData.userSound));
        }

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
        this.playerKills.SetValue(this.saveGameData.playerKills);
        this.gamesPlayed.SetValue(this.saveGameData.gamesPlayed);
        this.gyroEnabled.SetValue(this.saveGameData.gyroEnabled);
        this.pitchInverted.SetValue(this.saveGameData.pitchInverted);
        this.enemyOutlineColor.SetValue(this.saveGameData.enemyOutlineColor);
        if (saveGameData.userSound != "noSoundRecorded")
        {
            WavUtility.CreateEmpty(AudioRecorder.GetFullRecordingFilepath()).Close();
            File.WriteAllBytes(AudioRecorder.GetFullRecordingFilepath(),WavUtility.StringFileContentsToBytes(saveGameData.userSound));
        }
        saveGameData.WriteToSaveGameJsonFile();
    }

    private void SaveLocalSaveGameToCloud()
    {
        G_SaveGameInteractor.Instance.SaveGame(Encoding.UTF8.GetBytes(Resources.Load<TextAsset>("Json/SaveGame").text), TimeSpan.Zero);
    }

    public static void Save()
    {
        Instance.SaveAllConfigFilesLocal();
#if UNITY_EDITOR
        if (!Application.isPlaying)
        {
            DestroyImmediate(Instance.gameObject);
            instance = null;
        }
#else
        Instance.SaveLocalSaveGameToCloud();
#endif
    }

    //This is called once the user has been authenticated
    public static void ReadSaveGame()
    {
        G_SaveGameInteractor.Instance.ReadSavedGame();
    }

    private string CollapseConfigFilesToString()
    {
        
        bool userSubmittedSoundExists = File.Exists(AudioRecorder.GetFullRecordingFilepath());
        
        string sound = "noSoundRecorded";
        if (userSubmittedSoundExists)
        {
            sound = WavUtility.FilepathToBase64Contents(AudioRecorder.GetFullRecordingFilepath());
        }

        string output = "{\"configs\":[";
        output += this.armourConfigFile.text + ",";
        output += this.energySystemConfigFile.text + ",";
        output += this.engineConfigFile.text + ",";
        output += this.heavyWeaponsConfigFile.text + ",";
        output += lightWeaponsConfigFile.text + "], \n \"ownedUpgrades\": ";
        output += ownedUpgradesConfigFile.text + ",";
        output += "\"playerMoney\": " + this.playerMoney.GetValue() + ",";
        output += "\"playerKills\": " + this.playerKills.GetValue() + ",";
        output += "\"gamesPlayed\": " + this.gamesPlayed.GetValue() + ",";
        output += "\"gyroEnabled\": " + this.gyroEnabled.GetValue().ToString().ToLower() + ",";
        output += "\"pitchInverted\":" + this.pitchInverted.GetValue().ToString().ToLower() + ",";
        //todo work out serialisation of color to json
        output += "\"enemyOutlineColor\":" + this.enemyOutlineColor.GetValue() + ",";
        output += "\"userSound\": " + "\"" + sound + "\"";
        output += "}";
        return output;
    }
}
