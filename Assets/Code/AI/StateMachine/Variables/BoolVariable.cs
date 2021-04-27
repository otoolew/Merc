using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoolVariable : StateVariable
{
    [SerializeField] private string variableName;
    public override string VariableName { get => variableName; set => variableName = value; }
    
    [SerializeField] private bool value;
    public override object Value => value;
    
    public override Type VariableType => value.GetType();
    public void SetValue(bool value)
    {
        this.value = value;
    }
    
    public static BoolVariable CreateVariable(string varName)
    {
        BoolVariable variable = CreateInstance<BoolVariable>();
        variable.variableName = varName;
        variable.value = false;
        return variable;
    }
    
    public static BoolVariable CreateVariable(string varName, bool startValue)
    {
        BoolVariable variable = CreateInstance<BoolVariable>();
        variable.variableName = varName;
        variable.value = startValue;
        return variable;
    }
}
