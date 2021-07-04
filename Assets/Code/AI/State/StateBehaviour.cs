using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

/// <summary>
/// Abstract class for state behaviour
/// </summary>
public abstract class StateBehaviour : MonoBehaviour
{
    /// <summary>
    /// the Owning Controller
    /// </summary>
    protected abstract AIController Controller { get; set; }
    /// <summary>
    /// Name of the State
    /// </summary>
    public abstract string StateName { get; set; }
    
    /// <summary>
    /// Whether this state is actively running
    /// </summary>
    public abstract bool IsActiveState { get; set; }
    
    /// <summary>
    /// Initializes the component values;
    /// </summary>
    /// <param name="controller"></param>
    protected abstract void Init(AIController controller);

    /// <summary>
    /// Executes first when the StateMachine Enters this specific State
    /// </summary>
    public abstract void Enter(AIController controller);
    
    /// <summary>
    /// Executes the States Update logic
    /// </summary>
    public abstract void StateUpdate();

    /// <summary>
    /// Invoked when orders are complete;
    /// </summary>
    /// <param name="controller"></param>
    protected UnityAction<StateBehaviour, StateBehaviour> OrdersCompleted;

}
