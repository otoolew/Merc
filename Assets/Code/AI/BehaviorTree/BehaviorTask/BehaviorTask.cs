using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BehaviorTask : ScriptableObject
{
    [SerializeField] private BehaviorTree assignedBehaviorTree;
    public BehaviorTree AssignedBehaviorTree { get => assignedBehaviorTree; set => assignedBehaviorTree = value; }
    
    [SerializeField] private string taskName;
    public string TaskName { get => taskName; set => taskName = value; }
    
    [SerializeField] private KeyVariableInfo[] keyVariables;
    public KeyVariableInfo[] KeyVariables { get => keyVariables; set => keyVariables = value; }

    public abstract bool PreConditionsMet();
    public abstract bool Perform();
    public abstract bool Finish();
}
