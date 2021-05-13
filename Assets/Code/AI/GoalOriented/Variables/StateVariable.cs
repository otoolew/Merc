using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateVariable : ScriptableObject
{
    public abstract string VariableName { get; set; }
    public abstract object GetValue();
}
