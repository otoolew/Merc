using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AttackStateBehaviour : StateBehaviour
{
    [SerializeField] private string stateName;
    public override string StateName { get => stateName; set => stateName = value; }
    
    [SerializeField] private bool isActiveState;
    public override bool IsActiveState { get => isActiveState; set => isActiveState = value; }
    
    [SerializeField] private AIController controller;
    protected override AIController Controller { get => controller; set => controller = value; }

    [SerializeField] private AIMovement movementComp;
    public AIMovement MovementComp { get => movementComp; set => movementComp = value; }
    
    [SerializeField] private VisionPerception visionPerception;
    public VisionPerception VisionPerception { get => visionPerception; set => visionPerception = value; }

    // #region StateVariables
    // [Header("State Variables")]
    // [SerializeField] private HealthComponent targetHealth;
    // public HealthComponent TargetHealth { get => targetHealth; set => targetHealth = value; }
    //
    // #endregion
    
    #region States
    [Header("States")]
    [SerializeField] private HealthComponent targetHealth;
    public HealthComponent TargetHealth { get => targetHealth; set => targetHealth = value; }
    
    [SerializeField] private BoolVariable hasTarget;
    public BoolVariable HasTarget { get => hasTarget; set => hasTarget = value; }
    #endregion
    [SerializeField] private StateBehaviour cautionState;
    public StateBehaviour CautionState { get => cautionState; set => cautionState = value; }
    
    [SerializeField] private StateBehaviour idleState;
    public StateBehaviour IdleState { get => idleState; set => idleState = value; }

    [SerializeField] private UnityEvent<StateBehaviour, StateBehaviour> onOrdersComplete;
    public override UnityEvent<StateBehaviour, StateBehaviour> OnOrdersComplete { get => onOrdersComplete; set => onOrdersComplete = value; }
    
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
        movementComp = controller.AssignedCharacter.MovementComp;
        visionPerception = controller.AssignedCharacter.VisionPerception;
    }

    public override void Enter(AIController controller)
    {
        Controller = controller;
    }
    
    public override void StateUpdate()
    {
        if (!visionPerception.TargetInLineOfSight())
        {
            //controller.TransitionToState(cautionState);
            return;
        }
        movementComp.RotateTo(visionPerception.CurrentTarget.WorldLocation);
        controller.AssignedCharacter.Attack();
    }


}
