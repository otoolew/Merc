using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class IdleStateBehaviour : StateBehaviour , ISerializationCallbackReceiver
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
    
    [SerializeField] private List<VariableInfo> variableList = new List<VariableInfo>();
    public List<VariableInfo> VariableList { get => variableList; set => variableList = value; }
    
    [SerializeField] private StateBehaviour attackState;
    public StateBehaviour AttackState { get => attackState; set => attackState = value; }

    [SerializeReference] private string currentOrderName;
    public string CurrentOrderName { get => currentOrderName; set => currentOrderName = value; }
    
    [SerializeField] private StateBehaviour completeState;
    public StateBehaviour CompleteState { get => completeState; set => completeState = value; }

    private Stack<IOrder> orderStack = new Stack<IOrder>();
    public Stack<IOrder> OrderStack { get => orderStack; set => orderStack = value; }
    
    [SerializeReference] private List<IOrder> orderList = new List<IOrder>();
    public List<IOrder> OrderList { get => orderList; set => orderList = value; }
    
    [SerializeField] private UnityEvent<StateBehaviour, StateBehaviour> onOrdersComplete;
    public override UnityEvent<StateBehaviour, StateBehaviour> OnOrdersComplete { get => onOrdersComplete; set => onOrdersComplete = value; }
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
        Debug.Log("Enter " + this.stateName);
        this.isActiveState = true;
        this.controller = controller;
        PushOrder(new WaitOrder(controller, 0.5f));
    }

    public override void StateUpdate()
    {
        Debug.Log(this.StateName + " StateUpdate");
        if (visionPerception.HasTarget)
        {
            Debug.Log("visionPerception.HasTarget " + visionPerception.HasTarget);
            //controller.TransitionToState(attackState);
            orderStack.Clear();
            return;
        }
        
        if (orderStack.Count > 0)
        {
            Debug.Log("Tick");
            OrderStack.Peek().Tick();
        }
        else
        {
            onOrdersComplete?.Invoke(this, completeState);
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
        Debug.Log(order.OrderName + " Complete");
        order.OrderCompleted.RemoveAllListeners();
        
        if (orderStack.Count > 0)
        {
            if (OrderStack.Peek() == order)
            {
                OrderStack.Pop();
            }
        }
    }
    
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
}
