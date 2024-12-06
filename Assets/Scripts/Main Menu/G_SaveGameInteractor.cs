using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.Events;

public class G_SaveGameInteractor 
{
    //Statics
    private static G_SaveGameInteractor instance = null;

    public static G_SaveGameInteractor Instance
    {
        get
        {
            if (!PlayGamesPlatform.Instance.IsAuthenticated())
                throw new Exception("User not authenticated cannot create save game interactor");   
            
            return instance ??= new G_SaveGameInteractor();
        }
    }

    //this is used to hold callbacks if our instance doesnt exist yet.
    //we only want to make this instance using CreateInstance which is only called after our user has been authenticated
    private static List<Action<string>> readEventCallbackWaitingList;
    
    public static void AddReadEventCallback(Action<string> callback)
    {
        if (instance != null)
        {
            instance.SavedGameReadEvent += callback;
        }
        else
        {
            readEventCallbackWaitingList.Add(callback);
        }
    }

    public static void RemoveReadEventCallback(Action<string> callback)
    {
        if (instance == null)
            return;

        instance.SavedGameReadEvent -= callback;
    }
    
    
    
    
    
    
    
    //members
    private event Action<string> SavedGameReadEvent;
    private G_SaveGameInteractor()
    {
        AddListenersFromWaitingList();
    }

    private void AddListenersFromWaitingList()
    {
        for (int i = 0; i < readEventCallbackWaitingList.Count; ++i)
        {
            this.SavedGameReadEvent += readEventCallbackWaitingList[i];
        }

        readEventCallbackWaitingList.Clear();
    }

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
    }
}
