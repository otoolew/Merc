using System;
using UnityEngine;

[Serializable]
public struct MovementOrder
{
    [SerializeField] private AIMovement movementComp;
    public AIMovement MovementComp { get => movementComp; set => movementComp = value; }
    
    [SerializeField] private Vector3 destination;
    public Vector3 Destination { get => destination; set => destination = value; }

    public MovementOrder(AIMovement movementComp, Vector3 destination)
    {
        this.movementComp = movementComp;
        this.destination = destination;
    }
}
