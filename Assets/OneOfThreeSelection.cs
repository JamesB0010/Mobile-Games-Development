using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class OneOfThreeSelection : MonoBehaviour
{
    [SerializeField] private Button button1, button2, button3;

    private int selectedButton = -1;

    [SerializeField] private UnityEvent<int> selectedOptionChanged;
    
    public int SelectedButton
    {
        set
        {
            if (this.selectedButton == value)
                return;

            this.selectedButton = value;
            
            this.EnableAllButtons();
            this.buttons[this.selectedButton].interactable = false;
            this.selectedOptionChanged?.Invoke(this.selectedButton);
        }
    }
    
    private readonly Button[] buttons = new Button[3];

    private void Start()
    {
        this.buttons[0] = button1;
        this.buttons[1] = button2;
        this.buttons[2] = button3;
        this.EnableAllButtons();

        for (int i = 0; i < this.buttons.Length; i++)
        {
            var i1 = i;
            this.buttons[i].onClick.AddListener(() => this.SelectedButton = i1);
        }
    }


    private void EnableAllButtons()
    {
        foreach (Button button in this.buttons)
        {
            button.interactable = true;
        }
    }
}
