using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(OneOfThreeSelection))]
public class EnemyOutlineSizeSetter : MonoBehaviour
{
    [SerializeField] private float[] sizes;

    private IEnumerator Start()
    {
        yield return null;
        var selectionController = GetComponent<OneOfThreeSelection>();
        int outlineSize = (int)BuzzardGameData.EnemyOutlineWidth.GetValue();
        if (outlineSize == 2)
        {
            selectionController.SelectedButton = 0;
        }else if (outlineSize == 6)
        {
            selectionController.SelectedButton = 1;
        }
        else
        {
            selectionController.SelectedButton = 2;
        }
    }

    public void OutlineSizeSelectionChange(int index)
    {
        BuzzardGameData.EnemyOutlineWidth.SetValue(this.sizes[index]);
    }
}
