using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct VariableInfo
{
    [SerializeField] private string variableName;
    public string VariableName => variableName;
    [SerializeField] private VariableType variableType;
    public VariableType VariableType => variableType;
    
    public VariableInfo(string variableName, VariableType variableType)
    {
        this.variableName = variableName;
        this.variableType = variableType;
    }
}