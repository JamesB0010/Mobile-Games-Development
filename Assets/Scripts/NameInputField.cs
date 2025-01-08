using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class NameInputField : MonoBehaviour
{
    [SerializeField] private GameObject inputField;

    [SerializeField] private GameObject PlayButton;

    [SerializeField] private GameObject CreditsButton;

    private EventSystem eventSystem;

    private AnimationCurve animCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);

    [SerializeField] private GameObject keyboardParent;

    [SerializeField] private GameObject QButton;

    [SerializeField] private AnimationCurve playAndCreditsLeaveAnimCurve;
    

    private void Awake()
    {
        this.eventSystem = EventSystem.current;
    }

    void ActivateKeyboard()
    {
        inputField.transform.position.LerpTo(new Vector3(960, -200 + 1080, 0), 0.6f, value =>
            {
                inputField.transform.position = value;
            }, pkg =>
            {
                    
            },
            this.animCurve);
        
        
            Vector3 oldPos = this.PlayButton.transform.position;
            this.PlayButton.transform.position.LerpTo(new Vector3(-442.99f, oldPos.y, oldPos.z), 0.6f, value =>
                {
                    this.PlayButton.transform.position = value;
                },
                pkg => { },
            this.playAndCreditsLeaveAnimCurve);

            oldPos = this.CreditsButton.transform.position;
            this.CreditsButton.transform.position.LerpTo(new Vector3(-442.99f, oldPos.y, oldPos.z), 0.6f, value =>
            {
                this.CreditsButton.transform.position = value;
            },
                pkg =>
                {
                    this.keyboardParent.transform.position.LerpTo(new Vector3(960, -680 + 1080, 0), 0.8f, value =>
                        {
                            this.keyboardParent.transform.position = value;
                        },
                        pkg =>
                        {
                            this.eventSystem.SetSelectedGameObject(this.QButton);
                        },
                        this.animCurve);
                },
                this.playAndCreditsLeaveAnimCurve);
    }

    // Update is called once per frame
    void Update()
    {
        float verticalInput = Input.GetAxis("VerticalMenu");
        if (EventSystem.current.currentSelectedGameObject == inputField)
        {
            //move off input field
            if (verticalInput < -0.2f)
            {
                this.eventSystem.SetSelectedGameObject(this.PlayButton);
            }

            if (Input.GetAxis("Submit") == 1.0f)
            {
                this.ActivateKeyboard();
                Debug.Log("start typing");
                this.inputField.GetComponent<TMP_InputField>().text = "";
            }
        }
    }
}
