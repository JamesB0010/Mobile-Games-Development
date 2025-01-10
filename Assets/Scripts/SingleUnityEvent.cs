using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SingleUnityEvent : MonoBehaviour
{
    public UnityEvent e;

    public void Execute()
    {
        this.e?.Invoke();
    }
}
