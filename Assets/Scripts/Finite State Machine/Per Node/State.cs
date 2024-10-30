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