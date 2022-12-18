using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class MoveOrder: IOrder
{
    [SerializeField] private string orderName;
    public string OrderName { get => orderName; set => orderName = value; }
    
    [SerializeField] private AIController ownerController;
    public AIController OwnerController { get => ownerController; set => ownerController = value; }

    [SerializeField] private bool repeatUntilComplete;
    public bool RepeatUntilComplete { get => repeatUntilComplete; set => repeatUntilComplete = value; }
    
    [SerializeField] private bool inProcessing;
    public bool InProcessing { get => inProcessing; set => inProcessing = value; }
    
    [SerializeField] private bool isOrderComplete;
    public bool IsOrderComplete { get => isOrderComplete; set => isOrderComplete = value; }

    [SerializeField] private Vector3 location;
    public Vector3 Location { get => location; set => location = value; }
    
    [SerializeField] private float stoppingDistance;
    public float StoppingDistance { get => stoppingDistance; set => stoppingDistance = value; }

    [SerializeField] private UnityEvent<IOrder, bool> orderCompleted;


    public UnityEvent<IOrder, bool> OrderCompleted { get => orderCompleted; set => orderCompleted = value; }
    
    public MoveOrder(AIController ownerController, Vector3 location)
    {
        this.orderName = "ORDER[Move To -> "+location+"]";
        this.ownerController = ownerController;
        this.location = location;
        this.stoppingDistance = ownerController.AssignedCharacter.MovementComp.StoppingDistance;
        this.repeatUntilComplete = false;
        isOrderComplete = false;
        this.inProcessing = false;
        orderCompleted = new UnityEvent<IOrder, bool>();
    }
    
    public void Tick()
    {
        if (!inProcessing)
        {
            ownerController.StopAllCoroutines();
            ownerController.StartCoroutine(ProcessOrderRoutine());
        }
    }
    public bool ConditionMet()
    {
        return ConditionMet(location,stoppingDistance);
    }
    public bool ConditionMet(Vector3 pos, float acceptedRange)
    {
        return Vector3.Distance(pos, location) <= acceptedRange;
    }
    private IEnumerator ProcessOrderRoutine()
    {
        inProcessing = true;
        isOrderComplete = false;
        //ownerController.AssignedCharacter.MovementComp.Move(location);
        yield return new WaitUntil(ownerController.AssignedCharacter.MovementComp.HasArrived);
        orderCompleted?.Invoke(this, true);
        inProcessing = false;
        isOrderComplete = true;
    }
}
