using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public interface IPerceivable
{
    Vector3 Position { get; set; }
    Vector3 Direction { get; set; }
    float Distance { get; set; }
    float LastTimePerceived { get; set; }
}
