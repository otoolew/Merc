using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class Variable : IVariable
{
    //[SerializeField] private string variableName;
    //public virtual string VariableName { get => variableName; set => variableName = value; }
    public abstract string VariableName { get; set; }
    // [SerializeField] private VariableType variableType;
    // public virtual VariableType VariableType { get => variableType; set => variableType = value; }
    public abstract VariableType VariableType { get; set; }
    public abstract object GetValue();
    // public abstract string VariableName { get; set; }
    // public abstract VariableType VariableType { get;}
    // public abstract object GetValue();
    // public abstract UnityEvent<Variable> OnValueChanged { get; set; }
}
