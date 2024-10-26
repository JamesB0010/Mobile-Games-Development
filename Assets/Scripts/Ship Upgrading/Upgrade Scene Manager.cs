using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UpgradeSceneManager : MonoBehaviour
{
    [SerializeField] private InputAction click;

    [SerializeField] private InputAction mousePos;

    [SerializeField] private Animation inventoryAnimationComp;

    [SerializeField] private AnimationClip closeInventoryAnim;

    private PointerEventData pointerEventData;

    private Vector2 mousePosition;

    [FormerlySerializedAs("itemPurchaser")] [SerializeField]
    private PurchaseItem purchaseItem;

    [FormerlySerializedAs("itemEquipper")] [SerializeField] private EquipItem equipItem;

    [SerializeField] private UnityEvent<UpgradeCell> cellSelected = new UnityEvent<UpgradeCell>();

    [SerializeField] private UnityEvent CellPurchasedEvent = new UnityEvent();

    [SerializeField] private UnityEvent CellEquippedEvent = new UnityEvent();
    private void Start()
    {
        click.Enable();
        click.performed += this.OnClick;
        mousePos.Enable();
        mousePos.performed += this.OnMouseMove;
        this.purchaseItem.SelectedCellPurchased += this.CellPurchased;
        this.equipItem.SelectedCellEquipped += this.CellEquipped;
    }

    public void TryPurchaseEquipCell(SelectedCellHighlight highlight)
    {
        UpgradeCell cell = highlight.SelectedCell;

        bool cellCanBeEquipped = cell.Upgrade.Gun.OwnedByPlayer && 
                                 cell.GunOwnedByThisSide();
        
        if (cellCanBeEquipped) 
            this.equipItem.EquipCell(cell);
        else
            this.purchaseItem.PurchaseCell(cell);
    }

    private void OnDestroy()
    {
        this.purchaseItem.SelectedCellPurchased -= this.CellPurchased;
        this.equipItem.SelectedCellEquipped -= this.CellEquipped;
    }

    public void ExitShop()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void CellPurchased()
    {
        this.CellPurchasedEvent?.Invoke();
    }

    public void CellEquipped()
    {
        this.CellEquippedEvent?.Invoke();
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
                this.cellSelected?.Invoke(upgradeCell);
            }
        }


    }
}
