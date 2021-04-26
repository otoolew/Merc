using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RootStateNode : StateNode
{
    [SerializeField] private string stateName;
    public override string StateName { get => stateName; set => stateName = value; }
    
    public override void OnStateEnter()
    {
        Debug.Log("RootStateNode -> OnStateEnter.");
    }

    public override void OnStateUpdate()
    {
        Debug.Log("RootStateNode -> OnStateUpdate.");
    }

    public override void OnStateExit()
    {
        Debug.Log("RootStateNode -> OnStateExit.");
    }
    
    public new static RootStateNode Create(string nodeName)
    {
        return ScriptableObject.CreateInstance<RootStateNode>();
    }
}
