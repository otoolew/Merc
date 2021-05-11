using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class BehaviorTask : ScriptableObject
{
    public abstract AIController Controller { get; set; }
    public abstract string TaskName { get; set; }
    public abstract KeyVariableInfo[] KeyVariables { get; set; }
    public abstract UnityEvent<BehaviorTask> OnTaskFinished { get; set; }
    public abstract void Init(AIController controller);
    public abstract bool PreConditionsMet();

    public abstract void UpdateTick();

    public abstract void Finish();
}
