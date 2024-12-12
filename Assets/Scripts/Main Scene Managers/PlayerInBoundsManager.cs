using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class PlayerInBoundsManager : MonoBehaviour
{
    [SerializeField] private Transform top, bottom, forward, back, left, right;

    private Transform player;

    [SerializeField] private UnityEvent<FloatLerpPackage> outOfBounds = new UnityEvent<FloatLerpPackage>();

    [SerializeField] private UnityEvent outOfBoundsTimerAlmostUp = new UnityEvent();

    [SerializeField] private UnityEvent returnedToBounds = new UnityEvent();

    private FloatLerpPackage outOfBoundsTimeout;

    private bool playerInBounds = true;
    private bool almostTimeUpInvoked = false;

    public void SetPlayerTransform(GameObject topLevelPlayerObject)
    {
        //get the part of the player which actually moves around
        Transform movingPart = topLevelPlayerObject.transform.GetChild(0);

        this.player = movingPart;

        this.enabled = true;
    }
    
    private bool PlayerInBounds
    {
        get => this.playerInBounds;

        set
        {
            if (this.playerInBounds == value)
                return;
            
            
            bool playerLeftBounds = this.playerInBounds == true && value == false;
            if (playerLeftBounds)
            {
                this.PlayerOutOfBounds();
                this.outOfBounds?.Invoke(this.outOfBoundsTimeout);
            }

            bool playerReEnteredBounds = this.playerInBounds == false && value == true;
            if (playerReEnteredBounds)
            {
                this.playerReturnedToBounds();
                this.returnedToBounds?.Invoke();
            }
            
            this.playerInBounds = value;
        }
    }

    private void playerReturnedToBounds()
    {
        if (this.outOfBoundsTimeout?.currentPercentage != 1.0f)
        {
            GlobalLerpProcessor.RemovePackage(this.outOfBoundsTimeout);
        }

        this.almostTimeUpInvoked = false;
    }

    private void PlayerOutOfBounds()
    {
        this.outOfBoundsTimeout = 0.0f.LerpTo(1, 20, value =>
        {
            if (value > 0.8f && almostTimeUpInvoked == false)
            {
                this.outOfBoundsTimerAlmostUp?.Invoke();
                this.almostTimeUpInvoked = true;
            }
        }, pkg =>
        {
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        });
    }

    private void Update()
    {
        this.PlayerInBounds = this.CheckBoundaries();
    }

    private bool CheckBoundaries()
    {
        bool withinTop = !(this.player.position.y > this.top.position.y);
        bool withinBottom = !(this.player.position.y < this.bottom.position.y);
        bool withinFront = !(this.player.position.x > this.forward.position.x);
        bool withinBack = !(this.player.position.x < this.back.position.x);
        bool withinLeft = !(this.player.position.z > this.left.position.z);
        bool withinRight = !(this.player.position.z < this.right.position.z);

        return withinTop && withinBottom && withinFront && withinBack && withinLeft && withinRight;
    }
    
    
}
