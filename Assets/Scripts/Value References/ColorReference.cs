using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;


[CreateAssetMenu(menuName = "Scriptable Object Value References/Color")]
public class ColorReference : SuperBaseScriptableValRef<Color>
{
    [Serializable]
    public class JSON
    {
        public JsonColor value;

        public JSON (Color color)
        {
            this.value = new JsonColor(color);
        }

        public JSON(string filepath)
        {
            var data = JsonUtility.FromJson<ColorReference.JSON>(File.ReadAllText(filepath));
            this.value = data.value;
        }

        public static ColorReference CreateFromFilepath(string path)
        {
            ColorReference val = ScriptableObject.CreateInstance<ColorReference>();
            var data = JsonUtility.FromJson<ColorReference.JSON>(File.ReadAllText(path));
            val.value = data.value.ToColor();
            return val;
        }
    }
    private Color value;

    public event Action<Color> valueChanged;
    public override Color GetValue()
    {
        return value;
    }

    public void SetValue(Color val)
    {
        if (val == this.value)
            return;

        this.value = val;
        this.valueChanged?.Invoke(this.value);
    }

    public void SetValue(ColorReference.JSON json)
    {
        this.value = json.value.ToColor();
    }
}
