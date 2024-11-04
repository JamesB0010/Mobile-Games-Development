using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCellHighlight : MonoBehaviour
{
    private UpgradeCell selectedCell;
    public UpgradeCell SelectedCell => this.selectedCell;

    private bool stickToCell = false;

    private static SelectedCellHighlight instance = null;

    private void Awake()
    {
        if (SelectedCellHighlight.instance == null)
            SelectedCellHighlight.instance = this;
        else
            Destroy(this.gameObject);
    }

    private void Update()
    {
        if (this.stickToCell)
            transform.position = selectedCell.transform.position;
    }

    public void SelectCell(UpgradeCell cell)
    {
        this.selectedCell = cell;
        this.stickToCell = true;
        transform.position = cell.transform.position;
    }

    public void MoveOffScreen()
    {
        this.stickToCell = false;
        transform.position = new Vector3(-3000, -3000, 0);
    }
}
