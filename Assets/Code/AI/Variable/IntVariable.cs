using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class IntVariable : KeyVariable
{
    [SerializeField] private string variableName;
    public override string VariableName { get => variableName; set => variableName = value; }
    
    [SerializeField] private int value;
    public override object GetValue()
    {
        return value;
    }
    
    public void SetValue(int value)
    {
        this.value = value;
    }
}
