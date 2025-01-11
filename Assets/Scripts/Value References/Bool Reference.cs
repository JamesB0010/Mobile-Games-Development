using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object Value References/Boolean")]
public class BoolReference : SuperBaseScriptableValRef<bool>
{
    [Serializable]
    public class JSON
    {
        public bool value;

        public JSON(bool value)
        {
            this.value = value;
        }

        public JSON(string filepath)
        {
            var data = JsonUtility.FromJson<BoolReference.JSON>(File.ReadAllText(filepath));
            this.value = data.value;
        }

        public static BoolReference CreateFromFilepath(string path)
        {
            BoolReference val = ScriptableObject.CreateInstance<BoolReference>();
            var data = JsonUtility.FromJson<BoolReference.JSON>(File.ReadAllText(path));
            val.value = data.value;
            return val;
        }
    }
    
    private bool value;

    public event Action<bool> valueChanged;

    public override bool GetValue()
    {
        return value;
    }

    public void SetValue(bool val)
    {
        if (val == this.value)
            return;
        
        this.value = val;
        this.valueChanged?.Invoke(this.value);
    }

    public void SetValue(BoolReference.JSON json)
    {
        this.value = json.value;
    }
}