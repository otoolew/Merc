using System;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

[Serializable]
public class FloatVariable : Variable
{
    [SerializeField] private string variableName;
    public override string VariableName { get => variableName; set => variableName = value; }
    public override VariableType VariableType => VariableType.FLOAT;
    
    [SerializeField] private float value;
    public override object GetValue()
    {
        return value;
    }

    public void SetValue(float value)
    {
        this.value = value;
    }
    public static FloatVariable Create(string variableName, float startingValue)
    {
        FloatVariable variable = ScriptableObject.CreateInstance<FloatVariable>();
        variable.VariableName = variableName;
        variable.SetValue(startingValue);
        return variable;
    }
}
