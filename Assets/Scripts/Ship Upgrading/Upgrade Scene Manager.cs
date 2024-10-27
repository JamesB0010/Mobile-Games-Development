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

public class ItemShopActionsManager : MonoBehaviour
{
    [Header("Item Shop Actions")]
    [SerializeField] private PurchaseItem purchaseItemShopAction;

    [SerializeField] private EquipItem equipItemShopAction;
    
    [Space(2)]
    [Header("Events")]
    [SerializeField] private UnityEvent CellPurchasedEvent = new UnityEvent();

    [SerializeField] private UnityEvent CellEquippedEvent = new UnityEvent();
    
    private void Start()
    {
        this.purchaseItemShopAction.SelectedCellPurchased += this.CellPurchased;
        this.equipItemShopAction.SelectedCellEquipped += this.CellEquipped;
    }

    public void TryPurchaseEquipCell(SelectedCellHighlight highlight)
    {
        UpgradeCell cell = highlight.SelectedCell;

        bool cellCanBeEquipped = false;
        if (cell.Upgrade.IsPurchaseable)
            cellCanBeEquipped = OwnedUpgradesCounter.Instance.GetUpgradeCount(cell.Upgrade) > 0;
        
        if (cellCanBeEquipped) 
            this.equipItemShopAction.EquipCell(cell);
        else
        {
            this.purchaseItemShopAction.PurchaseCell(cell);
            this.equipItemShopAction.EquipCell(cell);
        }
    }


    public void CellPurchased()
    {
        this.CellPurchasedEvent?.Invoke();
    }

    public void CellEquipped()
    {
        this.CellEquippedEvent?.Invoke();
    }
    
    private void OnDestroy()
    {
        this.purchaseItemShopAction.SelectedCellPurchased -= this.CellPurchased;
        this.equipItemShopAction.SelectedCellEquipped -= this.CellEquipped;
    }
}
