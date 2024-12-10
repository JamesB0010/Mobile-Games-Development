using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class PrefetchAssetsLoadingText : MonoBehaviour
{
    [SerializeField] private List<string> loadingMessages;
    
    private Text text;

    private void Awake()
    {
        this.text = GetComponent<Text>();
    }

    void Start()
    {
        this.loadingMessages.Add(this.text.text);
        StartCoroutine(nameof(this.RandomiseTextRepeat));
    }

    private IEnumerator RandomiseTextRepeat()
    {
        int i = 0;
        while (true)
        {
            if (!this.enabled)
                break;
            
            text.text = loadingMessages[i];
            i++;
            i %= loadingMessages.Count;
            yield return new WaitForSeconds(4);
        }
    }
}
