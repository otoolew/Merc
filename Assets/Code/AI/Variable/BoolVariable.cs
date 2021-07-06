using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class BoolVariable : Variable
{
    [SerializeField] private string variableName;
    public override string VariableName { get => variableName; set => variableName = value; }
    
    [SerializeField] private VariableType variableType;
    public override VariableType VariableType { get => variableType; set => variableType = value; }
    
    [SerializeField] private bool value;
    public bool Value { get => value; set => this.value = value; }
    // public BoolVariable(string variableName, bool value)
    // {
    //     this.variableName = variableName;
    //     this.value = value;
    // }
    public BoolVariable(string variableName, bool value)
    {
        this.variableName = variableName;
        variableType = VariableType.BOOL;
        this.value = value;
    }
    public override object GetValue()
    {
        return value;
    }
    public void SetValue(bool value)
    {
        this.value = value;
    }

    // public override VariableType VariableType => VariableType.BOOL;
    
    // [SerializeField] private UnityEvent<Variable> onValueChanged;
    // public override UnityEvent<Variable> OnValueChanged { get => onValueChanged; set => onValueChanged = value; }
    // public static BoolVariable Create(string variableName, bool startingValue)
    // {
    //     BoolVariable variable = ScriptableObject.CreateInstance<BoolVariable>();
    //     variable.VariableName = variableName;
    //     variable.SetValue(startingValue);
    //     return variable;
    // }
}
