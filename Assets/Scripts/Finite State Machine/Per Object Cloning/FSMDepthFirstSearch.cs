using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//This allows us to traverse the fninite state machine
//either in a way like how an iterator would allowing other objects to visit each node as we traverse

//or

//we can traverse the whole graph and collate a list of visited nodes. this list can be quite useful elsewhere
public class FSMDepthFirstSearch
{
    //Attributes
    //The entry point for the graph
    public State startNode = null;

    //Attributes to keep track of traversal
    //the state stack keeps track of all the nodes we have yet to visit
    //Stacks are used for depth first searches
    private Stack<State> stateStack = new Stack<State>();

    //the list of visited nodes
    //once the traversal is complete this list will hold all the nodes
    private List<State> visited = new List<State>();
    public List<State> Visited => this.visited; //Getter


    //Attributes for when using the class as an iterator
    //has the search finished or are we still searching
    private bool searching = false;
    public bool Searching => this.searching; //Getter


    //The current node we are on in our traversal
    private State current;
    public State Current => this.current; //getter


    //Methods
    public FSMDepthFirstSearch(State start)
    {
        this.startNode = start;
    }

    //Traverse whole graph all at once
    public void SearchDepthFirst()
    {
        this.InitSearch();
        while (this.searching)
        {
            this.Step();
        }
    }


    //Iterator pattern stuff
    public void InitSearch()
    {
        this.searching = true;
        this.stateStack.Push(this.startNode);
        this.Step();
    }

    public void Step()
    {
        //check if the search has finished
        if (this.stateStack.Count <= 0)
        {
            this.searching = false;
            return;
        }

        //if the search hasnt finished
        //1. pop the top item off the stack of nodes to traverse
        this.current = this.stateStack.Pop();

        //2. Add the item to the visited list
        this.visited.Add(this.current);

        //3. add all unvisited neighbours of this node to the state stack
        foreach (State neighbour in this.current.GetConnectedStates())
        {
            bool alreadyVisited = false;

            //Check if this neighbour has been visited
            foreach (State node in this.visited)
            {
                if (node == neighbour)
                {
                    alreadyVisited = true;
                }
            }

            //if we haven't already visited this node add it to the state stack
            if (alreadyVisited == false)
            {
                this.stateStack.Push(neighbour);
            }
        }
    }
}
