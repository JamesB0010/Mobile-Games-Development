using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UiUpdaterInteractorStrategy
{
    protected UIViewUpdater ui;

    public UiUpdaterInteractorStrategy(UIViewUpdater ui)
    {
        this.ui = ui;
    }

    public abstract void UpdateItemDetailsText(int index = 0);
}
