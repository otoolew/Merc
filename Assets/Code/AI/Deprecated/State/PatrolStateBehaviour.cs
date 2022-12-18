using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PatrolStateBehaviour : StateBehaviour
{
    [SerializeField] private string stateName;
    public override string StateName { get => stateName; set => stateName = value; }
    
    [SerializeField] private bool isActiveState;
    public override bool IsActiveState { get => isActiveState; set => isActiveState = value; }
    
    [SerializeField] private AIController controller;
    protected override AIController Controller { get => controller; set => controller = value; }

    [SerializeField] private AIMovement movementComp;
    public AIMovement MovementComp { get => movementComp; set => movementComp = value; }
    
    [SerializeField] private VisionPerception visionPerception;
    public VisionPerception VisionPerception { get => visionPerception; set => visionPerception = value; }

    [SerializeField] private StateBehaviour idleState;
    public StateBehaviour IdleState { get => idleState; set => idleState = value; }
    
    [SerializeField] private StateBehaviour attackState;
    public StateBehaviour AttackState { get => attackState; set => attackState = value; }

    [SerializeReference] private string currentOrderName;
    public string CurrentOrderName { get => currentOrderName; set => currentOrderName = value; }
    
    private Stack<IOrder> orderStack = new Stack<IOrder>();
    public Stack<IOrder> OrderStack { get => orderStack; set => orderStack = value; }
    
    [SerializeReference] private List<IOrder> orderList = new List<IOrder>();
    public List<IOrder> OrderList { get => orderList; set => orderList = value; }
    
    [SerializeField] private UnityEvent<StateBehaviour, StateBehaviour> onOrdersComplete;
    public override UnityEvent<StateBehaviour, StateBehaviour> OnOrdersComplete { get => onOrdersComplete; set => onOrdersComplete = value; }
    
    [SerializeField] private PatrolCircuit patrolCircuit;
    public PatrolCircuit PatrolCircuit { get => patrolCircuit; set => patrolCircuit = value; }

    [SerializeField] private int currentWaypointIndex;
    public int CurrentWaypointIndex { get => currentWaypointIndex; set => currentWaypointIndex = value; }
    
    private void Start()
    {
        orderList = new List<IOrder>();
        orderStack = new Stack<IOrder>();
        
        controller = GetComponent<AIController>();
        if (controller != null)
        {
            Init(controller);
        }
    }
    
    protected override void Init(AIController controller)
    {
        movementComp = controller.AssignedCharacter.MovementComp;
        visionPerception = controller.AssignedCharacter.VisionPerception;
    }

    public override void Enter(AIController controller)
    {
        Debug.Log("Enter " + this.name);
        this.isActiveState = true;
        this.controller = controller;
        InvestigateArea();
    }

    public override void StateUpdate()
    {
        if (visionPerception.HasTarget)
        {
            //controller.TransitionToState(attackState);
            orderStack.Clear();
            return;
        }
        
        if (orderStack.Count > 0)
        {
            OrderStack.Peek().Tick();
        }
        else
        {
            //controller.TransitionToState(idleState);
        }
    }
    public void ContinueToPatrolPoint()
    {
        if (CurrentWaypointIndex < patrolCircuit.PatrolpointList.Count)
        {
            movementComp.SetDestination(patrolCircuit.PatrolpointList[CurrentWaypointIndex].transform.position);
        }
    }
    
    public void GoToNextPatrolPoint()
    {
        CurrentWaypointIndex++;
        if (CurrentWaypointIndex >= patrolCircuit.PatrolpointList.Count)
        {
            CurrentWaypointIndex = 0;
        }

        movementComp.SetDestination(patrolCircuit.PatrolpointList[CurrentWaypointIndex].transform.position);
    }
    public Vector3 GetCurrentPatrolPoint()
    {
        return patrolCircuit.PatrolpointList[CurrentWaypointIndex].transform.position;
    }
    
    public void RandomLocationInRadius()
    {
        CurrentWaypointIndex++;
        if (CurrentWaypointIndex >= patrolCircuit.PatrolpointList.Count)
        {
            CurrentWaypointIndex = 0;
        }

        movementComp.SetDestination(patrolCircuit.PatrolpointList[CurrentWaypointIndex].transform.position);
    }
    public void PushPatrolPattern()
    {
        Debug.Log("PushPatrolPattern");
        for (int i = 0; i < patrolCircuit.PatrolpointList.Count; i++)
        {
            PushOrder(new MoveOrder(controller, patrolCircuit.PatrolpointList[i].transform.position));
            PushOrder(new WaitOrder(controller, 0.5f));
        }
    }
    public void PushOrder(IOrder order)
    {
        order.OrderCompleted.AddListener(OnOrderComplete);
        OrderStack.Push(order);
        currentOrderName = OrderStack.Peek().OrderName;
    }
    public void OnOrderComplete(IOrder order, bool value)
    {
        Debug.Log(order.OrderName + " Complete " + value);
        order.OrderCompleted.RemoveAllListeners();
        
        if (orderStack.Count > 0)
        {
            if (OrderStack.Peek() == order)
            {
                OrderStack.Pop();
            }
        }
    }
    public void InvestigateArea()
    {
        orderStack.Push(new MoveOrder(controller, visionPerception.TargetLastKnownLocation));
        orderStack.Push(new WaitOrder(controller, 0.5f));
        orderStack.Push(new MoveOrder(controller, visionPerception.TargetLastKnownLocation * Random.insideUnitCircle));
        orderStack.Push(new WaitOrder(controller, 0.5f));
        orderStack.Push(new MoveOrder(controller, visionPerception.TargetLastKnownLocation * Random.insideUnitCircle));
        orderStack.Push(new WaitOrder(controller, 0.5f));
        orderStack.Push(new MoveOrder(controller, visionPerception.TargetLastKnownLocation * Random.insideUnitCircle));
        orderStack.Push(new WaitOrder(controller, 0.5f));
        Debug.Log("OrderStack " + OrderStack.Count);
    }

    #region Editor
    public void OnBeforeSerialize()
    {
        OrderList.Clear();
        if (OrderStack.Count > 0)
        {
            foreach (var kvp in OrderStack)
            {
                OrderList.Add(kvp);
            }
        }
    }
    
    public void OnAfterDeserialize()
    {
        if (orderList.Count > 0)
        {
            orderStack = new Stack<IOrder>();
            for (int i = orderList.Count; i >= 0; i--)
            {
                orderStack.Push(OrderList[i]);
            }
        }
    }

    private void OnDrawGizmos()
    {
        if (orderStack.Count > 0)
        {
            Gizmos.color = Color.yellow;
            if (orderStack.Peek().GetType() == typeof(MoveOrder))
            {
                MoveOrder order = (MoveOrder) orderStack.Peek();
                Gizmos.DrawSphere(order.Location, 0.5f);
            }
            
        }
    }

    #endregion
}
