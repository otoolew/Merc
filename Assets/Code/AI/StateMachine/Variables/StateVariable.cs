using System;
using UnityEngine;
public abstract class StateVariable : ScriptableObject
{
    public abstract string VariableName { get; set; }
    public abstract object Value { get;}
    public abstract Type VariableType { get; }
}
