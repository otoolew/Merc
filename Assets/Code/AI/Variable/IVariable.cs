using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVariable
{
    string VariableName { get; set; }
    VariableType VariableType { get; set; }
    object GetValue();
}
