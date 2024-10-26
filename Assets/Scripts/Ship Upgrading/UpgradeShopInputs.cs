using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

//1. Storing input values
//2. Checking for and responding to ui elements being pressed
public class UpgradeShopInputs : MonoBehaviour
{
    [Header("Input Actions")]
    [SerializeField] private InputAction click;
    [Space]
    [SerializeField] private InputAction mousePos;
    
    private Vector2 mousePosition;

    
    [Space(2)]
    [Header("Events")]
    [SerializeField]
    private UnityEvent<Vector2> ClickEvent = new UnityEvent<Vector2>();
    
    [SerializeField]
    private UnityEvent<Vector2> MoveEvent = new UnityEvent<Vector2>();
    

    private void Start()
    {
        click.Enable();
        click.performed += this.OnClick;
        
        mousePos.Enable();
        mousePos.performed += this.OnMouseMove;
    }
    
    
    private void OnClick(InputAction.CallbackContext ctx)
        {
            float click = ctx.ReadValue<float>();
    
            if (click != 1)
            {
                return;
            }
            
            this.ClickEvent?.Invoke(this.mousePosition);
        }
    
     private void OnMouseMove(InputAction.CallbackContext ctx)
        {
            this.mousePosition = ctx.ReadValue<Vector2>();
            this.MoveEvent?.Invoke(this.mousePosition);
        }
}
