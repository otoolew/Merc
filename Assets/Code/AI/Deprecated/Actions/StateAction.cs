using UnityEngine;
using UnityEngine.Events;

public abstract class StateAction : ScriptableObject
{
    public abstract bool IsComplete { get; set; }
    public abstract void Perform (AIController controller);
    public abstract UnityAction OnActionComplete { get; set; }
}
