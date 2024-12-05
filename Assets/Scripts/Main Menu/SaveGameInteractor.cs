using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using UnityEngine;
using UnityEngine.Events;

public class SaveGameInteractor : MonoBehaviour
{
    public UnityEvent<string> SavedGameReadEvent = new UnityEvent<string>();
    public void ReadSavedGame()
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution("PlayerSaveData", DataSource.ReadCacheOrNetwork,ConflictResolutionStrategy.UseLongestPlaytime,  this.OnSavedGameOpened);
    }
    
    private void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            this.LoadGameData(game);
        }
        else
        {
        }
    }

    private void LoadGameData(ISavedGameMetadata game)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(game, this.OnSavedGameDataRead);
    }

    private void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            //handle processing the byte array data
            this.SavedGameReadEvent?.Invoke(Encoding.UTF8.GetString(data));
        }
        else
        {
        }
    }

    public void SaveGame(byte[] savedData, TimeSpan totalPlaytime)
    {
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution("PlayerSaveData", DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime, (
                (status, game) =>
                {
                    SavedGameMetadataUpdate.Builder builder = new();
                    builder = builder
                        .WithUpdatedPlayedTime(totalPlaytime)
                        .WithUpdatedDescription("Saved game at " + DateTime.UtcNow);

                    SavedGameMetadataUpdate updatedMetadata = builder.Build();
                    savedGameClient.CommitUpdate(game, updatedMetadata, savedData, this.OnSavedGameWritten);
                }));
    }

    private void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            //handle writing of saved game
        }
        else
        {
            //handle error
        }
    }
}
