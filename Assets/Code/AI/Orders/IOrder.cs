using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public interface IOrder
{ 
    string OrderName { get; set; }
    AIController OwnerController { get; set; }
    bool RepeatUntilComplete { get; set; }
    bool InProcessing { get; set; }
    void Tick();
    UnityEvent<IOrder,bool> OrderCompleted { get; set; }
}
