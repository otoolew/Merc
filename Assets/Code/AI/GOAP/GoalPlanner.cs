using System.Collections;
using System.Collections.Generic;
using MLIGames.AI.GOAP.Node;
using UnityEngine;

public class GoalPlanner
{
    public Queue<GoalAction> Plan(List<GoalAction> actions, Dictionary<string, int> goal, WorldStates beliefStates) 
    {
        List<GoalAction> usableActions = new List<GoalAction>();

        //of all the actions available find the ones that can be achieved.
        foreach (GoalAction a in actions) {

            if (a.IsAchievable()) {

                usableActions.Add(a);
            }
        }

        //create the first node in the graph
        List<Node> leaves = new List<Node>();
        Node start = new Node(null, 0.0f, beliefStates.GetStates(), null);

        //pass the first node through to start branching out the graph of plans from
        bool success = BuildGraph(start, leaves, usableActions, goal);

        //if a plan wasn't found
        if (!success) {

            Debug.Log("NO PLAN");
            return null;
        }

        //of all the plans found, find the one that's cheapest to execute
        //and use that
        Node cheapest = null;
        foreach (Node leaf in leaves) {

            if (cheapest == null) {

                cheapest = leaf;
            } else if (leaf.cost < cheapest.cost) {

                cheapest = leaf;
            }
        }
        List<GoalAction> result = new List<GoalAction>();
        Node n = cheapest;

        while (n != null) {

            if (n.action != null) {

                result.Insert(0, n.action);
            }

            n = n.parent;
        }

        //make a queue out of the actions represented by the nodes in the plan
        //for the agent to work its way through
        Queue<GoalAction> queue = new Queue<GoalAction>();

        foreach (GoalAction a in result) {

            queue.Enqueue(a);
        }

        Debug.Log("The Plan is: ");
        foreach (GoalAction a in queue) {

            Debug.Log("Q: " + a.actionName);
        }

        return queue;
    }

    private bool BuildGraph(Node parent, List<Node> leaves, List<GoalAction> usableActions, Dictionary<string, int> goal) {

        bool foundPath = false;

        //with all the useable actions
        foreach (GoalAction action in usableActions) {

            //check their preconditions
            if (action.IsAchievableGiven(parent.state)) {

                //get the state of the world if the parent node were to be executed
                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);

                //add the effects of this node to the nodes states to reflect what
                //the world would look like if this node's action were executed
                foreach (KeyValuePair<string, int> eff in action.effects) {

                    if (!currentState.ContainsKey(eff.Key)) {

                        currentState.Add(eff.Key, eff.Value);
                    }
                }

                //create the next node in the branch and set this current node as the parent
                Node node = new Node(parent, parent.cost + action.cost, currentState, action);

                //if the current state of the world after doing this node's action is the goal
                //this plan will achieve that goal and will become the agent's plan
                if (GoalAchieved(goal, currentState)) {

                    leaves.Add(node);
                    foundPath = true;
                } else {
                    //if no goal has been found branch out to add other actions to the plan
                    List<GoalAction> subset = ActionSubset(usableActions, action);
                    bool found = BuildGraph(node, leaves, subset, goal);

                    if (found) {

                        foundPath = true;
                    }
                }
            }
        }
        return foundPath;
    }

    //remove and action from a list of actions
    private List<GoalAction> ActionSubset(List<GoalAction> actions, GoalAction removeMe) {

        List<GoalAction> subset = new List<GoalAction>();

        foreach (GoalAction a in actions) {

            if (!a.Equals(removeMe)) {

                subset.Add(a);
            }
        }
        return subset;
    }

    //check goals against state of the world to determine if the goal has been achieved.
    private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state) {

        foreach (KeyValuePair<string, int> g in goal) {

            if (!state.ContainsKey(g.Key)) {

                return false;
            }
        }
        return true;
    }
}
