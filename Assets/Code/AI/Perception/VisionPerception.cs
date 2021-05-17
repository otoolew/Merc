using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;
public class VisionPerception : MonoBehaviour
{
    #region Components

    [SerializeField] private Character owner;
    public Character Owner { get => owner; set => owner = value; }

    [SerializeField] private SphereCollider visionCollider;
    public SphereCollider VisionCollider { get => visionCollider; set => visionCollider = value; }
    
    [SerializeField] private TimerComponent lostTargetTimer;
    public TimerComponent LostTargetTimer { get => lostTargetTimer; set => lostTargetTimer = value; }
    
    #endregion

    #region Values

    [SerializeField] private float radius;
    public float Radius { get => radius; set => radius = value; }

    [Range(20, 360)]
    [SerializeField] private float viewAngle;
    public float ViewAngle { get => viewAngle; set => viewAngle = value; }

    [Range(5, 180)]
    [SerializeField] private float focusAngle;
    public float FocusAngle { get => focusAngle; set => focusAngle = value; }

    [SerializeField] private LayerMask detectionLayer;
    public LayerMask DetectionLayer { get => detectionLayer; set => detectionLayer = value; }
    
    [SerializeField] private string[] ignoreTags;

    [SerializeField] private List<GameObject> detectedList;
    public List<GameObject> DetectedList { get => detectedList; set => detectedList = value; }
    
    [SerializeField] private bool hasTarget;
    public bool HasTarget { get => hasTarget; set => hasTarget = value; }
    
    [SerializeField, CanBeNull] private GameObject currentTarget;
    public GameObject CurrentTarget { get => currentTarget; set => currentTarget = value; }
    
    [SerializeField] private Vector3 targetLastKnownLocation;
    public Vector3 TargetLastKnownLocation { get => targetLastKnownLocation; set => targetLastKnownLocation = value; }
    
    #endregion
    
    [SerializeField] private UnityEvent<GameObject> onPerceptionUpdate;
    public UnityEvent<GameObject> OnPerceptionUpdate { get => onPerceptionUpdate; set => onPerceptionUpdate = value; }
    
    
    
#region Monobehaviour
    private void Start()
    {
        // lostTargetTimer = new Timer(2) {TimerCompleteAction = () => currentTarget = null};
        detectedList = new List<GameObject>();
        lostTargetTimer.OnComplete += TargetLost;
    }

    private void OnEnable()
    {
        lostTargetTimer.ResetTimer();
    }

    private void Update()
    {
        TrackCurrentTarget();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (IsTagIgnored(other.gameObject.tag))
        {
            return;
        }
        AddToDetectedList(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        RemoveFromDetectedList(other.gameObject);
    }

    private void OnDestroy()
    {
        onPerceptionUpdate.RemoveAllListeners();
    }
    #endregion

    private void TrackCurrentTarget()
    {
        if (HasTarget)
        {
            if (currentTarget is null)
            {
                hasTarget = false;
                return;
            }
        }
        
        if (HasLineOfSight(currentTarget))
        {
            lostTargetTimer.ResetTimer();
            if (currentTarget is { }) targetLastKnownLocation = currentTarget.transform.position;
        }
    }

    private void TargetLost()
    {
        currentTarget = null;
        HasTarget = false;
    }
    private void AddToDetectedList(GameObject go)
    {
        Character character = (Character) go.GetComponent<Character>();
        if (character) // If the target is a character listen for the characters death
        {
            character.HealthComp.Died.AddListener(OnTargetDeath);
        }

        if (!detectedList.Contains(go)) 
        {
            detectedList.Add(go);
        }
    }

    private void RemoveFromDetectedList(GameObject go)
    {
        if (detectedList.Contains(go))
        {
            detectedList.Remove(go);
        }

        if (HasTarget && go == currentTarget)
        {
            currentTarget = null;
            RefreshPrimaryTarget();
        }
    }
    private void RemoveFromDetectedList(Character character)
    {

        if (character)
        {
            character.HealthComp.Died.RemoveListener(OnTargetDeath);
        }

        if (detectedList.Contains(character.gameObject))
        {
            detectedList.Remove(character.gameObject);
        }

        if (character.gameObject == currentTarget)
        {
            TargetLost();
        }
    }
    public bool IsTagIgnored(string tagValue)
    {
        return ignoreTags.Any(tagValue.Equals);
    }

    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    
    public bool HasLineOfSight()
    {
        return HasLineOfSight(currentTarget);
    }
    
    public bool HasLineOfSight(GameObject targetObject)
    {

        if (targetObject == null)
        {
            return false;
        }
        
        var position = transform.position;
        Vector3 directionToTarget = (targetObject.transform.position - position).normalized;
        Ray ray = new Ray
        {
            origin = position,
            direction = directionToTarget,
        };
        
        RaycastHit[] hitResults = new RaycastHit[1];
        if (Physics.RaycastNonAlloc(ray, hitResults, radius, LayerMask.NameToLayer("Default")) > 0)
        {
            if (hitResults[0].collider.gameObject == targetObject)
            {
                return true;
            }
        }

        return false;
    }
    public bool FindBestTargetFromList()
    {
        if (detectedList.Count > 0)
        {
            detectedList
                .OrderBy(go => Vector3.Distance(go.transform.position, transform.position))
                .FirstOrDefault(go => !go.CompareTag(gameObject.tag));
        
            for (int i = 0; i < detectedList.Count; i++)
            {
                Vector3 directionToTarget = (detectedList[i].transform.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
                {
                    var position = transform.position;
                    float distance = Vector3.Distance(position, detectedList[i].transform.position);
                    Ray ray = new Ray
                    {
                        origin = position + new Vector3(0,1,0),
                        direction = directionToTarget,
                    };

                    if (HasLineOfSight(detectedList[i]))
                    {  
                        onPerceptionUpdate.Invoke(currentTarget);
                        
                        return true;
                    }
                }
            }
            return false;
        }
        else
        {
            return false;
        }
    }
    
    private void RefreshPrimaryTarget()
    {
        if (detectedList.Count > 0)
        {
            // Sort by Distance
            detectedList
                .OrderBy(go => Vector3.Distance(go.transform.position, transform.position))
                .FirstOrDefault(go => !go.CompareTag(gameObject.tag));
        
            
            for (int i = 0; i < detectedList.Count; i++)
            {
                Vector3 directionToTarget = (detectedList[i].transform.position - transform.position).normalized;
                if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
                {
                    var position = transform.position;
                    float distance = Vector3.Distance(position, detectedList[i].transform.position);
                    Ray ray = new Ray
                    {
                        origin = position + new Vector3(0,1,0),
                        direction = directionToTarget,
                    };

                    if (HasLineOfSight(detectedList[i]))
                    {  
                        onPerceptionUpdate.Invoke(currentTarget);
                    }
                }
            }
        }
    }
    
    private void OnTargetDeath(Character character)
    {
        Debug.Log(" Target Died " + character.name);
        RemoveFromDetectedList(character);
    }
    
    private void OnValidate()
    {
        if (visionCollider)
        {
            visionCollider.radius = radius;
        }
        if(owner == null)
        {
            Debug.LogError("No Owner. Please Set the Owner");
        }
    }

    private void OnDrawGizmos()
    {
        if (HasTarget)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, currentTarget.transform.position);
        }
    }
}
