using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class KillOrder : IOrder
{
    [SerializeField] private string orderName;
    public string OrderName { get => orderName; set => orderName = value; }
    
    [SerializeField] private AIController ownerController;

    public AIController OwnerController { get => ownerController; set => ownerController = value; }

    [SerializeField] private bool repeatUntilComplete;
    public bool RepeatUntilComplete { get => repeatUntilComplete; set => repeatUntilComplete = value; }
    
    [SerializeField] private bool inProcessing;
    public bool InProcessing { get => inProcessing; set => inProcessing = value; }
    
    
    [SerializeField] private Character killTarget;
    public Character KillTarget { get => killTarget; set => killTarget = value; }

    [SerializeField] private UnityEvent<IOrder, bool> orderComplete;
    public UnityEvent<IOrder, bool> OrderCompleted { get => orderComplete; set => orderComplete = value; }

    public KillOrder(AIController ownerController, Character target)
    {
        this.orderName = "ORDER [Kill -> "+target.name+"]";
        this.ownerController = ownerController;
        this.killTarget = target;
        this.repeatUntilComplete = false;
        this.inProcessing = false;
        orderComplete = new UnityEvent<IOrder, bool>();
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
        return killTarget.IsDead();
    }

    private IEnumerator ProcessOrderRoutine()
    {
        Debug.Log("Executing Order: " + orderName);
        inProcessing = true;
        yield return new WaitUntil(ConditionMet);
        orderComplete?.Invoke(this, true);
        inProcessing = false;
    }
}
