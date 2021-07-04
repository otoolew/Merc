using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class IntVariable : Variable
{
    [SerializeField] private string variableName;
    public override string VariableName { get => variableName; set => variableName = value; }
    public override VariableType VariableType => VariableType.INT;

    [SerializeField] private int value;
    public int Value { get => value; set => this.value = value; }
    public override object GetValue()
    {
        return value;
    }
    
    public void SetValue(int value)
    {
        this.value = value;
    }
    
    [SerializeField] private UnityEvent<Variable> onValueChanged;
    public override UnityEvent<Variable> OnValueChanged { get => onValueChanged; set => onValueChanged = value; }
    
    public static IntVariable Create(string variableName, int value)
    {
        IntVariable variable = ScriptableObject.CreateInstance<IntVariable>();
        variable.VariableName = variableName;
        variable.SetValue(value);
        return variable;
    }
}
