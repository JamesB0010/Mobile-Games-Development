using System.Runtime.CompilerServices;
using Player_Movement;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerShipBooster : MonoBehaviour
{
    [SerializeField] private PlayerShipEngine engine;
    private bool isBoosting;

    public bool IsBoosting
    {
        get => this.isBoosting;

        set
        {
            if (value)
            {
                if (this.isBoosting == false)
                {
                    //this is the first frame of boosting
                    this.StartBoostingEvent?.Invoke();
                    this.isBoostingValRef.SetValue(true);
                }
                
                this.playerMovement.CurrentMaxVelocity = this.engine.MaxBoostVelocity;
                isBoosting = true;
            }
            else
            {
                if (this.isBoosting == true)
                {
                    //this is the first frame we stopped boosting
                    this.StopBoostingEvent?.Invoke();
                    this.isBoostingValRef.SetValue(false);
                }
                this.playerMovement.CurrentMaxVelocity = this.maxVelocityDefault;
                isBoosting = false;
            }
        }
    }



    [SerializeField] private BoolReference isBoostingValRef;
    private PlayerMovement playerMovement;
    [SerializeField] private UnityEvent StartBoostingEvent = new UnityEvent();
    [SerializeField] private UnityEvent StopBoostingEvent = new UnityEvent();
    private float maxVelocityDefault;
    private void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        this.maxVelocityDefault = this.engine.MaxVelocity;
    }
    public void OnBoost(InputAction.CallbackContext ctx)
    {
        if (!this.engine.CanBoost)
            return;
        
        this.IsBoosting = ctx.action.IsPressed();
    }
}