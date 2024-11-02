using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCellHighlight : MonoBehaviour
{
    private UpgradeCell selectedCell;
    public UpgradeCell SelectedCell => this.selectedCell;

    private static SelectedCellHighlight instance = null;

    private void Awake()
    {
        if (SelectedCellHighlight.instance == null)
            SelectedCellHighlight.instance = this;
        else
            Destroy(this.gameObject);
    }

    public void SelectCell(UpgradeCell cell)
    {
        transform.position = cell.transform.position;
        this.selectedCell = cell;
    }

    public void MoveOffScreen()
    {
        transform.position = new Vector3(-3000, -3000, 0);
    }
}
