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
    private BoolReference simpleControls;
    public static BoolReference SimpleControls => Instance.simpleControls;

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
        Path.Combine("Json", "Tertiary Ui Color.txt"),
        Path.Combine("Json", "Simple Controls.txt")
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
        this.armourConfigFile.name = "armourConfiguration";
        
        this.energySystemConfigFile = new TextAsset(File.ReadAllText(Path.Combine(Application.persistentDataPath,"Json", "energySystemConfiguration.txt")));
        this.energySystemConfigFile.name = "energySystemConfiguration";
        
        this.engineConfigFile = new TextAsset(File.ReadAllText(Path.Combine(Application.persistentDataPath,"Json", "engineConfiguration.txt")));
        this.engineConfigFile.name = "engineConfiguration";
        
        this.heavyWeaponsConfigFile = new TextAsset(File.ReadAllText(Path.Combine(Application.persistentDataPath,"Json", "heavyWeaponConfiguration.txt")));
        this.heavyWeaponsConfigFile.name = "heavyWeaponConfiguration";
        
        this.lightWeaponsConfigFile = new TextAsset(File.ReadAllText(Path.Combine(Application.persistentDataPath,"Json", "lightWeaponConfiguration.txt")));
        this.lightWeaponsConfigFile.name = "lightWeaponConfiguration";
        
        this.ownedUpgradesConfigFile = new TextAsset(File.ReadAllText(Path.Combine(Application.persistentDataPath,"Json", "ownedUpgradesCounter.txt")));
        this.ownedUpgradesConfigFile.name = "ownedUpgradesCounter";
        
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
        this.simpleControls = Resources.Load<BoolReference>("Json/Simple Controls");

        this.playerMoney.SetValue(new FloatReference.JSON(Path.Combine(Application.persistentDataPath, "Json", "Player Money.txt")));
        this.playerKills.SetValue(new IntReference.JSON(Path.Combine(Application.persistentDataPath, "Json", "EliminationCount.txt")));
        this.gamesPlayed.SetValue(new IntReference.JSON(Path.Combine(Application.persistentDataPath, "Json", "Games Played.txt")));
        this.gyroEnabled.SetValue(new BoolReference.JSON(Path.Combine(Application.persistentDataPath, "Json", "GyroEnabled.txt")));
        this.pitchInverted.SetValue(new BoolReference.JSON(Path.Combine(Application.persistentDataPath, "Json", "InvertPitch.txt")));
        this.enemyOutlineColor.SetValue(new ColorReference.JSON(Path.Combine(Application.persistentDataPath, "Json", "EnemyShipOutlineColor.txt")));
        this.enemyOutlineWidth.SetValue(new FloatReference.JSON(Path.Combine(Application.persistentDataPath, "Json", "EnemyOutlineSize.txt")));
        this.primaryUiColor.SetValue(new ColorReference.JSON(Path.Combine(Application.persistentDataPath, "Json", "PrimaryUiColor.txt")));
        this.secondaryUiColor.SetValue(new ColorReference.JSON(Path.Combine(Application.persistentDataPath, "Json", "Secondary Ui Color.txt")));
        this.tertiaryUiColor.SetValue(new ColorReference.JSON(Path.Combine(Application.persistentDataPath, "Json", "Tertiary Ui Color.txt")));
        this.simpleControls.SetValue(new BoolReference.JSON(Path.Combine(Application.persistentDataPath, "Json", "Simple Controls.txt")));
    }

    private void CreateDependentFiles()
    {
        this.armourConfigFile = new TextAsset("{\n    \"upgradeReferences\": [\n        \"DefaultArmour\"\n    ]\n}");
        this.armourConfigFile.name = "armourConfiguration";
        
        this.energySystemConfigFile = new TextAsset("{\n    \"upgradeReferences\": [\n        \"DefaultEnergySystem\"\n    ]\n}");
        this.energySystemConfigFile.name = "energySystemConfiguration";
            
        this.engineConfigFile = new TextAsset("{\n    \"upgradeReferences\": [\n        \"DefaultEngine\"\n    ]\n}");
        this.engineConfigFile.name = "engineConfiguration";
            
        this.heavyWeaponsConfigFile = new TextAsset("{\n    \"upgradeReferences\": [\n        \"NoGun\",\n        \"NoGun\",\n        \"NoGun\",\n        \"NoGun\"\n    ]\n}");
        this.heavyWeaponsConfigFile.name = "heavyWeaponConfiguration";
            
        this.lightWeaponsConfigFile = new TextAsset("{\n    \"upgradeReferences\": [\n        \"DefaultAutocannon\",\n        \"DefaultAutocannon\",\n        \"NoLightGun\",\n        \"NoLightGun\",\n        \"NoLightGun\",\n        \"NoLightGun\",\n        \"NoLightGun\",\n        \"NoLightGun\"\n    ]\n}");
        this.lightWeaponsConfigFile.name = "lightWeaponConfiguration";
            
        this.ownedUpgradesConfigFile = new TextAsset("{\n    \"serializedDictionary\": [\n        {\n            \"key\": \"AmmoHighDamageHigh\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"AmmoHighDamageLow\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"AmmoLowDamageHigh\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"AmmoMediumDamageHigh\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"AmmoMediumDamageLow\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"DefaultArmour\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"DefaultAutocannon\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"DefaultEnergySystem\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"DefaultEngine\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"HeavyHighEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"HeavyLowEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"HighDamageHighEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"HighDamageLowEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"HighMaxHighRecharge\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"HighMaxLowRecharge\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"HighThrustHighEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"HighThrustHighEnergyBoost\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"HighThrustLowEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"LowDamageLowEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"LowMaxHighRecharge\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"LowThrustHighEnergyBoost\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"LowThrustLowEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"MediumDamageHighEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"MediumDamageLowEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"MediumHighEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"MediumLowEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"MediumMaxHighRecharge\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"MediumMaxLowRecharge\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"MediumThrustHighEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"MediumThrustHighEnergyBoost\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"MediumThrustLowEnergy\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"NoGun\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"NoLightGun\",\n            \"value\": 0\n        },\n        {\n            \"key\": \"WeakLowEnergy\",\n            \"value\": 0\n        }\n    ]\n}");
        this.ownedUpgradesConfigFile.name = "ownedUpgradesCounter";

        this.playerMoney = Resources.Load<FloatReference>("Json/Player Money");
        this.playerMoney.SetValue(0);

        this.playerKills = Resources.Load<IntReference>("Json/EliminationCount");
        this.playerKills.SetValue(0);

        this.gamesPlayed = Resources.Load<IntReference>("Json/Games Played");
        this.gamesPlayed.SetValue(0);

        this.gyroEnabled = Resources.Load<BoolReference>("Json/GyroEnabled");
        this.gyroEnabled.SetValue(true);

        this.pitchInverted = Resources.Load<BoolReference>("Json/InvertPitch");
        this.pitchInverted.SetValue(true);

        this.enemyOutlineColor = Resources.Load<ColorReference>("Json/EnemyShipOutlineColor");
        this.enemyOutlineColor.SetValue(Color.red);

        this.enemyOutlineWidth = Resources.Load<FloatReference>("Json/EnemyOutlineSize");
        this.enemyOutlineWidth.SetValue(6);

        this.primaryUiColor = Resources.Load<ColorReference>("Json/PrimaryUiColor");
        this.primaryUiColor.SetValue(Color.white);

        this.secondaryUiColor = Resources.Load<ColorReference>("Json/Secondary Ui Color"); 
        this.secondaryUiColor.SetValue(new Color(0, 0.8862745098f, 0.09019607843f, 1));

        this.tertiaryUiColor = Resources.Load<ColorReference>("Json/Tertiary Ui Color");
        this.tertiaryUiColor.SetValue(Color.red);


        this.simpleControls = Resources.Load<BoolReference>("Json/Simple Controls");
        this.simpleControls.SetValue(false);
        

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
            JsonUtility.ToJson(new ColorReference.JSON(this.tertiaryUiColor.GetValue())),
            JsonUtility.ToJson(new BoolReference.JSON(this.simpleControls.GetValue()))
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
        this.simpleControls.SetValue(data.simpleControls);
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
        output += "\"simpleControls\":" + this.simpleControls.GetValue().ToString().ToLower() + ",";
        output += "\"userSound\": " + "\"" + sound + "\"";
        output += "}";
        return output;
    }

    public static void ReloadTextFiles()
    {
        Instance.armourConfigFile = new TextAsset(File.ReadAllText(Path.Combine(Application.persistentDataPath, "Json", "armourConfiguration.txt")));
        Instance.armourConfigFile.name = "armourConfiguration";

        Instance.energySystemConfigFile = new TextAsset(File.ReadAllText(Path.Combine(Application.persistentDataPath, "Json", "energySystemConfiguration.txt")));
        Instance.energySystemConfigFile.name = "energySystemConfiguration";

        Instance.engineConfigFile = new TextAsset(File.ReadAllText(Path.Combine(Application.persistentDataPath, "Json", "engineConfiguration.txt")));
        Instance.engineConfigFile.name = "engineConfiguration";

        Instance.heavyWeaponsConfigFile = new TextAsset(File.ReadAllText(Path.Combine(Application.persistentDataPath, "Json", "heavyWeaponConfiguration.txt")));
        Instance.heavyWeaponsConfigFile.name = "heavyWeaponConfiguration";

        Instance.lightWeaponsConfigFile = new TextAsset(File.ReadAllText(Path.Combine(Application.persistentDataPath, "Json", "lightWeaponConfiguration.txt")));
        Instance.lightWeaponsConfigFile.name = "lightWeaponConfiguration";

        Instance.ownedUpgradesConfigFile = new TextAsset(File.ReadAllText(Path.Combine(Application.persistentDataPath, "Json", "ownedUpgradesCounter.txt")));
        Instance.ownedUpgradesConfigFile.name = "ownedUpgradesCounter";
    }
}
