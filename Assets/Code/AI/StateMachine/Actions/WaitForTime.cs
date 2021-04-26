using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitForTime : StateAction 
{
    [SerializeField] private string actionName;
    public override string ActionName { get => actionName; set => actionName = value; }

    public override void PerformAction()
    {
        Debug.Log("WaitForTime -> PerformAction.");
    }
    
    public override void OnStateAction()
    {
        Debug.Log("WaitForTime -> OnStateAction.");
    }
}
