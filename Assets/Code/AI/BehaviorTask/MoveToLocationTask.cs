using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Game/AI/MoveTo", fileName = "newMoveToLocationTask")]
public class MoveToLocationTask : BehaviorTask
{    
    [SerializeField,HideInInspector] private AIController controller;
    public override AIController Controller { get => controller; set => controller = value; }
    
    [SerializeField] private string taskName;
    public override string TaskName { get => taskName; set => taskName = value; }
    
    [SerializeField] private KeyVariableInfo[] keyVariables;
    public override KeyVariableInfo[] KeyVariables { get => keyVariables; set => keyVariables = value; }
    
    [SerializeField,HideInInspector] private UnityEvent<BehaviorTask> onTaskFinished;
    public override UnityEvent<BehaviorTask> OnTaskFinished { get => onTaskFinished; set => onTaskFinished = value; }
    
    [SerializeField] private Vector3 location;
    public Vector3 Location { get => location; set => location = value; }
    
    public override void Init(AIController controller)
    {
        this.controller = controller;
    }

    public override bool PreConditionsMet()
    {
        return false;
    }

    public override void UpdateTick()
    {
        
        throw new System.NotImplementedException();
    }

    public override void Finish()
    {
        throw new System.NotImplementedException();
    }
}
