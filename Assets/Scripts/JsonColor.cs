using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class JsonColor 
{
    public float r, g, b, a;

    public JsonColor(Color color)
    {
        this.FromColor(color);
    }
    
    public Color ToColor()
    {
        return new Color(r, g, b, a);
    }

    public void FromColor(Color color)
    {
        this.r = color.r;
        this.g = color.g;
        this.b = color.b;
        this.a = color.a;
    }
}
