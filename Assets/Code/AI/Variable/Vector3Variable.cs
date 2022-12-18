using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Vector3Variable : Variable
{
    [SerializeField] private string variableName;
    public override string VariableName { get => variableName; set => variableName = value; }
    
    [SerializeField] private VariableType variableType;
    public override VariableType VariableType { get => variableType; set => variableType = value; }
    
    [SerializeField] private Vector3 value;
    public Vector3 Value { get => value; set => this.value = value; }
    
    public Vector3Variable(string variableName, Vector3 value)
    {
        this.variableName = variableName;
        variableType = VariableType.VECTOR3;
        this.value = value;
    }
    
    public override object GetValue()
    {
        return value;
    }
    
    public void SetValue(Vector3 value)
    {
        this.value = value;
    }

    // [SerializeField] private UnityEvent<Variable> onValueChanged;
    // public override UnityEvent<Variable> OnValueChanged { get => onValueChanged; set => onValueChanged = value; }
    // public static Vector3Variable Create(string variableName, Vector3 value)
    // {
    //     Vector3Variable variable = ScriptableObject.CreateInstance<Vector3Variable>();
    //     variable.VariableName = variableName;
    //     variable.SetValue(value);
    //     return variable;
    // }
}
