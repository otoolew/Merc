using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BoolVariable : IVariable
{
    public BoolVariable(string variableName, bool value)
    {
        this.variableName = variableName;
        this.value = value;
        this.variableType = VariableType.BOOL;
    }
    
    [SerializeField] private VariableType variableType;
    public VariableType VariableType { get => variableType; set => variableType = value; }

    [SerializeField] private string variableName;
    public string VariableName { get => variableName; set => variableName = value; }
    
    [SerializeField] private bool value;
    public bool Value { get => value; set => this.value = value; }
    object IVariable.GetValue => value;
}