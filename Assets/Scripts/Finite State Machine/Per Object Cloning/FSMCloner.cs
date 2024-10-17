using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FSMCloner
{
    //Attributes
    //The entrypoint of the cloned finite state machine initialized to be null
    private State newFsmEntry = null;
    public State NewFsmEntry => this.newFsmEntry;

    //the searcher here is used like an iterator to traverse the fsm. this way the FSMCloner can act as a visitor and perform part of the cloning process
    //as the searcher is traversing the graph
    private FSMDepthFirstSearch searcher;

    //The oldToNewNodes dictionary is used after the nodes have all been cloned in order to link up all the newly cloned nodes
    //in the same structure as all the old nodes
    private Dictionary<State, State> oldToNewNodes = new Dictionary<State, State>();


    //Methods
    public FSMCloner(FSMDepthFirstSearch searcher)
    {
        this.searcher = searcher;
    }

    public void CloneGraph()
    {
        //Clone all of the nodes in the graph
        for (searcher.InitSearch(); searcher.Searching; searcher.Step())
        {
            ConstructOldToNewMap();
        }


        //By this point we have performed a deep copy on all the nodes of the graph
        //we now need to link all the newly cloned nodes to each other in the same way 
        //as the old nodes
        LinkUpClonedNodes();

        //set our entry point for the newly cloned fsm
        this.newFsmEntry = this.oldToNewNodes[searcher.startNode];
    }

    private void ConstructOldToNewMap()
    {
        //New node already created in map then return
        if (this.oldToNewNodes.ContainsKey(searcher.Current))
        {
            return;
        }

        //if we are seeing a new node now then we can clone it and add it to the oldToNewNodes map
        State copy = (State)searcher.Current.Clone();
        this.oldToNewNodes.Add(this.searcher.Current, copy);
    }
    private void LinkUpClonedNodes()
    {
        foreach (State state in this.oldToNewNodes.Values)
        {
            List<StateConnection> connections = state.GetStateConnections();

            for (int i = 0; i < connections.Count; i++)
            {
                connections[i].StateTo = this.oldToNewNodes[connections[i].StateTo];
            }
        }
    }
}
