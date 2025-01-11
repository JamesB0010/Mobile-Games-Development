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
   public JsonColor enemyOutlineColor;
   public float enemyOutlineWidth;
   public JsonColor primaryUiColor;
   public JsonColor secondaryUiColor;
   public JsonColor tertiaryUiColor;
   public bool simpleControls;
   public string userSound;

   public void WriteToSaveGameJsonFile()
   {
      File.WriteAllText(Path.Combine(Application.persistentDataPath, "Json", "SaveGame.txt"), JsonUtility.ToJson(this, true));
   }
}
