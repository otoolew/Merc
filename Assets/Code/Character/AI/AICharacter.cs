using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;

public class AICharacter : Character
{
    #region Components
    [SerializeField] private AIController controller;
    public AIController Controller { get => controller; set => controller = value; }

    [SerializeField] private Rigidbody rigidbodyComp;
    protected override Rigidbody RigidbodyComp { get => rigidbodyComp; set => rigidbodyComp = value; }

    [SerializeField] private Animator animatorComp;
    protected override Animator AnimatorComp { get => animatorComp; set => animatorComp = value; }

    [SerializeField] private AIMovement movementComp;
    public AIMovement MovementComp { get => movementComp as AIMovement; set => movementComp = (AIMovement)value; }

    [SerializeField] private HealthComponent healthComp;
    public override HealthComponent HealthComp { get => healthComp; set => healthComp = value; }

    [SerializeField] private Vector3 defaultPosition;
    public Vector3 DefaultPosition { get => defaultPosition; set => defaultPosition = value; }
    
    [SerializeField] private CharacterFocus characterFocus;
    public CharacterFocus CharacterFocus { get => characterFocus; set => characterFocus = value; }
    
    [SerializeField] private VisionPerception visionPerception;
    public VisionPerception VisionPerception { get => visionPerception; set => visionPerception = value; }

    #region Ability
    [Header("Ability Controllers")]
    
    [Header("Ability Configs")]
    [SerializeField] private RaycastAbilityConfig raycastAbilityConfig;
    public RaycastAbilityConfig RaycastAbilityConfig { get => raycastAbilityConfig; set => raycastAbilityConfig = value; }

    [SerializeField] private WeaponAbilityController abilityController;
    public WeaponAbilityController AbilityController { get => abilityController; set => abilityController = value; }
    #endregion

    #endregion

    #region Monobehaviour
    private void Start()
    {
        HealthComp.Died.AddListener(character => OnDeath(this));
        SetUpAbility(abilityController, raycastAbilityConfig);
        visionPerception.enabled = true;
    }
    // Update is called once per frame

    #endregion
    
    private void SetUpAbility(AbilityController abilityController, AbilityConfig abilityConfig)
    {
        abilityController.Owner = this;
        abilityController.EquipAbility(abilityConfig,gameObject.tag);
    }

    #region Character

    public void Attack()
    {
        if (VisionPerception.CurrentTarget != null)
        {
            MovementComp.StopBySet();
            MovementComp.RotateTo(VisionPerception.CurrentTarget.transform.position);
            abilityController.PullTrigger();
        }
    }
    
    public override bool IsValid()
    {
        if (HealthComp == null) return false;
        
        return !HealthComp.IsDead;
    }

    public override bool IsDead()
    {
        return HealthComp.IsDead;
    }

    #region Death
    public override void OnDeath(Character character)
    {
        Debug.Log(gameObject.name + " is dead...");
        StartCoroutine(ProcessDeathRoutine());
    }
    #endregion

    private IEnumerator ProcessDeathRoutine()
    {
        visionPerception.enabled = false;
        movementComp.StopMovement();
        //controller.PlayMaker.enabled = false;
        yield return new WaitForSeconds(1);
        Destroy(this.gameObject);
    }
    #endregion
    
    #region Editor
    
    private void OnDrawGizmos()
    {
        
        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, transform.forward * 10);

    }

    #endregion
}
