using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct MoveOrder: IOrder
{
    [SerializeField] private AIMovement movementComp;
    public AIMovement MovementComp { get => movementComp; set => movementComp = value; }
    
    [SerializeField] private Vector3 location;
    public Vector3 Location { get => location; set => location = value; }
    
    [SerializeField] private float stoppingDistance;
    public float StoppingDistance { get => stoppingDistance; set => stoppingDistance = value; }
    
    public MoveOrder(AIMovement movementComp, Vector3 location)
    {
        this.movementComp = movementComp;
        this.location = location;
        this.stoppingDistance = movementComp.StoppingDistance;
    }
    
    public bool ConditionMet(Vector3 pos, float acceptedRange)
    {
        return Vector3.Distance(pos, location) <= acceptedRange;
    }
    
    public bool ConditionMet()
    {
        return ConditionMet(location,stoppingDistance);
    }
    // public static bool operator ==(MoveOrder x, MoveOrder y)
    // {
    //     return x.Location == y.Location;
    // }
    //
    // public static bool operator !=(MoveOrder x, MoveOrder y)
    // {
    //     return !(x == y);
    // }
}
