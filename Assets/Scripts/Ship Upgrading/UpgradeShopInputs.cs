using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Serialization;
using TouchPhase = UnityEngine.TouchPhase;

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
    private UnityEvent<Vector2> TouchDownEvent = new UnityEvent<Vector2>();

    [SerializeField] private UnityEvent TouchUpEvent = new UnityEvent();

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
        var control = ctx.control as TouchControl;
        switch (control.phase.ReadValue())
        {
            case UnityEngine.InputSystem.TouchPhase.Began:
        this.TouchDownEvent?.Invoke(this.mousePosition);
                break;
            case UnityEngine.InputSystem.TouchPhase.Ended:
            this.TouchUpEvent?.Invoke();
                break;
        }
    }

    private void OnMouseMove(InputAction.CallbackContext ctx)
    {
        this.mousePosition = ctx.ReadValue<Vector2>();
        this.MoveEvent?.Invoke(this.mousePosition);
    }
}
