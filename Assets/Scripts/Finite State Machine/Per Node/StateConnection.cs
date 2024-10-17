using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//A State Connection is a class used in the State Class (composition)
//this class holds the state connection from the parent state to 1 other state
//it also holds a list of conditions which will allow us to change from the parent state into the stateTo state
//This class is able to evaluate all the transitions and return if a transition is necessary or not

//Following feedback from carlo added transitionHandledByAgent bool to allow the agent to handle the transition
[Serializable]
public class StateConnection : ICloneable
{
    //Attributes
    [SerializeField]
    [Tooltip("The State in the FSM this connection is connected to")]
    private State stateTo;

    [Tooltip("If this is true then the agent owning the FSM this state connection is contained within will have to " +
             "evaluate if the state should transition," +
             " this is done in the evaluate function see cube behaviour for an example")]
    [SerializeField] public bool transitionHandledByAgent = false;

    [Tooltip("The conditions to get to the state this connection points to")]
    [SerializeReference]
    private List<TransitionConditionBase> transitionConditions = new List<TransitionConditionBase>();

    public void SetTransitionCondition(int index, TransitionConditionBase condition)
    {
        this.transitionConditions[index] = condition;
    }

    //Methods
    public StateConnection()
    {
        this.transitionConditions.Add(new FloatTransitionCondition());
    }

    //Logic
    public bool Evaluate(State stateAttachedTo, FSMBehaviour agent)
    {
        if (this.transitionHandledByAgent)
        {
            return agent.EvaluateTransition(stateAttachedTo, this.stateTo);
        }
        foreach (TransitionConditionBase condition in this.transitionConditions)
        {
            if (condition.Evaluate())
                return true;
        }

        return false;
    }

    //Getter
    public State StateTo
    {
        get
        {
            return this.stateTo;
        }
        set
        {
            this.stateTo = value;
        }
    }

    public void AddNewCondition()
    {
        this.transitionConditions.Add(new FloatTransitionCondition());
    }

    public void RemoveLatestCondition()
    {
        this.transitionConditions.RemoveAt(this.transitionConditions.Count - 1);
    }

    public List<TransitionConditionBase> GetTransitionConditions()
    {
        List<TransitionConditionBase> conditions = new List<TransitionConditionBase>();
        for (int i = 0; i < this.transitionConditions.Count; i++)
        {
            conditions.Add(this.transitionConditions[i]);
        }

        return conditions;
    }

    public object Clone()
    {
        StateConnection connection = new StateConnection();
        connection.stateTo = this.stateTo;
        connection.transitionConditions = this.transitionConditions;
        connection.transitionHandledByAgent = this.transitionHandledByAgent;

        return connection;
    }
}
