using System.Runtime.CompilerServices;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerShipBooster : MonoBehaviour
{
    private bool isBoosting;
    public bool IsBoosting => this.isBoosting;
    
    [SerializeField] private float maxBoostVelocity;

    private PlayerMovement playerMovement;

    [SerializeField] private UnityEvent StartBoostingEvent = new UnityEvent();

    [SerializeField] private UnityEvent StopBoostingEvent = new UnityEvent();
    
    
    private float maxVelocityDefault;

    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        this.maxVelocityDefault = this.playerMovement.MaxVelocity;
    }

    public void OnBoost(InputAction.CallbackContext ctx)
    {
        if (ctx.action.IsPressed())
        {
            if (this.isBoosting == false)
            {
                //this is the first frame of boosting
                this.StartBoostingEvent?.Invoke();
            }
            this.playerMovement.CurrentMaxVelocity = this.maxBoostVelocity;
            isBoosting = true;
        }
        else
        {
            if (this.isBoosting == true)
            {
                //this is the first frame we stopped boosting
                this.StopBoostingEvent?.Invoke();
            }
            this.playerMovement.CurrentMaxVelocity = this.maxVelocityDefault;
            isBoosting = false;
        }
    }
}