using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateNode : ScriptableObject
{
    public virtual string StateName { get; set; }

    public virtual void OnStateEnter()
    {
        Debug.Log("StateNode -> OnStateEnter.");
    }

    public virtual void OnStateUpdate()
    {
        Debug.Log("StateNode -> OnStateEnter.");
    }

    public virtual void OnStateExit()
    {
        Debug.Log("StateNode -> OnStateEnter.");
    }

    public static StateNode Create(string nodeName)
    {
        return ScriptableObject.CreateInstance<StateNode>();
    }
}
