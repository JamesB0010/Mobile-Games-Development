using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class resetUiColorPallete : MonoBehaviour
{
    [SerializeField] private ColorReference primaryColor, secondaryColor, tertiaryColor;

    [SerializeField] private Color defaultPrimaryColor, defaultSecondaryColor, defaultTertiaryColor;

    public void ResetColors()
    {
        this.primaryColor.SetValue(defaultPrimaryColor);
        this.secondaryColor.SetValue(this.defaultSecondaryColor);
        this.tertiaryColor.SetValue(this.defaultTertiaryColor);
    }
}
