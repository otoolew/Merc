using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vector3Variable : StateVariable
{
    [SerializeField] private string variableName;
    public override string VariableName { get => variableName; set => variableName = value; }
    
    [SerializeField] private Vector3 value;
    public override object Value => value;
    
    public override Type VariableType => value.GetType();
    
    public void SetValue(Vector3 value)
    {
        this.value = value;
    }
    
    public static Vector3Variable CreateVariable(string varName)
    {
        Vector3Variable variable = CreateInstance<Vector3Variable>();
        variable.variableName = varName;
        variable.value = new Vector3(0,0,0);
        return variable;
    }
    
    public static Vector3Variable CreateVariable(string varName, Vector3 startValue)
    {
        Vector3Variable variable = CreateInstance<Vector3Variable>();
        variable.variableName = varName;
        variable.value = startValue;
        return variable;
    }
}
