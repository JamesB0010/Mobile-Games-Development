using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
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
    
    private GameSaveData saveGameData;
    private IntReference gamesPlayed;
    private BoolReference gyroEnabled;
    private BoolReference pitchInverted;
    private ColorReference enemyOutlineColor;
    private FloatReference enemyOutlineWidth;
    private ColorReference primaryUiColor;
    private ColorReference tertiaryUiColor;
    private ColorReference secondaryUiColor;
    

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
        
        BuzzardGameData.ReadLocalSaveFile();
        
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
        Debug.Log(this.CheckAllDependenciesExist());

        if (this.CheckAllDependenciesExist())
        {
            this.LoadDependencies();
        }
        else
        {
            this.CreateDependentFiles();
        }
        //Load Files
        this.armourConfigFile = Resources.Load<TextAsset>("Json/armourConfiguration");
        this.energySystemConfigFile = Resources.Load<TextAsset>("Json/energySystemConfiguration");
        this.engineConfigFile = Resources.Load<TextAsset>("Json/engineConfiguration");
        this.heavyWeaponsConfigFile = Resources.Load<TextAsset>("Json/heavyWeaponConfiguration");
        this.lightWeaponsConfigFile = Resources.Load<TextAsset>("Json/lightWeaponConfiguration");
        this.ownedUpgradesConfigFile = Resources.Load<TextAsset>("Json/ownedUpgradesCounter");
        
        //Setup Value References
        this.playerMoney = Resources.Load<FloatReference>("Json/Player Money");
        this.playerKills = Resources.Load<IntReference>("Json/EliminationCount");
        this.gamesPlayed = Resources.Load<IntReference>("Json/Games Played");
        this.gyroEnabled = Resources.Load<BoolReference>("Json/GyroEnabled");
        this.pitchInverted = Resources.Load<BoolReference>("Json/InvertPitch");
        this.enemyOutlineColor = Resources.Load<ColorReference>("Json/EnemyShipOutlineColor");
        this.enemyOutlineWidth = Resources.Load<FloatReference>("Json/EnemyOutlineSize");
        this.primaryUiColor = Resources.Load<ColorReference>("Json/PrimaryUiColor");
        this.secondaryUiColor = Resources.Load<ColorReference>("Json/Secondary Ui Color");
        this.tertiaryUiColor = Resources.Load<ColorReference>("Json/Tertiary Ui Color");
    }


    private void LoadDependencies()
    {
        this.armourConfigFile = new TextAsset(File.ReadAllText("Json/armourConfiguration.txt"));
        this.energySystemConfigFile = new TextAsset(File.ReadAllText("Json/energySystemConfiguration.txt"));
        this.engineConfigFile = new TextAsset(File.ReadAllText("Json/engineConfiguration.txt"));
        this.heavyWeaponsConfigFile = new TextAsset(File.ReadAllText("Json/heavyWeaponConfiguration.txt"));
        this.lightWeaponsConfigFile = new TextAsset("Json/lightWeaponConfiguration.txt");
        this.ownedUpgradesConfigFile = new TextAsset("Json/ownedUpgradesCounter.txt");
    }

    private void CreateDependentFiles()
    {
        throw new NotImplementedException();
    }
    private bool CheckAllDependenciesExist()
    {
        bool result = true;
        string[] filePaths = new[]
        {
            "Json/armourConfiguration.txt",
            "Json/energySystemConfiguration.txt",
            "Json/engineConfiguration.txt",
            "Json/heavyWeaponConfiguration.txt",
            "Json/lightWeaponConfiguration.txt",
            "Json/ownedUpgradesCounter.txt",
            "Json/Player Money.asset",
            "Json/EliminationCount.asset",
            "Json/Games Played.asset",
            "Json/GyroEnabled.asset",
            "Json/InvertPitch.asset",
            "Json/EnemyShipOutlineColor.asset",
            "Json/EnemyOutlineSize.asset",
            "Json/PrimaryUiColor.asset",
            "Json/Secondary Ui Color.asset",
            "Json/Tertiary Ui Color.asset"
        };

        foreach (string path in filePaths)
        {
            result = result && File.Exists(Path.Combine(Application.persistentDataPath, path));
        }

        return result;
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
        this.debugInfo?.SetDataString(data);
        
        if (data == "")
        {
            //First time reading the data create defaults
            byte[] initSaveFileContents = Encoding.UTF8.GetBytes(this.CollapseConfigFilesToString());
            G_SaveGameInteractor.Instance.SaveGame(initSaveFileContents, TimeSpan.Zero);
            this.OnSavedGameRead(Encoding.UTF8.GetString(initSaveFileContents));
        }
        else
        {
            //parse data and update config files
            this.saveGameData = JsonUtility.FromJson<GameSaveData>(data);
            
            __debug__UpdateUi(this.saveGameData);

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
        string jsonString = this.CollapseConfigFilesToString();
        var gameData = JsonUtility.FromJson<GameSaveData>(jsonString);
        
        this.SaveAllConfigFilesLocal(gameData);
    }

    private void SaveAllConfigFilesLocal(GameSaveData data)
    {
        //Save to the different text files
        File.WriteAllText(Application.dataPath + "/Resources/Json/armourConfiguration.txt", JsonUtility.ToJson(data.configs[0], true));
        File.WriteAllText(Application.dataPath + "/Resources/Json/energySystemConfiguration.txt", JsonUtility.ToJson(data.configs[1], true));
        File.WriteAllText(Application.dataPath + "/Resources/Json/engineConfiguration.txt", JsonUtility.ToJson(data.configs[2], true));
        File.WriteAllText(Application.dataPath + "/Resources/Json/heavyWeaponConfiguration.txt", JsonUtility.ToJson(data.configs[3], true));
        File.WriteAllText(Application.dataPath + "/Resources/Json/lightWeaponConfiguration.txt", JsonUtility.ToJson(data.configs[4], true));
        File.WriteAllText(Application.dataPath + "/Resources/Json/ownedUpgradesCounter.txt", JsonUtility.ToJson(data.ownedUpgrades, true));
        this.playerMoney.SetValue(data.playerMoney);
        this.playerKills.SetValue(data.playerKills);
        this.gamesPlayed.SetValue(data.gamesPlayed);
        this.gyroEnabled.SetValue(data.gyroEnabled);
        this.pitchInverted.SetValue(data.pitchInverted);
        this.enemyOutlineColor.SetValue(data.enemyOutlineColor.ToColor());
        this.enemyOutlineWidth.SetValue(data.enemyOutlineWidth);
        this.primaryUiColor.SetValue(data.primaryUiColor.ToColor());
        this.secondaryUiColor.SetValue(data.secondaryUiColor.ToColor());
        this.tertiaryUiColor.SetValue(data.tertiaryUiColor.ToColor());
        if (data.userSound != "noSoundRecorded")
        {
            WavUtility.CreateEmpty(AudioRecorder.GetFullRecordingFilepath()).Close();
            File.WriteAllBytes(AudioRecorder.GetFullRecordingFilepath(),WavUtility.StringFileContentsToBytes(data.userSound));
        }
        data.WriteToSaveGameJsonFile();
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
        output += "\"enemyOutlineColor\":" + JsonUtility.ToJson(new JsonColor(this.enemyOutlineColor.GetValue())) + ",";
        output += "\"enemyOutlineWidth\":" + this.enemyOutlineWidth.GetValue() + ",";
        output += "\"primaryUiColor\":" + JsonUtility.ToJson(new JsonColor(this.primaryUiColor.GetValue())) + ",";
        output += "\"secondaryUiColor\":" + JsonUtility.ToJson(new JsonColor(this.secondaryUiColor.GetValue())) + ",";
        output += "\"tertiaryUiColor\":" + JsonUtility.ToJson(new JsonColor(this.tertiaryUiColor.GetValue())) + ",";
        output += "\"userSound\": " + "\"" + sound + "\"";
        output += "}";
        return output;
    }
}
