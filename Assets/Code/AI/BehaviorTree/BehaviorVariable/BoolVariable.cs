using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class BoolVariable : KeyVariable
{
    [SerializeField] private string variableName;
    public override string VariableName { get => variableName; set => variableName = value; }
    
    [SerializeField] private bool value;
    public override object GetValue()
    {
        return value;
    }
    
    public void SetValue(bool value)
    {
        this.value = value;
    }
}
