using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(UsefulPlayerComponents))]
public class LinkToPlayerEventsProxy : MonoBehaviour
{
    private UsefulPlayerComponents usefulPlayerComponents;
    void Start()
    {
        this.usefulPlayerComponents = GetComponent<UsefulPlayerComponents>();
        PlayerEventsProxy playerEventsProxy = FindObjectOfType<PlayerEventsProxy>();

        if (playerEventsProxy == null)
            return;


        this.LinkEvents(playerEventsProxy);
    }

    private void LinkEvents(PlayerEventsProxy playerEventsProxy)
    {
        this.LinkEnemyDeathEvent(playerEventsProxy);
        this.LinkUserSubmittedSoundLoader(playerEventsProxy);
        this.LinkPlayerOutOfBounds(playerEventsProxy);
        this.LinkPlayerReturnedToBounds(playerEventsProxy);
    }
    private void LinkEnemyDeathEvent(PlayerEventsProxy playerEventsProxy)
    {
        playerEventsProxy.EnemyDeathEventVoid.AddListener(usefulPlayerComponents.PlayerWallet.OnEnemyKilled);
        playerEventsProxy.EnemyDeathEventVoid.AddListener(usefulPlayerComponents.RightMonitorAnimPlayExposer.Play);
        playerEventsProxy.EnemyDeathEvent.AddListener(usefulPlayerComponents.PlayerHapticFeedback.OnEnemyKilled);
    }
    private void LinkUserSubmittedSoundLoader(PlayerEventsProxy playerEventsProxy)
    {
        playerEventsProxy.EventUserSubmittedSoundLoaderStatusUpdateEvent.AddListener(this.usefulPlayerComponents.SubtitleSetter.OnUserSubmittedSoundLoader);
    }
    private void LinkPlayerOutOfBounds(PlayerEventsProxy playerEventsProxy)
    {
        playerEventsProxy.PlayerOutOfBoundsEvent.AddListener(this.usefulPlayerComponents.OutOfBoundsUiElements.PlayerExitedBounds);
    }
    private void LinkPlayerReturnedToBounds(PlayerEventsProxy playerEventsProxy)
    {
        playerEventsProxy.PlayerReturnedToBoundsEvent.AddListener(this.usefulPlayerComponents.OutOfBoundsUiElements.PlayerBackInBounds);
    }
}
