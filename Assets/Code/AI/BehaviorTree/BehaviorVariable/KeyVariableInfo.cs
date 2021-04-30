using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct KeyVariableInfo
{
    [SerializeField] private KeyVariableType variableType;
    public KeyVariableType VariableType => variableType;
    
    [SerializeField] private string variableName;
    public string VariableName => variableName;

    public KeyVariableInfo(KeyVariableType variableType, string variableName)
    {
        this.variableType = variableType;
        this.variableName = variableName;
    }
    
    public static KeyVariableInfo CreateInstance(KeyVariableType variableType, string variableName)
    {
        return new KeyVariableInfo(variableType,variableName);
    }
}
