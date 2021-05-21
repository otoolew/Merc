using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct MovementOrders
{
    [SerializeField] private AIMovement movementComp;
    public AIMovement MovementComp { get => movementComp; set => movementComp = value; }
    
}
