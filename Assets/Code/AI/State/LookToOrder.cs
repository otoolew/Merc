using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class LookToOrder : ExecutionOrder
{
    [SerializeField] private AIController owner;
    public AIController Owner { get => owner; set => owner = value; }

    [SerializeField] private string description;
    public override string Description { get => description; set => description = value; }
    
    [SerializeField] private Vector3 location;
    public Vector3 Location { get => location; set => location = value; }

    [SerializeField] private UnityEvent<ExecutionOrder> orderCompleted;
    public override UnityEvent<ExecutionOrder> OrderCompleted { get => orderCompleted; set => orderCompleted = value; }
    
    public override bool PreconditionsMet()
    {
        return true;
    }

    public override void Perform(AIController controller)
    {
        
    }

    
    public static LookToOrder Create(AIController controller, Vector3 location)
    {
        LookToOrder order = CreateInstance<LookToOrder>();
        order.Owner = controller;
        order.Location = location;
        return order;
    }
}
