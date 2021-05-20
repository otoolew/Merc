using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateBehaviour : MonoBehaviour
{
    [SerializeField] private string stateName;
    public string StateName { get => stateName; set => stateName = value; }
    
    [SerializeField] private bool isActiveSate;
    public bool IsActiveSate { get => isActiveSate; set => isActiveSate = value; }

    /// <summary>
    /// Executes first when the StateMachine Enters this specific State
    /// </summary>
    public abstract void Enter();
    /// <summary>
    /// Executes the States Update logic
    /// </summary>
    public abstract void StateUpdate();
    /// <summary>
    /// Executes when the state exits this state
    /// </summary>
    public abstract void Exit();
}
