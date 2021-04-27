using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectVariable : StateVariable
{
    [SerializeField] private string variableName;
    public override string VariableName { get => variableName; set => variableName = value; }
    
    [SerializeField] private GameObject value;
    public override object Value => value;
    
    public override Type VariableType => value.GetType();
    
    public void SetValue(GameObject value)
    {
        this.value = value;
    }
    
    public static GameObjectVariable CreateVariable(string varName)
    {
        GameObjectVariable variable = CreateInstance<GameObjectVariable>();
        variable.variableName = varName;
        variable.value = null;
        return variable;
    }
    
    public static GameObjectVariable CreateVariable(string varName, GameObject startValue)
    {
        GameObjectVariable variable = CreateInstance<GameObjectVariable>();
        variable.variableName = varName;
        variable.value = startValue;
        return variable;
    }
}
