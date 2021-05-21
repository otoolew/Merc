using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public abstract class StateInstruction: ScriptableObject
{
    public abstract string Description { get; set; }
    public abstract void Perform(AIController controller);
}
