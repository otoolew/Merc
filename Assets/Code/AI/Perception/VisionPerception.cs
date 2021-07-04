using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Events;
public class VisionPerception : MonoBehaviour
{
    #region Components

    [SerializeField] private Character owner;
    public Character Owner { get => owner; set => owner = value; }

    [SerializeField] private SphereCollider visionCollider;
    public SphereCollider VisionCollider { get => visionCollider; set => visionCollider = value; }
    
    [SerializeField] private Timer lostTargetTimer;
    public Timer LostTargetTimer { get => lostTargetTimer; set => lostTargetTimer = value; }
    
    //TODO Implement Forget Current Target Timer
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

    [SerializeField] private List<Character> detectedList;
    public List<Character> DetectedList { get => detectedList; set => detectedList = value; }
    public bool HasTarget => currentTarget != null && currentTarget.IsValid();
    
    [SerializeField, CanBeNull] private Character currentTarget;
    public Character CurrentTarget { get => currentTarget; set => currentTarget = value; }
    
    [SerializeField] private Vector3 targetLastKnownLocation;
    public Vector3 TargetLastKnownLocation { get => targetLastKnownLocation; set => targetLastKnownLocation = value; }
    
    #endregion
    
    [SerializeField] private UnityEvent<Character> onPerceptionUpdate;
    public UnityEvent<Character> OnPerceptionUpdate { get => onPerceptionUpdate; set => onPerceptionUpdate = value; }
    
    
#region Monobehaviour
    private void Start()
    {
        lostTargetTimer = new Timer(2) {TimerCompleteAction = () => currentTarget = null};
        detectedList = new List<Character>();
    }

    private void Update()
    {
        // Check for Line of Sight if not in line of sight, tick forget timer...
        if (detectedList.Count > 0)
        {
            FindBestTargetFromList();
            
            if (TargetInLineOfSight())
            {
                lostTargetTimer.ResetTimer();
            }
            else
            {
                lostTargetTimer.Tick();
            }
        }
    }
    
    private void OnTriggerEnter(Collider other)
    {
        Character detectedCharacter = other.GetComponent<Character>();
        if(detectedCharacter == null || !detectedCharacter.IsValid())
        {
            return;
        }

        if (detectedCharacter != null && !IsTagIgnored(detectedCharacter.tag))
        {
            AddToFromList(detectedCharacter);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        Character detectedCharacter = other.GetComponent<Character>();
        if (detectedCharacter != null)
        {
            RemoveFromList(detectedCharacter);
            if (detectedCharacter == currentTarget)
            {
                currentTarget = null;
            }
        }
        FindBestTargetFromList();
    }

    private void OnDestroy()
    {
        onPerceptionUpdate.RemoveAllListeners();
    }
    #endregion
    
    public bool IsTagIgnored(string tagValue) 
    {
        for (int i = 0; i < ignoreTags.Length; i++)
        {
            if (tagValue.Equals(ignoreTags[i]))
            {
                return true;
            }
        }
        return false; 
    }

    public Vector3 DirectionFromAngle(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
    
    private bool HasLineOfSight(Vector3 location)
    {
        var position = transform.position;
        Vector3 directionToTarget = (location - position).normalized;
        Ray ray = new Ray
        {
            origin = position + new Vector3(0,1,0),
            direction = directionToTarget,
        };
        
        RaycastHit[] hitResults = new RaycastHit[1];
        if (Physics.RaycastNonAlloc(ray, hitResults, radius, detectionLayer) > 0)
        {
            if (hitResults[0].collider.gameObject.GetComponent<Character>() == currentTarget)
            {
                return true;
            }
        }

        return false;
    }
    
    public bool TargetInLineOfSight()
    {
        if (currentTarget is null)
        {
            return false;
        }
        
        var position = transform.position;
        var targetPosition = currentTarget.transform.position;
        Vector3 directionToTarget = (targetPosition - position).normalized;
        float distance = Vector3.Distance(position, targetPosition);
        Ray ray = new Ray
        {
            origin = position + new Vector3(0,1,0),
            direction = directionToTarget,
        };
        
        RaycastHit[] hitResults = new RaycastHit[1];
        if (Physics.RaycastNonAlloc(ray, hitResults, radius, detectionLayer) > 0)
        {
            if (hitResults[0].collider.gameObject.GetComponent<Character>() == currentTarget)
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

                    if (Physics.Raycast(ray, out RaycastHit raycastHit, distance, detectionLayer))
                    {  
                        Character detectedCharacter = raycastHit.collider.GetComponent<Character>();
                        if (!(detectedCharacter is null) && !IsTagIgnored(detectedCharacter.tag))
                        {
                            currentTarget = detectedCharacter;
                            targetLastKnownLocation = currentTarget.transform.position;
                            onPerceptionUpdate.Invoke(currentTarget);
                            return true;
                        }
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

    private void AddToFromList(Character character)
    {
        if (character)
        {
            character.HealthComp.Died.AddListener(OnTargetDeath);
        }

        if (!detectedList.Contains(character)) 
        {
            detectedList.Add(character);
        }
    }

    private void RemoveFromList(Character character)
    {
        if (character)
        {
            character.HealthComp.Died.RemoveListener(OnTargetDeath);
        }

        if (detectedList.Contains(character))
        {
            detectedList.Remove(character);
        }     
        
        if(character == currentTarget)
        {
            currentTarget = null;
            FindBestTargetFromList();
        }
    }
    
    private void OnTargetDeath(Character character)
    {
        Debug.Log(" Target Died " + character.name);
        RemoveFromList(character);
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
}
