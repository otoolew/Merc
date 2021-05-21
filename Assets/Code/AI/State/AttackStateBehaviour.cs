using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateBehaviour : StateBehaviour
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
        this.controller = controller;
        this.movementComp = controller.AssignedCharacter.MovementComp;
        this.visionPerception = controller.AssignedCharacter.VisionPerception;
    }
    
    public override void Enter()
    {
        base.Enter();

        if (visionPerception.HasTarget)
        {
            movementComp.StopBySet();  
        }
        else
        {
            Exit();
        }

    }

    public override void StateUpdate()
    {
        if (visionPerception.HasTarget)
        {
            movementComp.RotateTo(visionPerception.CurrentTarget.WorldLocation);
            controller.AssignedCharacter.Attack();
        }
        
        
    }

    public override void Exit()
    {
        base.Exit();
        Debug.Log("Exit Attack State");
        controller.CurrentState = exitState;
    }
    
    
}