using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateAction : ScriptableObject
{
    public abstract string ActionName { get; set; }
    
    public abstract void PerformAction();
    public abstract void OnStateAction();
    
}
