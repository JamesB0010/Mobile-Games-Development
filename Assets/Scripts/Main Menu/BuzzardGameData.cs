using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Text;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

/// <summary>
/// This is present in the first scene and is not destroyed on load.
/// it should be assumed that an instance of this class will be available in all game scenes
/// as it will be created in the main menu
/// </summary>
public class BuzzardGameData 
{
    private BuzzardGameData()
    {
    }
    
   private void Init()
   { 
    this.SetupDependencies();
        
        BuzzardGameData.ReadLocalSaveFile();
        
        G_SaveGameInteractor.AddReadEventCallback(this.OnSavedGameRead);
        
        SceneManager.activeSceneChanged += ((sceneFrom, sceneTo) =>
        {
            this.SaveAllConfigFilesLocal();
#if UNITY_EDITOR
#else
            this.SaveLocalSaveGameToCloud();
#endif
        }); 
        
        
        #if UNITY_EDITOR
        EditorApplication.playModeStateChanged += change =>
        {
            if (change == PlayModeStateChange.ExitingPlayMode)
                instance = null;
        };
        #endif
    }
    private TextAsset armourConfigFile,
        energySystemConfigFile,
        engineConfigFile,
        heavyWeaponsConfigFile,
        lightWeaponsConfigFile,
        ownedUpgradesConfigFile;

    public static TextAsset ArmourConfigFile => Instance.armourConfigFile;

    public static TextAsset EnergySystemsConfigFile => Instance.energySystemConfigFile;

    public static TextAsset EngineConfigFile => Instance.engineConfigFile;

    public static TextAsset HeavyWeaponsConfigFile => Instance.heavyWeaponsConfigFile;

    public static TextAsset LightWeaponsConfigFile => Instance.lightWeaponsConfigFile;

    public static TextAsset OwnedUpgradesConfigFile => Instance.ownedUpgradesConfigFile;

    private FloatReference playerMoney;

    public static FloatReference PlayerMoney => Instance.playerMoney;

    private IntReference playerKills;

    public static IntReference PlayerKills => Instance.playerKills;

    [SerializeField] private SaveGameDebugInfo debugInfo;

    private static BuzzardGameData instance = null;

    private static BuzzardGameData Instance
    {
        get
        {
            if (instance != null)
                return instance;

            instance = new BuzzardGameData();
            instance.Init();
            return instance;
        }
    }

    private GameSaveData saveGameData;
    private IntReference gamesPlayed;
    private BoolReference gyroEnabled;
    public static BoolReference GyroEnabled => Instance.gyroEnabled;
    private BoolReference pitchInverted;
    public static BoolReference PitchInverted => Instance.pitchInverted;
    private ColorReference enemyOutlineColor;
    private FloatReference enemyOutlineWidth;

    public static FloatReference EnemyOutlineWidth => Instance.enemyOutlineWidth;
    private ColorReference primaryUiColor;
    private ColorReference tertiaryUiColor;
    private ColorReference secondaryUiColor;

    private string[] filePaths = new[]
    {
        Path.Combine("Json", "armourConfiguration.txt"),
        Path.Combine("Json", "energySystemConfiguration.txt"),
        Path.Combine("Json", "engineConfiguration.txt"),
        Path.Combine("Json", "heavyWeaponConfiguration.txt"),
        Path.Combine("Json", "lightWeaponConfiguration.txt"),
        Path.Combine("Json", "ownedUpgradesCounter.txt"),
        Path.Combine("Json", "Player Money.txt"),
        Path.Combine("Json", "EliminationCount.txt"),
        Path.Combine("Json", "Games Played.txt"),
        Path.Combine("Json", "GyroEnabled.txt"),
        Path.Combine("Json", "InvertPitch.txt"),
        Path.Combine("Json", "EnemyShipOutlineColor.txt"),
        Path.Combine("Json", "EnemyOutlineSize.txt"),
        Path.Combine("Json", "PrimaryUiColor.txt"),
        Path.Combine("Json", "Secondary Ui Color.txt"),
        Path.Combine("Json", "Tertiary Ui Color.txt")
    };

    private void SetupDependencies()
    {
        string dependencyDirectoryPath = Path.Combine(Application.persistentDataPath, "Json");
        bool jsonFolderExists = Directory.Exists(dependencyDirectoryPath);
        if(!jsonFolderExists)
            Directory.CreateDirectory(dependencyDirectoryPath);
        
        
        if (this.CheckAllDependenciesExist())
        {
            this.LoadDependencies();
        }
        else
        {
            if (jsonFolderExists)
            {
                //if the folder exists but not all the correct items were found in it then the folder has been corrupted and we need to regenerate the folder
                Directory.Delete(Path.Combine(Application.persistentDataPath, "Json"));
                Directory.CreateDirectory(dependencyDirectoryPath);
            }
            this.CreateDependentFiles();
        }
        
        this.debugInfo = GameObject.FindObjectOfType<SaveGameDebugInfo>();
    }


