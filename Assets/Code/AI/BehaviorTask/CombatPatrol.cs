using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Game/AI/CombatPatrol", fileName = "newCombatPatrol")]
public class CombatPatrol : BehaviorTask
{    
    [SerializeField,HideInInspector] private AIController controller;
    public override AIController Controller { get => controller; set => controller = value; }
    
    [SerializeField] private string taskName;
    public override string TaskName { get => taskName; set => taskName = value; }

    [SerializeField] private KeyVariableInfo[] keyVariables;
    public override KeyVariableInfo[] KeyVariables { get => keyVariables; set => keyVariables = value; }
    
    [SerializeField,HideInInspector] private UnityEvent<BehaviorTask> onTaskFinished;
    public override UnityEvent<BehaviorTask> OnTaskFinished { get => onTaskFinished; set => onTaskFinished = value; }

    public override void Init(AIController controller)
    {
        this.controller = controller;
    }

    public override bool PreConditionsMet()
    {
        if (!(controller is null) && !(controller.AssignedCharacter.VisionPerception.HasTarget) &&
            !(controller.AssignedCharacter.MovementComp.PatrolCircuit is null))
        {
            return true;
        }
        return false;
    }

    public override void UpdateTick()
    {
        if (controller.AssignedCharacter.MovementComp.HasArrived())
        {
            controller.AssignedCharacter.MovementComp.GoToNextPatrolPoint();
        }
        //Debug.Log("Combat Patrol");
    }

    public override void Finish()
    {
        
    }

    private void OnArrival()
    {
        
    }

    
}
