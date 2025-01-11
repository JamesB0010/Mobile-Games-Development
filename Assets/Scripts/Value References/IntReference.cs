using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object Value References/Int")]
public class IntReference : SuperBaseScriptableValRef<int>
{
    [Serializable]
    public class JSON
    {
        public int value;

        public JSON(int val)
        {
            this.value = val;
        }

        public JSON(string filepath)
        {
            var data = JsonUtility.FromJson<IntReference.JSON>(File.ReadAllText(filepath));
            this.value = data.value;
        }

        public static IntReference CreateFromFilepath(string path)
        {
            IntReference val = ScriptableObject.CreateInstance<IntReference>();
            var data = JsonUtility.FromJson<IntReference.JSON>(File.ReadAllText(path));
            val.value = data.value;
            return val;
        }
    }
    private int value;

    public event Action<int> valueChanged;

    public override int GetValue()
    {
        return value;
    }

    public void SetValue(int val)
    {
        if (val == this.value)
            return;
        
        this.value = val;
        this.valueChanged?.Invoke(this.value);
    }

    public void SetValue(IntReference.JSON json)
    {
        this.value = json.value;
    }

    public static IntReference operator +(IntReference to, int val)
    {
        to.SetValue(to.GetValue() + val);
        return to;
    }
}