    private void LoadDependencies()
    {
        this.armourConfigFile = new TextAsset(File.ReadAllText(Path.Combine(Application.persistentDataPath, "Json", "armourConfiguration.txt")));
        this.energySystemConfigFile = new TextAsset(File.ReadAllText(Path.Combine(Application.persistentDataPath,"Json", "energySystemConfiguration.txt")));
        this.engineConfigFile = new TextAsset(File.ReadAllText(Path.Combine(Application.persistentDataPath,"Json", "engineConfiguration.txt")));
        this.heavyWeaponsConfigFile = new TextAsset(File.ReadAllText(Path.Combine(Application.persistentDataPath,"Json", "heavyWeaponConfiguration.txt")));
        this.lightWeaponsConfigFile = new TextAsset(File.ReadAllText(Path.Combine(Application.persistentDataPath,"Json", "lightWeaponConfiguration.txt")));
        this.ownedUpgradesConfigFile = new TextAsset(File.ReadAllText(Path.Combine(Application.persistentDataPath,"Json", "ownedUpgradesCounter.txt")));

        this.playerMoney = FloatReference.JSON.CreateFromFilepath(Path.Combine(Application.persistentDataPath, "Json", "Player Money.txt"));
        this.playerKills = IntReference.JSON.CreateFromFilepath(Path.Combine(Application.persistentDataPath, "Json", "EliminationCount.txt"));
        this.gamesPlayed = IntReference.JSON.CreateFromFilepath(Path.Combine(Application.persistentDataPath, "Json", "Games Played.txt"));
        this.gyroEnabled = BoolReference.JSON.CreateFromFilepath(Path.Combine(Application.persistentDataPath, "Json", "GyroEnabled.txt"));
        this.pitchInverted = BoolReference.JSON.CreateFromFilepath(Path.Combine(Application.persistentDataPath, "Json", "InvertPitch.txt"));
        this.enemyOutlineColor = ColorReference.JSON.CreateFromFilepath(Path.Combine(Application.persistentDataPath, "Json", "EnemyShipOutlineColor.txt"));
        this.enemyOutlineWidth = FloatReference.JSON.CreateFromFilepath(Path.Combine(Application.persistentDataPath, "Json", "EnemyOutlineSize.txt"));
        this.primaryUiColor = ColorReference.JSON.CreateFromFilepath(Path.Combine(Application.persistentDataPath, "Json", "PrimaryUiColor.txt"));
        this.secondaryUiColor = ColorReference.JSON.CreateFromFilepath(Path.Combine(Application.persistentDataPath, "Json", "Secondary Ui Color.txt"));
        this.tertiaryUiColor = ColorReference.JSON.CreateFromFilepath(Path.Combine(Application.persistentDataPath, "Json", "Tertiary Ui Color.txt"));
    }

