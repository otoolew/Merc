using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackStateBehaviour : StateBehaviour
{
    [SerializeField] private AIController controller;
    public AIController Controller { get => controller; set => controller = value; }

    private void Start()
    {
        controller = GetComponent<AIController>();
    }

    public override void Enter()
    {
        Debug.Log("Enter Attack State");
    }

    public override void StateUpdate()
    {
        Debug.Log("Update Attack State");
        
    }

    public override void Exit()
    {
        Debug.Log("Exit Attack State");
    }
    
    
}
