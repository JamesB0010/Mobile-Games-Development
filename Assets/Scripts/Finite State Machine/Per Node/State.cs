using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.LowLevel;

//Each state is a scriptable object which can be created as an asset
//A state has a list of connected states 
//each state has a behave method where it will send a message to the owning agent for them to respond
[CreateAssetMenu(fileName = "New State", menuName = "Finite State Machine/State")]
public class State : ScriptableObject, ICloneable
{
    //Attributes
    //The game object agent which owns the fsm this state is part of. The actual behaviours of each state will be part of components attached to that object
    private FSMBehaviour agent;

    [SerializeField]
    [Tooltip("The other States connected to this state")]
    protected List<StateConnection> stateConnections;

    private string stateName;
    public string StateName => this.stateName;

    //Behavioural methods

    public void EnterState()
    {
        this.agent.EnterState(this);
    }
    public void Behave()
    {
        //following feedback no longer using send message
        this.agent.Behave(this);
    }

    public void SetLinkedAgent(FSMBehaviour agentObject)
    {
        this.agent = agentObject;
    }

    public State TestTransitions()
    {
        foreach (StateConnection connection in this.stateConnections)
        {
            if (connection.Evaluate(this, this.agent))
            {
                return connection.StateTo;
            }
        }

        return this;
    }


    //Getters
    public List<State> GetConnectedStates()
    {
        List<State> states = new List<State>();
        foreach (StateConnection connection in this.stateConnections)
        {
            states.Add(connection.StateTo);
        }

        return states;
    }

    public List<StateConnection> GetStateConnections()
    {
        return this.stateConnections;
    }

    public void AddEmptyTransition()
    {
        this.stateConnections.Add(new StateConnection());
    }

    public void RemoveLatestTransition()
    {
        this.stateConnections.RemoveAt(this.stateConnections.Count - 1);
    }

    public void AddConditionToTransition(int transitionIndex)
    {
        this.stateConnections[transitionIndex].AddNewCondition();
    }

    public void RemoveLastConditionFromTransition(int transitionIndex)
    {
        this.stateConnections[transitionIndex].RemoveLatestCondition();
    }

    //Cloning
    public object Clone()
    {
        //Create a new instance of a state
        State state = ScriptableObject.CreateInstance<State>();
        state.stateName = this.name;

        //Copy over data by value
        state.name = this.name + " (Clone)";

        //Deep copy the state connections
        DeepCopyStateConnections(state);

        return state;
    }

    private void DeepCopyStateConnections(State state)
    {
        state.stateConnections = new List<StateConnection>();
        for (int i = 0; i < this.stateConnections.Count; i++)
        {
            StateConnection clonedConnection = (StateConnection)this.stateConnections[i].Clone();
            state.stateConnections.Add(clonedConnection);
        }
    }
}



//Custom editor for states
[CustomEditor(typeof(State))]
class StateCustomEditor : Editor
{
    //Attributes
    internal State state;

    //this is a reference to the array its self
    internal SerializedProperty connectionListArrayReference;

    //this is an array of serialised properties. its the same as the transition list but faster to access
    internal SerializedProperty[] connectionList;

    //this is a list of references to the condition list found on each state connection
    internal SerializedProperty[] conditionLists;

    //this is the connection list but its full of state connections rather than serializedproperties
    internal List<StateConnection> stateConnections = new List<StateConnection>();


    internal GUIContent plusIcon;
    internal GUIContent minusIcon;

    private StateCustomEditorSetupHelper setupHelper = new StateCustomEditorSetupHelper();
    //Methods

    //setup methods
    private void OnEnable()
    {
        this.setupHelper.editor = this;

        this.setupHelper.SetupCustomEditor();
    }


    //Drawing the Ui methods
    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        DrawConnectionsListFoldout();

        bool connectionsFoldoutIsExpanded = this.connectionListArrayReference.isExpanded;
        if (connectionsFoldoutIsExpanded)
        {
            DrawStateConnectionsFoldoutContent();
        }
        
