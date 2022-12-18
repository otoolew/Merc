using System;
using UnityEngine;

[Serializable]
public class IntVariable : Variable
{
    [SerializeField] private string variableName;
    public override string VariableName { get => variableName; set => variableName = value; }
    
    [SerializeField] private VariableType variableType;
    public override VariableType VariableType { get => variableType; set => variableType = value; }
    
    [SerializeField] private int value;
    public int Value { get => value; set => this.value = value; }
    
    public IntVariable(string variableName, int value)
    {
        this.variableName = variableName;
        variableType = VariableType.INT;
        this.value = value;
    }
    public override object GetValue()
    {
        return value;
    }
    public void SetValue(int value)
    {
        this.value = value;
    }
    // public static IntVariable Create(string variableName, int value)
    // {
    //     IntVariable variable = ScriptableObject.CreateInstance<IntVariable>();
    //     variable.VariableName = variableName;
    //     variable.SetValue(value);
    //     return variable;
    // }

}
