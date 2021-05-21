using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public class MoveOrderStack : StackCollection<MoveOrder>
{
    [SerializeField] private Stack<MoveOrder> stateStack;
    protected override Stack<MoveOrder> StateStack => stateStack;
    
    [SerializeField] private List<MoveOrder> stateList;
    protected override List<MoveOrder> StateList => stateList;

    private void Awake()
    {
        stateList = new List<MoveOrder>();
        stateStack = new Stack<MoveOrder>();
    }
}
