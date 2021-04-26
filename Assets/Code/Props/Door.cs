using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Door : MonoBehaviour
{
    #region Components
    
    [SerializeField] private Animator animatorComp;
    public Animator AnimatorComp { get => animatorComp; set => animatorComp = value; }
    
    [SerializeField] private Transform interactionPoint;
    public Transform InteractionPoint { get => interactionPoint; set => interactionPoint = value; }
    
    #endregion

    
    #region Variables
    
    [SerializeField] private bool isOpen;
    public bool IsOpen { get => isOpen; set => isOpen = value; }
    
    [SerializeField] private bool isClosed;
    public bool IsClosed { get => isClosed; set => isClosed = value; }
    
    [SerializeField] private bool isBlocked;
    public bool IsBlocked { get => isBlocked; set => isBlocked = value; }
    
    #endregion

    public void DoorAction()
    {
        if (IsOpen)
        {
            CloseDoor();
        }
        else
        {
            OpenDoor();
        }
    }

    public void OpenDoor()
    {
        animatorComp.SetTrigger("Open");
        IsOpen = true;
    }

    public void CloseDoor()
    {
        if (isBlocked)
        {
            Debug.Log("Door is blocked");          
        }
        else
        {
            animatorComp.SetTrigger("Close");
            IsOpen = false;
        }
    }
}
