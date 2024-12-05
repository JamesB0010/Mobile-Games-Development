using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class SaveGameTest : MonoBehaviour
{
    [SerializeField] private SaveGameInteractor saveGameInteractor;


    public void TestSaveGame()
    {
        saveGameInteractor.SaveGame(Encoding.ASCII.GetBytes("Test Save Data"), TimeSpan.Zero);
        saveGameInteractor.ReadSavedGame();
    }
}
