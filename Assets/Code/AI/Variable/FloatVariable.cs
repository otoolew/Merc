using System;
using UnityEngine;

[Serializable]
public class FloatVariable : IVariable
{
    public FloatVariable(string variableName, float value)
    {
        this.variableName = variableName;
        this.value = value;
        this.variableType = VariableType.FLOAT;
    }
    
    [SerializeField] private VariableType variableType;
    public VariableType VariableType { get => variableType; set => variableType = value; }

    [SerializeField] private string variableName;
    public string VariableName { get => variableName; set => variableName = value; }
    
    [SerializeField] private float value;
    public float Value { get => value; set => this.value = value; }
    object IVariable.GetValue => value;
}
