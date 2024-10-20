using System;
using System.Collections;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditorInternal;
#endif
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

//The state manager is the 1 of 2 components attached to any agent with a state machine
//The state manager holds the reference to the state machine and drives its updates

//the other component is a user defined component which has methods for responding to when
//a state updates
public class StateManager : MonoBehaviour
{
    [SerializeField]
    private State activeState;

    private void Start()
    {
        this.activeState = this.CloneTreeTemplate();
        LinkFSMToGameObject();
    }
    private State CloneTreeTemplate()
    {
        FSMDepthFirstSearch searcher = new FSMDepthFirstSearch(this.activeState);
        FSMCloner cloner = new FSMCloner(searcher);
        cloner.CloneGraph();
        return cloner.NewFsmEntry;
    }

    private void LinkFSMToGameObject()
    {
        FSMDepthFirstSearch searcher = new FSMDepthFirstSearch(this.activeState);
        searcher.SearchDepthFirst();
        FSMBehaviourStateLinker linker = new FSMBehaviourStateLinker(GetComponent<FSMBehaviour>(), searcher.Visited);
        linker.Link();
    }


    private void Update()
    {
        State newState = activeState.TestTransitions();
        if (this.activeState != newState)
        {
            //Start
            newState.EnterState();
        }

        activeState = newState;

        activeState.Behave();
    }
}
