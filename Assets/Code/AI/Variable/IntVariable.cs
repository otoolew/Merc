using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IntVariable : Variable
{
    [SerializeField] private string variableName;
    public override string VariableName { get => variableName; set => variableName = value; }
    public override VariableType VariableType => VariableType.INT;

    [SerializeField] private int value;
    public override object GetValue()
    {
        return value;
    }
    
    public void SetValue(int value)
    {
        this.value = value;
    }
    public static IntVariable Create(string variableName, int value)
    {
        IntVariable variable = ScriptableObject.CreateInstance<IntVariable>();
        variable.VariableName = variableName;
        variable.SetValue(value);
        return variable;
    }
}
