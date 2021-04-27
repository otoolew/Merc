using System;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

public class FloatVariable : StateVariable
{
    [SerializeField] private string variableName;
    public override string VariableName { get => variableName; set => variableName = value; }
    
    [SerializeField] private float value;
    public override object Value => value;

    public override Type VariableType => value.GetType();

    public void SetValue(float value)
    {
        this.value = value;
    }
    
    public static FloatVariable CreateVariable(string varName)
    {
        FloatVariable variable = CreateInstance<FloatVariable>();
        variable.variableName = varName;
        variable.value = 0.0f;
        return variable;
    }
    
    public static FloatVariable CreateVariable(string varName, float startValue)
    {
        FloatVariable variable = CreateInstance<FloatVariable>();
        variable.variableName = varName;
        variable.value = startValue;
        return variable;
    }
}
