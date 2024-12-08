using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SaveGameDebugInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI saveGameContentsText, working1, working2, working3, playerMoney;

    private string dataStrinig;

    public void SetDataString(string data)
    {
        this.dataStrinig = data;
    }

    public void SaveGame()
    {
        BuzzardGameData.Save();
    }

    public void LoadGame()
    {
        #if UNITY_EDITOR
        return;
        #endif
        BuzzardGameData.ReadSaveGame();
    }
    
    public void UpdateUi(GameSaveData saveGameData)
    {
        saveGameContentsText.text = dataStrinig;

        if (saveGameData.configs[0].upgradeReferences != null)
        {
            this.working1.gameObject.SetActive(true);
        }

        if (saveGameData.ownedUpgrades != null)
        {
            this.working2.gameObject.SetActive(true);
        }

        if (saveGameData.playerMoney != null)
        {
            this.working3.gameObject.SetActive(true);
            this.playerMoney.text = $"Player Money: {saveGameData.playerMoney}";
        }
    }
}
