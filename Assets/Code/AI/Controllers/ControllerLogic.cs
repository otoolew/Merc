using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public abstract class ControllerLogic : ScriptableObject
{
    public abstract AIController Controller { get; set; }
    public abstract string Description { get; set; }
    public abstract void Init(AIController controller);
    public abstract void UpdateTick();
}
