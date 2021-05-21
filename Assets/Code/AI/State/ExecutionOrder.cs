using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ExecutionOrder : ScriptableObject
{
    public abstract string Description { get; set; }
    public abstract bool PreconditionsMet();
    public abstract void Perform(AIController controller);
    public abstract UnityEvent<ExecutionOrder> OrderCompleted { get; set; }
}
