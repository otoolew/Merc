using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MLIGames.AI.GOAP.Node
{
    public class Node
    { //the parent node this node is connected to

        //the parent node this node is connected to
        public Node parent;
        //how much it cost to get to this node
        public float cost;
        //the state of the environment by the time the
        //action assigned to this node is achieved
        public Dictionary<string, int> state;
        //the action this node represents in the plan
        public GoalAction action;

        // Constructor
        public Node(Node parent, float cost, Dictionary<string, int> allStates, GoalAction action) {

            this.parent = parent;
            this.cost = cost;
            this.state = new Dictionary<string, int>(allStates);
            this.action = action;
        }

        // Overloaded Constructor
        public Node(Node parent, float cost, Dictionary<string, int> allStates, Dictionary<string, int> beliefStates, GoalAction action) {

            this.parent = parent;
            this.cost = cost;
            this.state = new Dictionary<string, int>(allStates);

            //as well as the world states add the agents beliefs as states that can be
            //used to match preconditions
            foreach (KeyValuePair<string, int> b in beliefStates) {

                if (!this.state.ContainsKey(b.Key)) {

                    this.state.Add(b.Key, b.Value);
                }
            }
            this.action = action;
        }
    }
}

