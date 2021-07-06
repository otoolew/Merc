using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PatrolState : StateComponent
{
    private AIController controller;
    
    // [SerializeField] private string stateName;
    // public override string StateName { get => stateName; set => stateName = value; }
    //
    // [SerializeField] private PatrolCircuit patrolCircuit;
    // public PatrolCircuit PatrolCircuit { get => patrolCircuit; set => patrolCircuit = value; }
    //
    // [SerializeField] private int currentWaypointIndex;
    // public int CurrentWaypointIndex { get => currentWaypointIndex; set => currentWaypointIndex = value; }
    //
    // public PatrolState(AIController controller, PatrolCircuit patrolCircuit, int currentWaypointIndex)
    // {
    //     this.stateName = "Patrol";
    //     this.controller = controller;
    //     this.patrolCircuit = patrolCircuit;
    //     this.currentWaypointIndex = currentWaypointIndex;
    // }
    //
    // public override void EnterState(AIController controller)
    // {
    //     Debug.Log(controller.name + " Enter State " + stateName);
    //     this.controller = controller;
    //     controller.StopAllCoroutines();
    //     controller.StartCoroutine(StateRoutine());
    // }
    //
    // public override void UpdateState(AIController controller)
    // {
    //     Debug.Log(controller.name + " Update State " + stateName);
    // }
    //
    // public override void ExitState(AIController controller)
    // {
    //     Debug.Log(controller.name + " Exit State " + stateName);
    // }
    //
    // private IEnumerator StateRoutine()
    // {
    //     Debug.Log(controller.name + " Exit State " + stateName);
    //     yield return null;
    // }
    
    // public void ContinueToPatrolPoint()
    // {
    //     if (CurrentWaypointIndex < patrolCircuit.PatrolpointList.Count)
    //     {
    //         controller.AssignedCharacter.MovementComp.SetDestination(patrolCircuit.PatrolpointList[CurrentWaypointIndex].transform.position);
    //     }
    // }
    //
    // public void GoToNextPatrolPoint()
    // {
    //     CurrentWaypointIndex++;
    //     if (CurrentWaypointIndex >= patrolCircuit.PatrolpointList.Count)
    //     {
    //         CurrentWaypointIndex = 0;
    //     }
    //
    //     controller.AssignedCharacter.MovementComp.SetDestination(patrolCircuit.PatrolpointList[CurrentWaypointIndex].transform.position);
    // }
    // public Vector3 GetCurrentPatrolPoint()
    // {
    //     return patrolCircuit.PatrolpointList[CurrentWaypointIndex].transform.position;
    // }
    //
    // public void RandomLocationInRadius()
    // {
    //     CurrentWaypointIndex++;
    //     if (CurrentWaypointIndex >= patrolCircuit.PatrolpointList.Count)
    //     {
    //         CurrentWaypointIndex = 0;
    //     }
    //
    //     controller.AssignedCharacter.MovementComp.SetDestination(patrolCircuit.PatrolpointList[CurrentWaypointIndex].transform.position);
    // }
}
