using System;
using JetBrains.Annotations;
using UnityEngine;

[Serializable]
public struct PerceptionInfo
{
    [SerializeField, CanBeNull] private GameObject perceivedObject;
    public GameObject PerceivedObject { get => perceivedObject; set => perceivedObject = value; }
    
    [SerializeField] private Vector3 location;
    public Vector3 Location { get => location; set => location = value; }
    
    public PerceptionInfo(GameObject perceivedObject, Vector3 location)
    {
        this.perceivedObject = perceivedObject;
        this.location = location;
    }
}
