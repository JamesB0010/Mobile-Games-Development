using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

internal class StateCustomEditorSetupHelper
{
    public StateCustomEditor editor;
    public void SetupCustomEditor()
    {
        this.InitDataFromTargetObject();
        this.CacheIcons();
        this.SetupSerialisedProperties();
    }

    private void InitDataFromTargetObject()
    {
        this.editor.state = (State)editor.target;
        this.editor.stateConnections = this.editor.state.GetStateConnections();
    }

    private void CacheIcons()
    {
        this.editor.plusIcon = EditorGUIUtility.IconContent("Toolbar Plus");
        this.editor.minusIcon = EditorGUIUtility.IconContent("Toolbar Minus");
    }
    public void SetupSerialisedProperties()
    {
        this.editor.connectionListArrayReference = editor.serializedObject.FindProperty("stateConnections");
        editor.connectionList = new SerializedProperty[editor.connectionListArrayReference.arraySize];
        editor.conditionLists = new SerializedProperty[editor.connectionListArrayReference.arraySize];


        for (int i = 0; i < editor.connectionListArrayReference.arraySize; i++)
        {
            editor.connectionList[i] = editor.connectionListArrayReference.GetArrayElementAtIndex(i);
            editor.conditionLists[i] = editor.connectionList[i].FindPropertyRelative("transitionConditions");
        }
    }
}

