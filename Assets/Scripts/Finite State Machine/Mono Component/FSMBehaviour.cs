using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Added following feedback from module lead. This will be implemented by any agent controlled by a finite state machine
//It has a Behave method. this way the state can call that behave method instead of using send message.

//additionally i was told in many fsm implementations its the agents responsibility to evaluate the transitions between the states
//so i added the EvaluateTransition so the designer has the option for the transition to be handled by the agent or handled by the state
//by comparing scriptable object value references with a comparand
public abstract class FSMBehaviour : MonoBehaviour
{
    public virtual void EnterState(State state)
    {
        
    }
    public abstract void Behave(State state);

    public virtual bool EvaluateTransition(State current, State to)
    {
        return false;
    }
}
