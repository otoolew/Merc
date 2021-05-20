using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterMovement : MonoBehaviour
{
    #region Values
    [Header("Settings")]
    [SerializeField] private bool orientToMovement;
    public bool OrientToMovement { get => orientToMovement; set => orientToMovement = value; }

    [SerializeField] private float moveSpeed;
    public float MoveSpeed { get => moveSpeed; set => moveSpeed = value; }

    [SerializeField] private float rotationSpeed;
    public float RotationSpeed { get => rotationSpeed; set => rotationSpeed = value; }
    
    #endregion
    public virtual void Move()
    {

    }
    public virtual void Stop()
    {
        
    }
    public virtual void Move(Vector3 moveVector)
    {

    }
    public virtual void SetDestination(Vector3 moveVector)
    {
    }


    public virtual void OnLook(InputAction.CallbackContext callbackContext)
    {

    }

    public virtual void RotateTo(Vector3 value)
    {
        Vector3 direction = (Vector3.right * value.x) + (Vector3.forward * value.y);
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        if (targetRotation.eulerAngles != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
        }
    }
    
    public void RotateTo(Transform value)
    {
        var position = value.position;
        Vector3 direction = (Vector3.right * position.x) + (Vector3.forward * position.y);
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        if(targetRotation.eulerAngles != Vector3.zero)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, RotationSpeed * Time.deltaTime);
        }
    }
}
