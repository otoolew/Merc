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
    
    [SerializeField] private bool value;
    public override VariableType VariableType => VariableType.BOOL;
    
    // [SerializeField] private VariableChangedEvent onVariableChanged;
    // public override VariableChangedEvent OnVariableChanged { get => onVariableChanged; set => onVariableChanged = value; }
    public override object GetValue()
    {
        return value;
    }
    
    public void SetValue(bool value)
    {
        this.value = value;
    }
    
    public static BoolVariable Create(string variableName, bool startingValue)
    {
        BoolVariable variable = ScriptableObject.CreateInstance<BoolVariable>();
        variable.VariableName = variableName;
        variable.SetValue(startingValue);
        return variable;
    }
}
