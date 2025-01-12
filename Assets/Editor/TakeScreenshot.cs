using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
[CreateAssetMenu]
public class TakeScreenshot : ScriptableObject
{
    [SerializeField] private string path;

    [SerializeField, Range(1, 5)] private int size = 1;

    public void Excecute()
    {
        string filename = this.path;
        filename += "screenshot ";
        filename += System.Guid.NewGuid().ToString() + ".png";
        
        ScreenCapture.CaptureScreenshot(filename, this.size);
    }
}


[CustomEditor(typeof(TakeScreenshot))]
public class TakeScreenshotInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.DrawDefaultInspector();
        if (GUILayout.Button("Take Screenshot"))
        {
            ((TakeScreenshot)target).Excecute();
        }
    }
}
