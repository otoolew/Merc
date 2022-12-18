using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class GameObjectVariable : Variable
{
    [SerializeField] private string variableName;
    public override string VariableName { get => variableName; set => variableName = value; }
    
    [SerializeField] private VariableType variableType;
    public override VariableType VariableType { get => variableType; set => variableType = value; }
    
    [SerializeField] private GameObject value;
    public GameObject Value { get => value; set => this.value = value; }
    
    public GameObjectVariable(string variableName, GameObject value)
    {
        this.variableName = variableName;
        variableType = VariableType.GAMEOBJECT;
        this.value = value;
    }
    public override object GetValue()
    {
        return value;
    }

    public void SetValue(GameObject value)
    {
        this.value = value;
    }
    
    // [SerializeField] private UnityEvent<Variable> onValueChanged;
    // public override UnityEvent<Variable> OnValueChanged { get => onValueChanged; set => onValueChanged = value; }
    
    // public static GameObjectVariable Create(string variableName, GameObject startingValue)
    // {
    //     GameObjectVariable variable = ScriptableObject.CreateInstance<GameObjectVariable>();
    //     variable.VariableName = variableName;
    //     variable.SetValue(startingValue);
    //     return variable;
    // }
}
