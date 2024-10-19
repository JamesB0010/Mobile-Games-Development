using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UpgradeSceneManager : MonoBehaviour
{
    [SerializeField] private FloatReference playerMoney;

    [SerializeField] private InputAction click;

    [SerializeField] private InputAction mousePos;

    [SerializeField] private Animation inventoryAnimationComp;

    [SerializeField] private AnimationClip closeInventoryAnim;

    private PointerEventData pointerEventData;

    private Vector2 mousePosition;

    private void Start()
    {
        click.Enable();
        click.performed += this.OnClick;
        mousePos.Enable();
        mousePos.performed += this.OnMouseMove;
    }

    public void ExitShop()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public bool PurchaseGun(Gun gun, float price)
    {
        if (price <= (float)playerMoney.GetValue())
        {
            float newBalance = (float)playerMoney.GetValue() - price;
            this.playerMoney.SetValue(newBalance);
            
            //Do gun purchasing logic here
            return true;
        }
        return false;
    }

    private void OnMouseMove(InputAction.CallbackContext ctx)
    {
        this.mousePosition = ctx.ReadValue<Vector2>();
    }

    [SerializeField] private UnityEvent CloseInventoryEvent = new UnityEvent();
    public void CloseInventory()
    {
        this.inventoryAnimationComp.clip = this.closeInventoryAnim;
        this.inventoryAnimationComp.Play();
        this.CloseInventoryEvent?.Invoke();
    }

    private void OnClick(InputAction.CallbackContext ctx)
    {
        float click = ctx.ReadValue<float>();

        if (click != 1)
        {
            return;
        }

        this.pointerEventData = new PointerEventData(EventSystem.current)
        {
            position = this.mousePosition
        };

        List<RaycastResult> raycastResults = new List<RaycastResult>();
        
        EventSystem.current.RaycastAll(this.pointerEventData, raycastResults);
        
        foreach (var result in raycastResults)
        {
            Debug.Log(result.gameObject.name);
            if (result.gameObject.TryGetComponent(out ShipPartLabel partLabel))
            {
                partLabel.Clicked();
            }
            else if (result.gameObject.TryGetComponent(out UpgradeCell upgradeCell))
            {
                upgradeCell.SetAsSelectedUpgradeCell();
            }
        }


    }
}
