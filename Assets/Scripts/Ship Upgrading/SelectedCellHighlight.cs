using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCellHighlight : MonoBehaviour
{
    private UpgradeCell selectedCell;
    public UpgradeCell SelectedCell => this.selectedCell;
    public void SelectCell(UpgradeCell cell)
    {
        transform.position = cell.transform.position;
        this.selectedCell = cell;
    }
}
