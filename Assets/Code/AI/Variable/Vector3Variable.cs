using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Vector3Variable : Variable
{
    [SerializeField] private string variableName;
    public override string VariableName { get => variableName; set => variableName = value; }
    public override VariableType VariableType => VariableType.VECTOR3;
    
    [SerializeField] private Vector3 value;
    public override object GetValue()
    {
        return value;
    }
    
    public void SetValue(Vector3 value)
    {
        this.value = value;
    }
    
    public static Vector3Variable Create(string variableName, Vector3 value)
    {
        Vector3Variable variable = ScriptableObject.CreateInstance<Vector3Variable>();
        variable.VariableName = variableName;
        variable.SetValue(value);
        return variable;
    }
}
