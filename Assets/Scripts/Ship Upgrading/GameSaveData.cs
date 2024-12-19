using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[Serializable]
public class GameSaveData 
{
   public List<SavedUpgradesJsonObject> configs;
   public UpgradesCounterJsonObject ownedUpgrades;
   public int playerMoney;
   public int playerKills;
   public int gamesPlayed;
   public bool gyroEnabled;
   public bool pitchInverted;
   public string userSound;

   public void WriteToSaveGameJsonFile()
   {
      bool fileExists = File.Exists(Application.dataPath + "/Resources/Json/SaveGame.txt");
      File.WriteAllText(Application.dataPath + "/Resources/Json/SaveGame.txt", JsonUtility.ToJson(this, true));
      #if UNITY_EDITOR
      if(fileExists)
         AssetDatabase.SaveAssetIfDirty(Resources.Load<TextAsset>("Json/SaveGame"));
      #endif
   }
}
