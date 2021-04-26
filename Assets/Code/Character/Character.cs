using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    /// <summary>
    /// The Characters Movement Component
    /// </summary>
    protected abstract Rigidbody RigidbodyComp { get; set; }

    /// <summary>
    /// The Characters Animation Component
    /// </summary>
    protected abstract Animator AnimatorComp { get; set; }
    
    //public abstract CharacterMovement MovementComp { get; set; }
    /// <summary>
    /// The Characters Movement Component
    /// </summary>
    public abstract CharacterRotation RotationComp { get; set; }
    /// <summary>
    /// The Characters Health Component
    /// </summary>
    public abstract HealthComponent HealthComp { get; set; }
    
    /// <summary>
    /// The Location of the Character in the world.
    /// </summary>
    public Vector3 WorldLocation { get => transform.position;}

    /// <summary>
    /// Is the character a valid active player in the scene.
    /// </summary>
    /// <returns></returns>
    public abstract bool IsValid();

    /// <summary>
    /// Called when a character dies.
    /// </summary>
    /// <param name="character"></param>
    public abstract void OnDeath(Character character);
    
}
