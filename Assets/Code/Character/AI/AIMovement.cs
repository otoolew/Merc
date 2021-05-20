using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.AI;

public class AIMovement : CharacterMovement
{
    #region Components
    [SerializeField] private Rigidbody rigidbodyComp;
    public Rigidbody RigidbodyComp { get => rigidbodyComp; set => rigidbodyComp = value; }

    [SerializeField] private NavMeshAgent navAgent;
    public NavMeshAgent NavAgent { get => navAgent; set => navAgent = value; }
    
    [SerializeField] private PatrolCircuit patrolCircuit;
    public PatrolCircuit PatrolCircuit { get => patrolCircuit; set => patrolCircuit = value; }

    [SerializeField] private int currentWaypointIndex;
    public int CurrentWaypointIndex { get => currentWaypointIndex; set => currentWaypointIndex = value; }
    
    #endregion
    
    #region Values
    [SerializeField] private float accelerationSpeed;
    public float AccelerationSpeed { get => accelerationSpeed; set => accelerationSpeed = value; }

    [SerializeField] private float stoppingDistance;
    public float StoppingDistance { get => stoppingDistance; set => stoppingDistance = value; }

    [SerializeField] private float strafeDistance;
    public float StrafeDistance { get => strafeDistance; set => strafeDistance = value; }
    
    #endregion

    #region Properties
    public Vector3 StrafeLeftPosition => transform.right * -strafeDistance;
    public Vector3 StrafeRightPosition => transform.right * strafeDistance;
    public Vector3 CurrentDestination { get => NavAgent.destination; set => NavAgent.destination = value; }
    
    #endregion

    // Start is called before the first frame update
    private void Start()
    {
        if (navAgent)
        {
            navAgent.stoppingDistance = stoppingDistance;
            navAgent.speed = MoveSpeed;
            navAgent.angularSpeed = RotationSpeed;
            navAgent.acceleration = accelerationSpeed;
        }
    }
    
    public override void Move(Vector3 moveVector)
    {
        if (navAgent.isActiveAndEnabled)
        {
            navAgent.destination = moveVector;
        }
        else
        {
            Debug.Log("Path was stale... Calculating new one.");
            SetDestination(moveVector);
        }
    }
    
    public override void RotateTo(Vector3 value)
    {
        Vector3 direction = (Vector3.right * value.x) + (Vector3.forward * value.y);
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        if (targetRotation.eulerAngles != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
        }
    }
    
    public void ContinueMovement()
    {
        if (navAgent != null)
        {
            if (navAgent.isPathStale)
            {
                navAgent.SetDestination(CurrentDestination);
            }
            navAgent.isStopped = false;
        }
    }

    /// <summary>
    /// Stop AI Movement but can continue
    /// </summary>
    public void HaltMovement()
    {
        if (!(navAgent is null))
        {
            navAgent.isStopped = true;
        }
    }
    /// <summary>
    /// Set to position to current and finished path nav.
    /// </summary>
    public void HardStop()
    {
        SetDestination(transform.position);
    }

    public override void SetDestination(Vector3 moveVector)
    {
        if (navAgent.isActiveAndEnabled)
        {
            CurrentDestination = moveVector;
        }
    }

    public bool HasArrived()
    {
        return Vector3.Distance(transform.position, CurrentDestination) <= navAgent.stoppingDistance;
    }
    
    public void ContinueToPatrolPoint()
    {
        if (CurrentWaypointIndex < patrolCircuit.PatrolpointList.Count)
        {
            SetDestination(patrolCircuit.PatrolpointList[CurrentWaypointIndex].transform.position);
        }
    }
    
    public void GoToNextPatrolPoint()
    {
        CurrentWaypointIndex++;
        if (CurrentWaypointIndex >= patrolCircuit.PatrolpointList.Count)
        {
            CurrentWaypointIndex = 0;
        }

        SetDestination(patrolCircuit.PatrolpointList[CurrentWaypointIndex].transform.position);
    }
    
    public void RandomLocationInRadius()
    {
        CurrentWaypointIndex++;
        if (CurrentWaypointIndex >= patrolCircuit.PatrolpointList.Count)
        {
            CurrentWaypointIndex = 0;
        }

        SetDestination(patrolCircuit.PatrolpointList[CurrentWaypointIndex].transform.position);
    }
    
    #region Editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        var position = transform.position;
        Gizmos.DrawRay(position, StrafeLeftPosition);
        Gizmos.DrawRay(position, StrafeRightPosition);
        Gizmos.DrawCube(CurrentDestination + new Vector3(0,0.5f,0), new Vector3(1.0f, 0.5f, 1.0f));
    }
    #endregion
}
