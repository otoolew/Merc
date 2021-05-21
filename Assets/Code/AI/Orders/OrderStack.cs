using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class OrderStack : StackCollection<IOrder>
{
    [SerializeField] private Stack<IOrder> stateStack;
    protected override Stack<IOrder> StateStack => stateStack;
    
    [SerializeField] private List<IOrder> stateList;
    protected override List<IOrder> StateList => stateList;

    private void Awake()
    {
        stateList = new List<IOrder>();
        stateStack = new Stack<IOrder>();
    }
}
