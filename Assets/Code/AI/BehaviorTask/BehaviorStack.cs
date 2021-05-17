using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BehaviorStack : StackCollection<BehaviorTask>
{
    // [SerializeField] private Stack<BehaviorTask> stateStack;
    // public override Stack<BehaviorTask> StateStack => stateStack;
    //
    // [SerializeField] private List<BehaviorTask> stateList;
    // public override List<BehaviorTask> StateList => stateList;
    //
    // private void OnEnable()
    // {
    //     Debug.Log("Projectile Stack Enabled");
    //     stateStack = new Stack<BehaviorTask>();
    //     stateList = new List<BehaviorTask>();
    // }
}
