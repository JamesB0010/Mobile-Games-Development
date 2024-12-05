using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameSaveData : MonoBehaviour
{
   public List<SavedUpgradesJsonObject> configs;
   public UpgradesCounterJsonObject ownedUpgrades;
   public int playerMoney;
}
