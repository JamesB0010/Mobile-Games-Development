using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Object Value References/Float")]
public class FloatReference : SuperBaseScriptableValRef<float>
{
    [Serializable]
    public class JSON
    {
        public float value;

        public JSON (float val)
        {
            this.value = val;
        }

        public JSON(string filepath)
        {
            var data = JsonUtility.FromJson<FloatReference.JSON>(File.ReadAllText(filepath));
            this.value = data.value;
        }

        public static FloatReference CreateFromFilepath(string path)
        {
            FloatReference val = ScriptableObject.CreateInstance<FloatReference>();
            var data = JsonUtility.FromJson<FloatReference.JSON>(File.ReadAllText(path));
            val.value = data.value;
            return val;
        }
    }
    private float value;

    public event Action<float> valueChanged;

    public override float GetValue()
    {
        return value;
    }

    public void SetValue(float val)
    {
        if (val == this.value)
            return;
        
        this.value = val;
        this.valueChanged?.Invoke(this.value);
    }

    public void SetValue(FloatReference.JSON json)
    {
        this.value = json.value;
    }

    public static FloatReference operator +(FloatReference to, float val)
    {
        to.SetValue(to.GetValue() + val);
        return to;
    }
}