using System;
using System.Collections;
using System.Collections.Generic;
using Merc;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerCharacter : Character
{
    [SerializeField] private PlayerController playerController;
    public PlayerController PlayerController { get => playerController; set => playerController = value; }

    [SerializeField] private float moveSpeed;
    public float MoveSpeed
    {
        get => moveSpeed;
        set => moveSpeed = value;
    }



    #region Character

    [SerializeField] private Rigidbody rigidbodyComp;

    protected override Rigidbody RigidbodyComp
    {
        get => rigidbodyComp;
        set => rigidbodyComp = value;
    }

    [SerializeField] private Animator animatorComp;

    protected override Animator AnimatorComp
    {
        get => animatorComp;
        set => animatorComp = value;
    }

    [SerializeField] private CharacterController characterControllerComp;

    public CharacterController CharacterControllerComp
    {
        get => characterControllerComp;
        set => characterControllerComp = value;
    }

    [SerializeField] private HealthComponent healthComp;

    public override HealthComponent HealthComp
    {
        get => healthComp;
        set => healthComp = value;
    }

    #endregion

    #region Ability

    [Header("Ability Controllers")] [SerializeField]
    private WeaponAbilityController leftAbilityController;

    public WeaponAbilityController LeftAbilityController
    {
        get => leftAbilityController;
        set => leftAbilityController = value;
    }

    [SerializeField] private WeaponAbilityController rightAbilityController;

    public WeaponAbilityController RightAbilityController
    {
        get => rightAbilityController;
        set => rightAbilityController = value;
    }

    [SerializeField] private DashAbilityController dashAbilityController;

    public DashAbilityController DashAbilityController
    {
        get => dashAbilityController;
        set => dashAbilityController = value;
    }

    [Header("Ability Configs")] [SerializeField]
    private MeleeAbilityConfig meleeAbilityConfigConfig;

    public MeleeAbilityConfig MeleeAbilityConfigConfig
    {
        get => meleeAbilityConfigConfig;
        set => meleeAbilityConfigConfig = value;
    }

    [SerializeField] private RaycastAbilityConfig raycastAbilityConfig;

    public RaycastAbilityConfig RaycastAbilityConfig
    {
        get => raycastAbilityConfig;
        set => raycastAbilityConfig = value;
    }

    [SerializeField] private ProjectileAbilityConfig projectileAbilityConfig;

    public ProjectileAbilityConfig ProjectileAbilityConfig
    {
        get => projectileAbilityConfig;
        set => projectileAbilityConfig = value;
    }

    #endregion

    public UnityEvent onUseInteractable;

    #region Monobehaviour

    private void Awake()
    {
        onUseInteractable = new UnityEvent();
        SetUpAbility(LeftAbilityController, meleeAbilityConfigConfig);
        SetUpAbility(RightAbilityController, projectileAbilityConfig);
        SetUpAbility(DashAbilityController);
    }

    void Start()
    {
        HealthComp.Died.AddListener(character => OnDeath(this));
    }

    // Update is called once per frame
    void Update()
    {
        MoveCharacter(PlayerController.MoveInput);
        RotateCharacter(PlayerController.LookInput);
    }

    private void OnDestroy()
    {
        HealthComp.Died.RemoveListener(character => OnDeath(this));
        onUseInteractable.RemoveAllListeners();
    }

    #endregion

    #region PlayerCharacter

    public void SetUpPlayerUI(PlayerUI playerUI)
    {
        //Debug.Log("Setup Player UI");
        playerUI.CharacterPanel.gameObject.SetActive(true);
        playerUI.CharacterPanel.AssignCharacterUIElements(this);
    }

    private void SetUpAbility(AbilityController abilityController, AbilityConfig abilityConfig)
    {
        abilityController.Owner = this;
        abilityController.EquipAbility(abilityConfig, gameObject.tag);
    }

    private void SetUpAbility(DashAbilityController abilityController)
    {
        abilityController.Owner = this;
    }


    #endregion

    #region Character

    
    public void MoveCharacter(Vector2 value)
    {
        CharacterControllerComp.Move(new Vector3(value.x, 0.0f, value.y) * Time.deltaTime * moveSpeed);
    }

    /*public void OnLook(InputValue value)
    {
        lookInput = value.Get<Vector2>().normalized;
    }
    public void OnLook(InputValue.InputContext value)
    {
        lookInput = value.Get<Vector2>().normalized;
    }*/
    public void RotateCharacter(Vector2 value)
    {
        Vector3 direction = (Vector3.right * value.x) + (Vector3.forward * value.y);
        if (direction != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 180 * Time.deltaTime);
        }
    }

    public void OnPoint(InputValue value)
    {
        Debug.Log(value.Get<Vector2>().ToString());
    }

    public override bool IsDead()
    {
        return HealthComp.IsDead;
    }

    public override void OnDeath(Character character)
    {
        Debug.LogWarning("TODO: Implement PLAYER Character Death...");
    }
    
#endregion
    
    public override bool IsValid()
    {
        if (healthComp.IsDead)
        {
            return false;
        }
        return true;
    }

    
}
