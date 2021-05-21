using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class StateBehaviour : MonoBehaviour
{
    protected abstract AIController Controller { get; set; }

    [SerializeField] private string stateName;
    public string StateName { get => stateName; set => stateName = value; }
    
    [SerializeField] private bool isActiveState;
    public bool IsActiveState { get => isActiveState; set => isActiveState = value; }
    public abstract StateBehaviour ExitState { get; set; }

    /// <summary>
    /// Initializes the component values;
    /// </summary>
    /// <param name="controller"></param>
    protected virtual void Init(AIController controller)
    {
        Controller = controller;
    }

    /// <summary>
    /// Executes first when the StateMachine Enters this specific State
    /// </summary>
    public virtual void Enter()
    {
        isActiveState = true;
    }
    /// <summary>
    /// Executes the States Update logic
    /// </summary>
    public abstract void StateUpdate();

    /// <summary>
    /// Executes when the state exits this state
    /// </summary>
    public virtual void Exit()
    {
        isActiveState = false;
    }
}