        serializedObject.ApplyModifiedProperties();
        EditorUtility.SetDirty(target);
    }

    private void DrawConnectionsListFoldout()
    {
        EditorGUILayout.BeginHorizontal();

        this.connectionListArrayReference.isExpanded =
            EditorGUILayout.Foldout(this.connectionListArrayReference.isExpanded, "State Connections");

        EditorGUILayout.IntField(this.connectionListArrayReference.arraySize, GUILayout.Width(47.5f));

        EditorGUILayout.EndHorizontal();
    }

    private void DrawStateConnectionsFoldoutContent()
    {
        DrawEachStateConnection();

        DrawAddRemoveConnectionButtons();
    }

    private void DrawEachStateConnection()
    {
        //iterate over all the connections from this state to the other states its connected to
        int connectionListSize = this.connectionListArrayReference.arraySize;
        for (int i = 0; i < connectionListSize; i++)
        {
            DrawStateConnection(i);
        }
    }

    private void DrawStateConnection(int i)
    {
        bool transitionHandledByAgent = DrawStateConnectionObjectData(i);
        if (transitionHandledByAgent) return;

        bool transitionConditionsListFoldoutRetracted = !DrawTransitionConditionsListFoldout(i);
        if (transitionConditionsListFoldoutRetracted) return;

        DrawEachTransitionCondition(i);

        DrawAddRemoveTransitionConditionButtons(i);
    }

    private bool DrawStateConnectionObjectData(int i)
    {
        object objectReferenceValue;

        EditorGUI.indentLevel = 1;

        SerializedProperty currentConnection = this.connectionList[i];
        currentConnection.isExpanded = EditorGUILayout.Foldout(currentConnection.isExpanded, "Element " + i);

        if (currentConnection.isExpanded == false)
            return true;

        EditorGUI.indentLevel += 3;


        objectReferenceValue = EditorGUILayout.ObjectField("State To", this.stateConnections[i].StateTo, typeof(State), true);

        if (objectReferenceValue is State stateTo)
            if (stateTo != null)
                this.stateConnections[i].StateTo = stateTo;


        this.stateConnections[i].transitionHandledByAgent = EditorGUILayout.Toggle(
            "Are Transition Conditions handled by Agent", this.stateConnections[i].transitionHandledByAgent);

        bool isThisTransitionHandledByAgent = this.stateConnections[i].transitionHandledByAgent;
        return isThisTransitionHandledByAgent;
    }

    private bool DrawTransitionConditionsListFoldout(int i)
    {
        this.conditionLists[i].isExpanded =
            EditorGUILayout.Foldout(conditionLists[i].isExpanded, "Transition Conditions");

        bool isTheConditionsListExpanded = this.conditionLists[i].isExpanded;
        return isTheConditionsListExpanded;
    }

    private void DrawEachTransitionCondition(int i)
    {
        EditorGUI.indentLevel += 2;
        int conditionListsArraySize = this.conditionLists[i].arraySize;

        for (int j = 0; j < conditionListsArraySize; j++)
        {
            DrawTransitionCondition(i, j);
        }
    }

    private void DrawTransitionCondition(int connectionIndex, int conditionOfThatConnectionIndex)
    {
        bool conditionFoldoutRetracted = DrawFoldoutForThisCondition(connectionIndex, conditionOfThatConnectionIndex);
        if (conditionFoldoutRetracted) return;

        //Get the transition condition object we are going to be drawing the data for
        TransitionConditionBase transitionCondition =
            this.stateConnections[connectionIndex].GetTransitionConditions()[conditionOfThatConnectionIndex];

        EditorGUI.indentLevel = 9;
        DrawTransitionConditionObjectData(connectionIndex, conditionOfThatConnectionIndex, transitionCondition);
    }

    private bool DrawFoldoutForThisCondition(int connectionIndex, int conditionOfThatConnectionIndex)
    {
        EditorGUI.indentLevel = 6;
        this.conditionLists[connectionIndex].GetArrayElementAtIndex(conditionOfThatConnectionIndex).isExpanded =
            EditorGUILayout.Foldout(
                this.conditionLists[connectionIndex].GetArrayElementAtIndex(conditionOfThatConnectionIndex).isExpanded,
                "Condition " + conditionOfThatConnectionIndex);

        bool conditionFoldoutRetracted = !this.conditionLists[connectionIndex].GetArrayElementAtIndex(conditionOfThatConnectionIndex).isExpanded;

        return conditionFoldoutRetracted;
    }
    private void DrawTransitionConditionObjectData(int connectionIndex, int conditionOfThatConnectionIndex,
            TransitionConditionBase transitionCondition)
    {
        DrawConditionValueReferenceField(transitionCondition);

        DrawComparisonOperatorSelector(transitionCondition);

        DrawAppropriateConditionComparand(connectionIndex, conditionOfThatConnectionIndex, transitionCondition);
    }

    private static void DrawConditionValueReferenceField(TransitionConditionBase transitionCondition)
    {
        object objectReferenceValue = EditorGUILayout.ObjectField("Value Reference",
            transitionCondition.ValueToTest,
            typeof(ScriptableObjectValueReference), false);

        if (objectReferenceValue != null)
            transitionCondition.ValueToTest = (ScriptableObjectValueReference)objectReferenceValue;
    }

    private static void DrawComparisonOperatorSelector(TransitionConditionBase transitionCondition)
    {
        object objectReferenceValue = EditorGUILayout.ObjectField(
            "Comparison Operator", transitionCondition.comparisonOperator, typeof(ComparisonOperator), false);
        if (objectReferenceValue != null)
        {
            transitionCondition.comparisonOperator = (ComparisonOperator)objectReferenceValue; 
            EditorUtility.SetDirty(transitionCondition.comparisonOperator);
        }
        
    }

    private void DrawAppropriateConditionComparand(int connectionIndex, int conditionOfThatConnectionIndex,
            TransitionConditionBase transitionCondition)
    {
        if (transitionCondition.ValueToTest is BoolReference && transitionCondition is BoolTransitionCondition)
        {
            transitionCondition.SetComparand(EditorGUILayout.Toggle("Comparand",
                Convert.ToBoolean(transitionCondition.GetComparand())));
        }
        else if (transitionCondition.ValueToTest is BoolReference)
        {
            transitionCondition = transitionCondition.CastToBoolCondition();
            this.stateConnections[connectionIndex]
                .SetTransitionCondition(conditionOfThatConnectionIndex, transitionCondition);
            transitionCondition.SetComparand(EditorGUILayout.Toggle("Comparand",
                Convert.ToBoolean(transitionCondition.GetComparand())));
        }
        else if (transitionCondition.ValueToTest is FloatReference &&
                 transitionCondition is FloatTransitionCondition)
        {
            transitionCondition.SetComparand(EditorGUILayout.FloatField("Comparand",
                (float)transitionCondition.GetComparand()));
        }
        else if (transitionCondition.ValueToTest is FloatReference)
        {
            transitionCondition = transitionCondition.CastToFloatCondition();
            this.stateConnections[connectionIndex]
                .SetTransitionCondition(conditionOfThatConnectionIndex, transitionCondition);
            transitionCondition.SetComparand(EditorGUILayout.FloatField("Comparand",
                (float)transitionCondition.GetComparand()));
        }
    }



    private void DrawAddRemoveTransitionConditionButtons(int i)
    {
        EditorGUILayout.BeginHorizontal();
        GUILayout.FlexibleSpace();
        if (GUILayout.Button(this.plusIcon, GUILayout.Width(25)))
        {
            this.state.AddConditionToTransition(i);
        }

        if (GUILayout.Button(this.minusIcon, GUILayout.Width(25)))
        {
            this.state.RemoveLastConditionFromTransition(i);
        }

        EditorGUILayout.EndHorizontal();
    }
    private void DrawAddRemoveConnectionButtons()
    {
        EditorGUILayout.BeginHorizontal();

        if (GUILayout.Button(this.plusIcon, GUILayout.Width(25)))
        {
            this.state.AddEmptyTransition();
            serializedObject.Update();
            this.setupHelper.SetupSerialisedProperties();
        }

        if (GUILayout.Button(this.minusIcon, GUILayout.Width(25)))
        {
            this.state.RemoveLatestTransition();
            serializedObject.Update();
            this.setupHelper.SetupSerialisedProperties();
        }

        EditorGUILayout.EndHorizontal();
    }
}

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