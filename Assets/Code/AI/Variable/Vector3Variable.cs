using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Vector3Variable : KeyVariable
{
    [SerializeField] private string variableName;
    public override string VariableName { get => variableName; set => variableName = value; }
    
    [SerializeField] private Vector3 value;
    public override object GetValue()
    {
        return value;
    }
    
    public void SetValue(Vector3 value)
    {
        this.value = value;
    }
}
