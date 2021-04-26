using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetMark : ScriptableObject
{
    [SerializeField] private Character markedBy;
    public Character MarkedBy { get => markedBy; set => markedBy = value; }
    
    [SerializeField] private Transform target;
    public Transform Target { get => target; set => target = value; }

    public static TargetMark MarkTarget(Transform assignedTarget)
    {
        TargetMark mark = CreateInstance<TargetMark>();
        mark.Target = assignedTarget;
        return mark;
    }
    
}
