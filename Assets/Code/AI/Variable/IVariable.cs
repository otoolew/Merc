using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVariable 
{
    public VariableType VariableType { get; set; }
    public string VariableName { get; set; }
    public object GetValue { get;}
}
