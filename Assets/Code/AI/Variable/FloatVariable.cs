using System;
using UnityEngine;

[Serializable]
public class FloatVariable : KeyVariable
{
    [SerializeField] private string variableName;
    public override string VariableName { get => variableName; set => variableName = value; }
    
    [SerializeField] private float value;
    public override object GetValue()
    {
        return value;
    }

    public void SetValue(float value)
    {
        this.value = value;
    }
}
