using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "PatrolState", menuName = "Game/AI/Patrol State")]
public class PatrolStateNode : StateNode
{
    [SerializeField] private string stateName;
    public override string StateName { get => stateName; set => stateName = value; }
    
    [SerializeField] private PatrolCircuit patrolCircuit;
    public PatrolCircuit PatrolCircuit { get => patrolCircuit; set => patrolCircuit = value; }
    
    public override void OnStateEnter()
    {
        Debug.Log("RootStateNode -> OnStateEnter.");
    }

    public override void OnStateUpdate()
    {
        Debug.Log("RootStateNode -> OnStateUpdate.");
    }

    public override void OnStateExit()
    {
        Debug.Log("RootStateNode -> OnStateExit.");
    }
    
}
