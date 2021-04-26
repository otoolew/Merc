using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterFocus : MonoBehaviour
{
    #region Components
    [SerializeField] private Character owner;
    public Character Owner { get => owner; set => owner = value; }
    #endregion

    [SerializeField] private Transform focusTransform;
    public Transform FocusTransform { get => focusTransform; set => focusTransform = value; }

    [SerializeField] private Vector3 defaultPosition;
    public Vector3 DefaultPosition { get => defaultPosition; set => defaultPosition = value; }
    
    // Start is called before the first frame update
    private void Start()
    {
        ResetAim();
    }
    
    public void ResetAim()
    {
        focusTransform.position = owner.transform.position + defaultPosition;
    }
    
    #region Editor
    /// <summary>
    /// On Validate is only called in Editor. By performing checks here was can rest assured they will not be null.
    /// Usually what is in the Components region is in here.
    /// </summary>
    protected void OnValidate()
    {
        if (owner == null)
        {
            Debug.LogError("No Character assigned");
        }
    }
    
    #endregion
}
