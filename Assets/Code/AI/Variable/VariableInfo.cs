using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[Serializable]
public struct VariableInfo
{
    [SerializeField] private VariableType variableType;
    public VariableType VariableType => variableType;
    
    [SerializeField] private string variableName;
    public string VariableName => variableName;

    public VariableInfo(VariableType variableType, string variableName)
    {
        this.variableType = variableType;
        this.variableName = variableName;
    }
    // public static VariableInfo CreateInstance(VariableInfo variableType, string variableName)
    // {
    //     return new VariableInfo(variableType,variableName);
    // }
}