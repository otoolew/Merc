using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class StringVariable : Variable
{
    [SerializeField] private string variableName;
    public override string VariableName { get => variableName; set => variableName = value; }
    
    [SerializeField] private VariableType variableType;
    public override VariableType VariableType { get => variableType; set => variableType = value; }
    
    [SerializeField] private string value;
    public string Value { get => value; set => this.value = value; }
    
    public StringVariable(string variableName, string value)
    {
        this.variableName = variableName;
        variableType = VariableType.STRING;
        this.value = value;
    }
    
    public override object GetValue()
    {
        return value;
    }

    public void SetValue(string value)
    {
        this.value = value;
    }
}
