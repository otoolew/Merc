using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Variable : ScriptableObject
{
    public abstract string VariableName { get; set; }
    public abstract VariableType VariableType { get;}
    public abstract object GetValue();

    // public abstract VariableChangedEvent OnVariableChanged { get; set; }
}
