using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionStack : StackCollection<GoalAction>
{
    [SerializeField] private Stack<GoalAction> stateStack;
    protected override Stack<GoalAction> StateStack => stateStack;

    [SerializeField] private List<GoalAction> stateList;
    protected override List<GoalAction> StateList => stateList;

    private void OnEnable()
    {
        stateStack = new Stack<GoalAction>();
        stateList = new List<GoalAction>();
    }
    
}
