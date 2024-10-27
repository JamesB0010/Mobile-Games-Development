using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct KeyValuePairWrapper<T1, T2>
{
    [SerializeField]
    public T1 key;

    [SerializeField]
    public T2 value;

    public KeyValuePairWrapper(T1 key, T2 value)
    {
        this.key = key;
        this.value = value;
    }
}
