using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class GameObjectVariable : KeyVariable
{
    [SerializeField] private string variableName;
    public override string VariableName { get => variableName; set => variableName = value; }
    
    [SerializeField] private GameObject value;
    public override object GetValue()
    {
        return value;
    }

    public void SetValue(GameObject value)
    {
        this.value = value;
    }
}
