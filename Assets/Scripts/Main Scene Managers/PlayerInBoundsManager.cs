using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerInBoundsManager : MonoBehaviour
{
    [SerializeField] private Transform top, bottom, forward, back, left, right;

    [SerializeField] private Transform player;

    [SerializeField] private UnityEvent outOfBounds = new UnityEvent();

    [SerializeField] private UnityEvent returnedToBounds = new UnityEvent();

    private bool playerInBounds = true;

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
                this.outOfBounds?.Invoke();
            }

            bool playerReEnteredBounds = this.playerInBounds == false && value == true;
            if (playerReEnteredBounds)
            {
                this.returnedToBounds?.Invoke();
            }
            
            this.playerInBounds = value;
        }
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
