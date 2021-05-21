using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleStateBehaviour : StateBehaviour
{
    [SerializeField] private AIController controller;
    protected override AIController Controller { get => controller; set => controller = value; }
    
    [SerializeField] private AIMovement movementComp;
    public AIMovement MovementComp { get => movementComp; set => movementComp = value; }
    
    [SerializeField] private VisionPerception visionPerception;
    public VisionPerception VisionPerception { get => visionPerception; set => visionPerception = value; }
    
    [SerializeField] private StateBehaviour exitState;
    public override StateBehaviour ExitState { get => exitState; set => exitState = value; }
    private void Start()
    {
        controller = GetComponent<AIController>();
        if (controller != null)
        {
            Init(controller);
        }
    }
    
    protected override void Init(AIController controller)
    {
        base.Init(controller);
        movementComp = controller.AssignedCharacter.MovementComp;
        visionPerception = controller.AssignedCharacter.VisionPerception;
    }
    
    public override void Enter()
    {
        if (visionPerception.HasTarget)
        {
            Exit();
            Debug.Log("Transition To Attack State");
        }
        PushPatrolPattern();
    }

    public override void StateUpdate()
    {
        if (movementComp.MoveOrderStack.Count > 0)
        {
            
        }
        else
        {
            Debug.Log("Patrol Complete!");
        }
    }

    public override void Exit()
    {
        Debug.Log("Exit Attack State");
        movementComp.ContinueToPatrolPoint();
        IsActiveState = false;
        controller.CurrentState = exitState;
    }

    public void PushPatrolPattern()
    {
        for (int i = 0; i < movementComp.PatrolCircuit.PatrolpointList.Count; i++)
        {
            movementComp.PushMoveOrder(new MoveOrder(movementComp, movementComp.PatrolCircuit.PatrolpointList[i].transform.position));
        }
    }
}