    private void CreateDependentFiles()
    {
        this.armourConfigFile = new TextAsset("{\n    \"upgradeReferences\": [\n        \"DefaultArmour\"\n    ]\n}");
        this.energySystemConfigFile = new TextAsset("{\n    \"upgradeReferences\": [\n        \"DefaultEnergySystem\"\n    ]\n}");
        this.engineConfigFile = new TextAsset("{\n    \"upgradeReferences\": [\n        \"DefaultEngine\"\n    ]\n}");
        this.heavyWeaponsConfigFile = new TextAsset("{\n    \"upgradeReferences\": [\n        \"NoGun\",\n        \"NoGun\",\n        \"NoGun\",\n        \"NoGun\"\n    ]\n}");
        this.lightWeaponsConfigFile = new TextAsset("{\n    \"upgradeReferences\": [\n        \"DefaultAutocannon\",\n        \"DefaultAutocannon\",\n        \"NoLightGun\",\n        \"NoLightGun\",\n        \"NoLightGun\",\n        \"NoLightGun\",\n        \"NoLightGun\",\n        \"NoLightGun\"\n    ]\n}");
        this.ownedUpgradesConfigFile = new TextAsset("{\n    \"serializedDictionary\": [\n        {\n            \"key\": \"AmmoHighDamageHigh\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"AmmoHighDamageLow\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"AmmoLowDamageHigh\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"AmmoMediumDamageHigh\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"AmmoMediumDamageLow\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"DefaultArmour\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"DefaultAutocannon\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"DefaultEnergySystem\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"DefaultEngine\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"HeavyHighEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"HeavyLowEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"HighDamageHighEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"HighDamageLowEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"HighMaxHighRecharge\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"HighMaxLowRecharge\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"HighThrustHighEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"HighThrustHighEnergyBoost\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"HighThrustLowEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"LowDamageLowEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"LowMaxHighRecharge\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"LowThrustHighEnergyBoost\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"LowThrustLowEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"MediumDamageHighEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"MediumDamageLowEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"MediumHighEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"MediumLowEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"MediumMaxHighRecharge\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"MediumMaxLowRecharge\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"MediumThrustHighEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"MediumThrustHighEnergyBoost\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"MediumThrustLowEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"NoGun\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"NoLightGun\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"WeakLowEnergy\",\n            \"value\": 0\n        }\n    ]\n}");

        this.playerMoney = ScriptableObject.CreateInstance<FloatReference>();
        this.playerMoney.SetValue(0);

        this.playerKills = ScriptableObject.CreateInstance<IntReference>();
        this.playerKills.SetValue(0);

        this.gamesPlayed = ScriptableObject.CreateInstance<IntReference>();
        this.gamesPlayed.SetValue(0);

        this.gyroEnabled = ScriptableObject.CreateInstance<BoolReference>();
        this.gyroEnabled.SetValue(true);

        this.pitchInverted = ScriptableObject.CreateInstance<BoolReference>();
        this.pitchInverted.SetValue(true);

        this.enemyOutlineColor = ScriptableObject.CreateInstance<ColorReference>();
        this.enemyOutlineColor.SetValue(Color.red);

        this.enemyOutlineWidth = ScriptableObject.CreateInstance<FloatReference>();
        this.enemyOutlineWidth.SetValue(6);

        this.primaryUiColor = ScriptableObject.CreateInstance<ColorReference>();
        this.primaryUiColor.SetValue(Color.white);

        this.secondaryUiColor = ScriptableObject.CreateInstance<ColorReference>();
        this.secondaryUiColor.SetValue(new Color(0, 0.8862745098f, 0.09019607843f, 1));

        this.tertiaryUiColor = ScriptableObject.CreateInstance<ColorReference>();
        this.tertiaryUiColor.SetValue(Color.red);
        

        string[] fileContents = new string[]
        {
            this.armourConfigFile.text,
            this.energySystemConfigFile.text,
            this.engineConfigFile.text,
            this.heavyWeaponsConfigFile.text,
            this.lightWeaponsConfigFile.text,
            this.ownedUpgradesConfigFile.text,
            JsonUtility.ToJson(new FloatReference.JSON(this.playerMoney.GetValue())),
            JsonUtility.ToJson(new IntReference.JSON(this.playerKills.GetValue())), 
            JsonUtility.ToJson(new IntReference.JSON(this.gamesPlayed.GetValue())),
            JsonUtility.ToJson(new BoolReference.JSON(this.gyroEnabled.GetValue())),
            JsonUtility.ToJson(new BoolReference.JSON(this.pitchInverted.GetValue())),
            JsonUtility.ToJson(new ColorReference.JSON(this.enemyOutlineColor.GetValue())), 
            JsonUtility.ToJson(new FloatReference.JSON(this.enemyOutlineWidth.GetValue())), 
            JsonUtility.ToJson(new ColorReference.JSON(this.primaryUiColor.GetValue())), 
            JsonUtility.ToJson(new ColorReference.JSON(this.secondaryUiColor.GetValue())),
            JsonUtility.ToJson(new ColorReference.JSON(this.tertiaryUiColor.GetValue())) 
        };
        
        //save to files
        for(int i = 0; i < this.filePaths.Length; i++)
        {
            WriteTextAssetToNewFile(Path.Combine(Application.persistentDataPath, filePaths[i]), fileContents[i]);
        }
    }

    private static void WriteTextAssetToNewFile(string filepath, string content)
    {
        using FileStream fs = File.Create(filepath);
        fs.Write(UTF8Encoding.UTF8.GetBytes(content));
    }

    private bool CheckAllDependenciesExist()
    {
        bool result = true;

        foreach (string path in filePaths)
        {
            bool fileExists = File.Exists(Path.Combine(Application.persistentDataPath, path));
            result = result && fileExists;
        }

        return result;
    }

    private void OnDestroy()
    {
        G_SaveGameInteractor.RemoveReadEventCallback(this.OnSavedGameRead);
    }

    public static void ReadLocalSaveFile()
    {
        string saveGameFilePath = Path.Combine(Application.persistentDataPath, "Json", "SaveGame.txt");
        if(File.Exists(saveGameFilePath))
            Instance.OnSavedGameRead(File.ReadAllText(saveGameFilePath));
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
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "Json", "armourConfiguration.txt"), JsonUtility.ToJson(data.configs[0], true));
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "Json", "energySystemConfiguration.txt"), JsonUtility.ToJson(data.configs[1], true));
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "Json", "engineConfiguration.txt"), JsonUtility.ToJson(data.configs[2], true));
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "Json", "heavyWeaponConfiguration.txt"), JsonUtility.ToJson(data.configs[3], true));
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "Json", "lightWeaponConfiguration.txt"), JsonUtility.ToJson(data.configs[4], true));
        File.WriteAllText(Path.Combine(Application.persistentDataPath, "Json", "ownedUpgradesCounter.txt"), JsonUtility.ToJson(data.ownedUpgrades, true));
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
        string saveGameText = File.ReadAllText(Path.Combine(Application.persistentDataPath, "Json", "SaveGame.txt"));
        G_SaveGameInteractor.Instance.SaveGame(Encoding.UTF8.GetBytes(saveGameText), TimeSpan.Zero);
    }

    public static void Save()
    {
        Instance.SaveAllConfigFilesLocal();
#if UNITY_EDITOR
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
