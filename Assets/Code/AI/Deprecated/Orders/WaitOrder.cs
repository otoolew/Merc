using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class WaitOrder : IOrder
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
    
    [SerializeField] private float waitTime;
    public  float WaitTime { get => waitTime; set => waitTime = value; }

    [SerializeField] private UnityEvent<IOrder, bool> orderComplete;
    public UnityEvent<IOrder, bool> OrderCompleted { get => orderComplete; set => orderComplete = value; }

    public WaitOrder(AIController ownerController, float waitTime)
    {
        this.orderName = "ORDER [Wait -> "+waitTime+"]";
        this.ownerController = ownerController;
        this.waitTime = waitTime;
        this.repeatUntilComplete = false;
        this.inProcessing = false;
        this.isOrderComplete = false;
        orderComplete = new UnityEvent<IOrder, bool>();
    }
    public void Tick()
    {
        if (!inProcessing)
        {
            ownerController.StopAllCoroutines();
            ownerController.StartCoroutine(WaitRoutine(waitTime));
        }
    }
    
    public bool ConditionMet()
    {
        return isOrderComplete;
    }

    private IEnumerator WaitRoutine(float waitTime)
    {
        Debug.Log("Executing Order " + orderName);
        inProcessing = true;
        isOrderComplete = false;
        yield return new WaitForSeconds(waitTime);
        orderComplete?.Invoke(this, true);
        inProcessing = false;
        isOrderComplete = true;
    }
}
